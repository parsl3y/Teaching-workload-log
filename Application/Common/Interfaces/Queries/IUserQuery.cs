using Domain.Entity;
using Optional;

namespace Application.Common.Interfaces.Queries;

public interface IUserQuery
{
    Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken);
    Task<Option<User>> GetById(UserId id, CancellationToken cancellationToken);
    Task<Option<User>> SearchBySurName(string surName, CancellationToken cancellationToken);
}