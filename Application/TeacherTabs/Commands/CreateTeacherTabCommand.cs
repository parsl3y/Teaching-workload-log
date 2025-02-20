using Application.Common;
using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Application.TeacherTabs.Exceptions;
using Domain.Entity;
using Domain.Entity.TeacherTab;
using MediatR;

namespace Application.TeacherTabs.Commands;

public class CreateTeacherTabCommand : IRequest<Result<TeacherTab, TeacherTabException>>
{
    public required Guid ClassId { get; set; }
    public required string Theme { get; set; }
    public required int HourCount { get; set; }
}

public class CreateTeacherTabCommandHandler : IRequestHandler<CreateTeacherTabCommand, Result<TeacherTab, TeacherTabException>>
{
    private readonly IClassQuery _classQuery;
    private readonly ITeacherTabRepository _teacherTabRepository;
    private readonly ITeacherTabQuery _teacherTabQuery;

    public CreateTeacherTabCommandHandler(IClassQuery classQuery, ITeacherTabRepository teacherTabRepository, ITeacherTabQuery teacherTabQuery)
    {
        _classQuery = classQuery;
        _teacherTabRepository = teacherTabRepository;
        _teacherTabQuery = teacherTabQuery;
    }

    public async Task<Result<TeacherTab, TeacherTabException>> Handle(CreateTeacherTabCommand request,
        CancellationToken cancellationToken)
    {
        var classId = new ClassId(request.ClassId);

        var classEntity = await _classQuery.GetById(classId, cancellationToken);
        return await classEntity.Match(
            async c => await CreateEntity(c.Id, request.Theme, request.HourCount, cancellationToken),
            () => Task.FromResult<Result<TeacherTab, TeacherTabException>>(new LessonInDocumentationNotFoundException(classId)) 
        );
    }

    private async Task<Result<TeacherTab, TeacherTabException>> CreateEntity(
        ClassId classId,
        string theme,
        int hourCount,
        CancellationToken cancellationToken)
    {
        var existingTeacherTab = await _teacherTabQuery.GetByClassId(classId, cancellationToken);
        if (existingTeacherTab.HasValue)
        {
            return new DocumentOfThisClassAlreadyExists(classId);
        }

        try
        {
            var entity = TeacherTab.New(TeacherTabId.New(), classId, theme,  2);
            return await _teacherTabRepository.Create(entity, cancellationToken);
        }
        catch (Exception ex)
        {
            return new DocumentationUnknownExceptions(TeacherTabId.Empty(), ex);
        }
    }
}

