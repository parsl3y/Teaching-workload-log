using Domain.Entity;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IClassQuery
{
    Task<IEnumerable<Class>> GetAll(CancellationToken cancellationToken);
    Task<Option<Class>> GetById(ClassId id, CancellationToken cancellationToken);
    Task<Option<IEnumerable<Class>>> GetByClassName(string className, CancellationToken cancellationToken);

    Task<Option<Class>> GetByClassNameAndNumber(string className, int classNumberToday, CancellationToken cancellationToken);
}