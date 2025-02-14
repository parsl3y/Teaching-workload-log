using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity;

public class Class
{
    [BsonId]
    public ClassId Id { get; set; } 
    
    [BsonElement("class_name"), BsonRepresentation(BsonType.String)]
    public string ClassName { get; set; }
    
    [BsonElement("total_class_number"), BsonRepresentation(BsonType.Int32)]
    public int TotalClassNumber { get; set; }
    
    [BsonElement("class_number_today"), BsonRepresentation(BsonType.Int32)]
    public int ClassNumberToday { get; set; }
    
    [BsonElement("teacher_id"), BsonRepresentation(BsonType.ObjectId)]
    public UserId? TeacherId { get; set; }
    
    [BsonElement("class_date")]
    public DateTime ClassDate { get; set; }

    private Class(ClassId classId, string className, int totalClassNumber, int classNumberToday, UserId? teacherId,
        DateTime classDate)
    {
        Id = classId;
        ClassName = className;
        TotalClassNumber = totalClassNumber;
        ClassNumberToday = classNumberToday;
        TeacherId = teacherId;
        ClassDate = classDate;
    }
    
    public static Class New(ClassId id, string className, int totalClassNumber, int classNumberToday, UserId? teacherId, DateTime classDate)
        => new(id, className, totalClassNumber, classNumberToday, teacherId, classDate);

    public void UpdateDetail(UserId? teacherId, DateTime classDate)
    {
        TeacherId = teacherId;
        ClassDate = classDate;
     }
}