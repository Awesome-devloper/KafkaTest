using Confluent.Kafka;
using KafkaTest;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<KafkaService>();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory();

    //x.UsingInMemory(broker =>
    //{
    //    broker.ConfigureJsonSerializerOptions(options =>
    //    {
    //        // customize the JsonSerializerOptions here
    //        return options;
    //    });
    //})

    x.AddRider(rider =>
    {


        rider.AddProducer<Null, KafkaMessage>("algo", (context, cfg) =>
        {
            cfg.SetValueSerializer(new ProtobufSerializer<KafkaMessage>());
        });

        rider.AddConsumer<KafkaMessageConsumer>();

        rider.UsingKafka((context, cfg) =>
        {
            //MassTransit.KafkaIntegration.Serializers.IKafkaSerializerFactory

            cfg.Host("localhost:9092");

            cfg.TopicEndpoint<KafkaMessage>("algo", "algo-group-name", e =>
            {
                e.SetValueDeserializer(new ProtobufDeserializer<KafkaMessage>());
                e.ConfigureConsumer<KafkaMessageConsumer>(context);
                e.CreateIfMissing();

            });
        });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
