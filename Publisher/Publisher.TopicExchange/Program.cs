using RabbitMQ.Client;
using System.Text;


// Create Connection
ConnectionFactory factory = new()
{
    Uri = new("amqps")
};


// Connection Activate and Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-edu", type: ExchangeType.Topic);

Console.Write("Topic belirleniyiz: ");
string topics = Console.ReadLine()!;

while (true)
{
    Console.Write("Mesaj yazınız: ");
    string message = Console.ReadLine()!;
    channel.BasicPublish
        (
            exchange: "topic-exchange-edu",
            routingKey: topics,
            body: Encoding.UTF8.GetBytes(message)
        );
}