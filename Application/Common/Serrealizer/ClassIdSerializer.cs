using Domain.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Application.Common.Serrealizer;

public class ClassIdSerializer : SerializerBase<ClassId>
{
    public override ClassId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new ClassId(Guid.Parse(value));  
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, ClassId value)
    {
        context.Writer.WriteString(value.ToString()); 
    }
}