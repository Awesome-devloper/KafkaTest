using Confluent.Kafka;

namespace KafkaTest.Serislizer
{
    public class ProtobufDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        {
            using (var ms = new MemoryStream())
            {
                ms.Write(data);
                return ProtoBuf.Serializer.Deserialize<T>(ms);
            }
        }
    }
}
