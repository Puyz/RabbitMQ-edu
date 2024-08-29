using MassTransit;
using Shared.ESB.MassTransit.Messages;

string rabbitMQUri = "amqps";

string queueName = "example-mass-transit-queue";


IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"{rabbitMQUri}/{queueName}"));


Console.Write("Gönderilecek mesaj: ");
string message = Console.ReadLine()!;
await sendEndpoint.Send<IMessage>(new ExampleMessage() { Text = message });

Console.Read();