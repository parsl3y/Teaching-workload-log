using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Entity;
using MongoDB.Driver;
using Optional;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository, IUserQuery
{
    private readonly IMongoCollection<User> _users;
    
    public UserRepository(MongoDbService mongoDbService)
    {
        _users = mongoDbService.Database.GetCollection<User>("users");
    }

    public async Task<IEnumerable<User>> GetAll(CancellationToken cancellationToken)
    {
        return await _users.Find(user => true).ToListAsync(cancellationToken);
    }
    
    public async Task<Option<User>> GetById(UserId id,CancellationToken cancellationToken)
    {
        var entity = await _users.Find(user => user.Id == id).FirstOrDefaultAsync(cancellationToken);
        return entity == null ? Option.None<User>() : Option.Some(entity);
    }
    
    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        await _users.InsertOneAsync(user, cancellationToken: cancellationToken);
        return user;
    }

    public async Task<Option<User>> SearchBySurName(string surName, CancellationToken cancellationToken)
    {
        var entity = await _users.Find(u => u.SurName == surName).FirstOrDefaultAsync(cancellationToken);
        return entity == null ? Option.None<User>() : Option.Some(entity);
    }
    
    public async Task<User> Update(User user, CancellationToken cancellationToken)
    {
       await _users.ReplaceOneAsync(x => x.Id == user.Id, user, cancellationToken: cancellationToken );
       return user;
    }
    
    public async Task<User> Delete(User user, CancellationToken cancellationToken)
    {
       await _users.DeleteOneAsync(x => x.Id == user.Id, cancellationToken);
       return user;
    }
}