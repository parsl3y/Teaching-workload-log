using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class ClassRepository : IClassRepository, IClassQuery
{
    private readonly IMongoCollection<Classes> _classes;

    public ClassRepository(MongoDbService mongoDbService)
    {
        _classes = mongoDbService.Database.GetCollection<Classes>("classes");
    }

    public async Task<IEnumerable<Classes>> GetAllAsync()
    {
        return await _classes.Find(FilterDefinition<Classes>.Empty).ToListAsync();
    }

    public async Task<Classes> GetByIdAsync(string id)
    {
        var filter = Builders<Classes>.Filter.Eq(x => x.Id, id);
        var classes = await _classes.Find(filter).FirstOrDefaultAsync();

        if (classes == null)
        {
            throw new KeyNotFoundException($"Class with id {id} not found.");
        }

        return classes;
    }

    public async Task CreateAsync(Classes classes)
    {
        await _classes.InsertOneAsync(classes);
    }

    public async Task UpdateAsync(string id, Classes classes)
    {
        var filter = Builders<Classes>.Filter.Eq(x => x.Id, id);
        await _classes.ReplaceOneAsync(filter, classes);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<Classes>.Filter.Eq(x => x.Id, id);
        await _classes.DeleteOneAsync(filter);
    }
}
