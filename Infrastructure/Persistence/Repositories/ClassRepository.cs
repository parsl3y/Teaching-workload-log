using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using MongoDB.Driver;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class ClassRepository : IClassRepository, IClassQuery
{
    private readonly IMongoCollection<Class> _classes;

    public ClassRepository(MongoDbService mongoDbService)
    {
        _classes = mongoDbService.Database.GetCollection<Class>("classes");
    }

    public async Task<IEnumerable<Class>> GetAll(CancellationToken cancellationToken)
    {
        return await _classes.Find(classes => true).ToListAsync(cancellationToken);
    }
    
    public async Task<Option<Class>> GetById(ClassId id,CancellationToken cancellationToken)
    {
        var entity = await _classes.Find(classes => classes.Id == id).FirstOrDefaultAsync(cancellationToken);
        return entity == null ? Option.None<Class>() : Option.Some(entity);
    }
    
    public async Task<Class> Create(Class @class, CancellationToken cancellationToken)
    {
        await _classes.InsertOneAsync(@class, cancellationToken: cancellationToken);
        return @class;
    }

    public async Task<Option<Class>> GetByClassName(string className, CancellationToken cancellationToken)
    {
        var entity = await _classes.Find(c => c.ClassName == className).FirstOrDefaultAsync(cancellationToken);
        return entity == null ? Option.None<Class>() : Option.Some(entity);
    }
    
    public async Task<Class> Update(Class @class, CancellationToken cancellationToken)
    {
        await _classes.ReplaceOneAsync(x => x.Id == @class.Id, @class, cancellationToken: cancellationToken );
        return @class;
    }
    
    public async Task<Class> Delete(Class @class, CancellationToken cancellationToken)
    {
        await _classes.DeleteOneAsync(x => x.Id == @class.Id, cancellationToken);
        return @class;
    }
}
