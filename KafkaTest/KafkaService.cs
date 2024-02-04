using KafkaTest.Consumers;
using KafkaTest.Proto;
using MassTransit;

namespace KafkaTest
{
    public class KafkaService( IServiceScopeFactory serviceScope) 
    {
        public async Task SendMessage(KafkaMessage data)
        {
            using var scope = serviceScope.CreateScope();

            var producer = scope.ServiceProvider.GetRequiredService<ITopicProducerProvider>();

            var messageProducer =
                producer.GetProducer<KafkaMessage>(new Uri("topic:algo"));

            await messageProducer.Produce(data);
           
        }
    }
}
