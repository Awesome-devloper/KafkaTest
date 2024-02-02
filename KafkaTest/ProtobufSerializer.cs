using Confluent.Kafka;
using System.Text;

namespace KafkaTest
{
    public class ProtobufSerializer<TMessageModel> : ISerializer<TMessageModel> where TMessageModel : class
    {
        public byte[] Serialize(TMessageModel data, SerializationContext context)
        {
            using (var ms = new MemoryStream())
            {
               
                ProtoBuf.Serializer.Serialize<TMessageModel>(ms,data);

                return ms.ToArray();
            }
        }
    }
}
