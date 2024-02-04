using MassTransit;

namespace KafkaTest.Consumers
{
    public class ParentConsumer : IConsumer<Parent>
    {
        public Task Consume(ConsumeContext<Parent> context)
        {
            return Task.CompletedTask;
        }
    }

    public class ParentConsumerDefinition : ConsumerDefinition<ParentConsumer>
    {
        public ParentConsumerDefinition()
        {
            EndpointName = "algo-order-stat";
            ConcurrentMessageLimit = 1;
        }
    }
}
