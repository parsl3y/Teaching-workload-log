using Domain.Entity.TeacherTab;

namespace Application.Common.Interfaces.Repositories;

public interface ITeacherTabRepository
{
    Task<TeacherTab> Create(TeacherTab documentation, CancellationToken cancellationToken);
    Task<TeacherTab> Delete(TeacherTab documentation, CancellationToken cancellationToken);
}