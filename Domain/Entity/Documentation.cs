using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity;

public class Documentation
{
    [BsonId]
    public string? Id {get; set;}
    
    [BsonElement("class_id"), BsonRepresentation(BsonType.ObjectId)]
    public string ClassId {get;}

    [BsonElement("class_theme"), BsonRepresentation(BsonType.String)]
    public string ClassTheme { get; }
    
    [BsonElement("count_hours"), BsonRepresentation(BsonType.Int32)]
    public int HoursCount { get; set; }
}