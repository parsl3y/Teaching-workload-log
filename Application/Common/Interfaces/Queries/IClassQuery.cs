using Domain.Entity;

namespace Application.Common.Interfaces.Queries;

public interface IClassQuery
{
    Task<IEnumerable<Classes>> GetAllAsync();
    Task<Classes> GetByIdAsync(string id);
}