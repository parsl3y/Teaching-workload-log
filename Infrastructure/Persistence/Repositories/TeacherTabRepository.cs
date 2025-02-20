using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using Domain.Entity.TeacherTab;
using MongoDB.Driver;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class TeacherTabRepository : ITeacherTabRepository, ITeacherTabQuery
{
    private readonly IMongoCollection<TeacherTab> _documents;
    private readonly IMongoCollection<Class> _classes;

    public TeacherTabRepository(MongoDbService mongoDbService)
    {
        _documents = mongoDbService.Database.GetCollection<TeacherTab>("TeacherTabs");
        _classes = mongoDbService.Database.GetCollection<Class>("classes");

    }

    public async Task<IEnumerable<TeacherTab>> GetAll(CancellationToken cancellationToken)
    {
        return await _documents.Find(document => true).ToListAsync(cancellationToken);
    }

    public async Task<Option<TeacherTab>> GetById(TeacherTabId id,CancellationToken cancellationToken)
    {
        var entity = await _documents.Find(documents => documents.Id == id).FirstOrDefaultAsync(cancellationToken);
        return entity == null ? Option.None<TeacherTab>() : Option.Some(entity);
    }
    
    public async Task<TeacherTab> Create(TeacherTab document, CancellationToken cancellationToken)
    {
        await _documents.InsertOneAsync(document, cancellationToken: cancellationToken);
        return document;
    }
    
    public async Task<TeacherTab> Delete(TeacherTab document, CancellationToken cancellationToken)
    {
        await _documents.DeleteOneAsync(x => x.Id == document.Id, cancellationToken);
        return document;
    }
    public async Task<IEnumerable<TeacherTab>> GetByClassName(string className, CancellationToken cancellationToken)
    {
        var classIds = await _classes
            .Find(c => c.ClassName == className)
            .Project(c => c.Id)
            .ToListAsync(cancellationToken);

        if (!classIds.Any()) 
            return new List<TeacherTab>();
        
        return await _documents
            .Find(t => classIds.Contains(t.ClassId))
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Option<TeacherTab>> GetByClassId(ClassId classId, CancellationToken cancellationToken)
    {
        var entity = await _documents.Find(t => t.ClassId == classId).FirstOrDefaultAsync(cancellationToken);
        return entity == null ? Option.None<TeacherTab>() : Option.Some(entity);
    }

}