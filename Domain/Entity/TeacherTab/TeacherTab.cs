using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entity.TeacherTab;

public class TeacherTab
{
    [BsonId]
    public TeacherTabId? Id {get; set; }
    
    [BsonElement("class_id")]
    public ClassId ClassId {get; set; }

    [BsonElement("class_theme"), BsonRepresentation(BsonType.String)]
    public string ClassTheme { get; set; }
    
    [BsonElement("count_hours"), BsonRepresentation(BsonType.Int32)]
    public int HoursCount { get; set; }

    private TeacherTab(TeacherTabId id, ClassId classId, string classTheme, int hoursCount)
    {
        Id = id;
        ClassId = classId;
        ClassTheme = classTheme;
        HoursCount = hoursCount;
    }
    
    public static TeacherTab New(TeacherTabId id, ClassId classId, string classTheme, int hoursCount)
    => new (id, classId ,classTheme, hoursCount);
}