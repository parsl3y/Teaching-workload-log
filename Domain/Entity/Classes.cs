using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity;

public class Classes
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    
    [BsonElement("class_name"), BsonRepresentation(BsonType.String)]
    public string? ClassName { get; set; }
    
    [BsonElement("class_room"), BsonRepresentation(BsonType.String)]
    public string? ClassRoom { get; set; }
    
    [BsonElement("total_class_number"), BsonRepresentation(BsonType.Int32)]
    public int TotalClassNumber { get; set; }
    
    [BsonElement("class_number_today"), BsonRepresentation(BsonType.Int32)]
    public int ClassNumberToday { get; set; }
    
    [BsonElement("class_date"),BsonRepresentation(BsonType.DateTime)]
    public DateTime ClassDate { get; set; }
}