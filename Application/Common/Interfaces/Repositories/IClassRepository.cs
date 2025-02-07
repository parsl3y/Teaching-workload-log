using Domain.Entity;

namespace Application.Common.Interfaces.Repositories;

public interface IClassRepository
{
    Task CreateAsync(Classes classes);
    Task UpdateAsync(string id, Classes classes);
    Task DeleteAsync(string id);
}