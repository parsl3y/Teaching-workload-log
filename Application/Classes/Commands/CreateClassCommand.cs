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
    public required int TotalClassNumber  { get; init; }
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
        if (request.TeacherId.HasValue)
        {
            var teacherId = new UserId(request.TeacherId.Value);
            var teacher = await _userQuery.GetById(teacherId, cancellationToken);
        
            return await teacher.Match(
                async t => await ProcessClassCreation(request, t.Id, cancellationToken),
                () => Task.FromResult<Result<Class, ClassException>>(
                    new UserClassNotFoundException(teacherId))
            );
        }

        return await ProcessClassCreation(request, null, cancellationToken);
    }

    private async Task<Result<Class, ClassException>> ProcessClassCreation(
        CreateClassCommand request,
        UserId? teacherId,
        CancellationToken cancellationToken)
    {
        var existingClasses = await _classQuery.GetByClassName(request.ClassName, cancellationToken);
    
        int newClassNumberToday = 1; 

        await existingClasses.Match(
            async classes =>
            {
                int maxClassNumberToday = classes.Max(c => c.ClassNumberToday);
                newClassNumberToday = maxClassNumberToday + 1;

                if (newClassNumberToday > request.TotalClassNumber + 1)
                {
                    await Task.FromResult<Result<Class, ClassException>>(
                        new ClassLimitExceededException(request.ClassName, request.TotalClassNumber));
                }
            },
            () => Task.CompletedTask 
        );

        if (newClassNumberToday > request.TotalClassNumber)
        {
            return new ClassLimitExceededException(request.ClassName, request.TotalClassNumber);
        }

        return await CreateEntity(request.ClassName, request.TotalClassNumber,
            newClassNumberToday, teacherId, request.Date, cancellationToken);
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
            var entity = Class.New(ClassId.New(), className, totalClassNumber, classNumberToday, userId, DateTime.UtcNow);
            
            return await _classRepository.Create(entity, cancellationToken);
        }
        catch (Exception e)
        {
            return new ClassUknownException(ClassId.Empty(), e);
        }
    }

}
