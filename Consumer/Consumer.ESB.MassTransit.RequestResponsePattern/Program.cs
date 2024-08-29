using Consumer.ESB.MassTransit.RequestResponsePattern.Consumers;
using MassTransit;

string rabbitMQUri = "amqps";
string queueName = "example-mass-transit-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);

    factory.ReceiveEndpoint(queueName, configure =>
    {
        configure.Consumer<RequestResponseConsumer>();
    });
});

await bus.StartAsync();

Console.Read();