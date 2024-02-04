using MassTransit;
using ProtoBuf;

namespace KafkaTest.Consumers
{
    class KafkaMessageConsumer : IConsumer<KafkaMessage>
    {
        public Task Consume(ConsumeContext<KafkaMessage> context)
        {
            var data = context.Message;
            return Task.CompletedTask;
        }
    }
    [ProtoContract]
    public record KafkaMessage
    {
        [ProtoMember(1)]
        public string Text { get; init; }
        [ProtoMember(2)]
        public DateTime MyProperty { get; set; }
    }
}
