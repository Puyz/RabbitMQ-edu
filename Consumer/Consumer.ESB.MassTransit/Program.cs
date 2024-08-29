using Consumer.ESB.MassTransit.Consumers;
using MassTransit;

string rabbitMQUri = "amqps";
string queueName = "example-mass-transit-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(queueName, endpoint =>
    {
        endpoint.Consumer<ExampleMessageConsumer>();
    });
});

await bus.StartAsync();

Console.Read();