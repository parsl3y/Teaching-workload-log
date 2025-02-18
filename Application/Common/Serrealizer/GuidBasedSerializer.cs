using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Application.Common.Serializer
{
    public class GuidBasedSerializer<T> : SerializerBase<T>
    {
        public override T Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var value = context.Reader.ReadString();
            return (T)Activator.CreateInstance(typeof(T), Guid.Parse(value));
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, T value)
        {
            context.Writer.WriteString(value.ToString());
        }
    }
}
