using Domain.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Application.Common.Serializer;

public class UserIdSerializer : SerializerBase<UserId>
{
    public override UserId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var value = context.Reader.ReadString();
        return new UserId(Guid.Parse(value));  
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, UserId value)
    {
        context.Writer.WriteString(value.ToString()); 
    }
}