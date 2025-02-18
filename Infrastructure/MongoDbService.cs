using Application.Common.Serializer; // замість Application.Common.Serrealizer
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Domain.Entity;

namespace Infrastructure
{
    public class MongoDbService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _database;

        public MongoDbService(IConfiguration configuration)
        {
            _configuration = configuration;
            
            var connectionString = _configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);

            RegisterSerializers();
        }
        
        public IMongoDatabase Database => _database;

        private void RegisterSerializers()
        {
            BsonSerializer.RegisterSerializer(new GuidBasedSerializer<ClassId>());
            BsonSerializer.RegisterSerializer(new GuidBasedSerializer<UserId>());
        }
    }
}