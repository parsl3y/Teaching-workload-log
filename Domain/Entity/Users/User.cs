using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity;

public class User
{
    [BsonId]
    public UserId Id { get; set; }
    
    [BsonElement("first_name"), BsonRepresentation(BsonType.String)]
    public string? FirstName { get; set; }
    
    [BsonElement("sur_name"), BsonRepresentation(BsonType.String)]
    public string? SurName { get; set; }
    
    [BsonElement("middle_name"), BsonRepresentation(BsonType.String)]
    public string? MiddleName { get; set; }
    
    [BsonIgnore]
    public string FullName => $"{FirstName} {MiddleName} {SurName}".Trim();


}