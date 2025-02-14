using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IClassRepository
{
    Task<Class> Create(Class @class, CancellationToken cancellationToken);
    Task<Class> Update(Class @class, CancellationToken cancellationToken);
    Task<Class> Delete(Class @class, CancellationToken cancellationToken);
}