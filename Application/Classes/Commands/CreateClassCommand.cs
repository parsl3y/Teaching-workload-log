using Application.Classes.Exceptions;
using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using MediatR;

namespace Application.Classes.Commands;

public class CreateClassCommand : IRequest <Result<Class, ClassException>>
{
    public required string ClassName { get; init; }
    public required int TotalClassNumber { get; init; }
    public required int CLassNumberToday { get; init; }
    public required Guid? TeacherId { get; init; }
    public required DateTime Date { get; init; }
}

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Result<Class, ClassException>>
{
    private readonly IClassRepository _classRepository;
    private readonly IClassQuery _classQuery;
    private readonly IUserQuery _userQuery;


    public CreateClassCommandHandler(
        IClassRepository classRepository,
        IClassQuery classQuery, IUserQuery userQuery)
    {
        _classRepository = classRepository;
        _classQuery = classQuery;
        _userQuery = userQuery;
    }

    public async Task<Result<Class, ClassException>> Handle(
        CreateClassCommand request,
        CancellationToken cancellationToken)
    {
        var teacherId = new UserId(request.TeacherId);

        var teacher = await _userQuery.GetById(teacherId, cancellationToken);
        return await teacher.Match(
            async t =>
                {
                    var existingClass =
                        await _classQuery.GetByClassName(request.ClassName, cancellationToken);
                    return await existingClass.Match(
                        c => Task.FromResult<Result<Class, ClassException>>(
                            new ClassAlreadExistsException(c.Id),

                            async () => await CreateEntity(request.ClassName, request.TotalClassNumber,
                                request.CLassNumberToday, t.Id, request.Date, cancellationToken)
                        ));
                },

                () => Task.FromResult<Result<Class, ClassException>>(
            new UserClassNotFoundException(teacherId)));
    }


private async Task<Result<Class, ClassException>> CreateEntity(
            string className,
            int totalClassNumber,
            int classNumberToday,
            UserId? userId,
            DateTime date,
            CancellationToken cancellationToken)
    {
            try
            {
                var entity = Class.New(ClassId.New(), className, totalClassNumber, classNumberToday, userId, date);

                return await _classRepository.Create(entity, cancellationToken);
            }
            catch (Exception e)
            {
                return new ClassUknownException(ClassId.Empty(), e);
            }
    }
            
    }
}