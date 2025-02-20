using Domain.Entity;
using Domain.Entity.TeacherTab;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface ITeacherTabQuery
{
    Task<IEnumerable<TeacherTab>> GetAll(CancellationToken cancellationToken);
    Task<Option<TeacherTab>> GetById(TeacherTabId id, CancellationToken cancellationToken);
    Task<IEnumerable<TeacherTab>> GetByClassName(string className, CancellationToken cancellationToken);
    Task<Option<TeacherTab>> GetByClassId(ClassId classId, CancellationToken cancellationToken);
}