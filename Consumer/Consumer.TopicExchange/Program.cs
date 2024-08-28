using RabbitMQ.Client;
using RabbitMQ.Client.Events;
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

Console.Write("Abone olacağınız topic'in adını yazınız: ");
string topics = Console.ReadLine()!;
string queueName = channel.QueueDeclare().QueueName;


channel.QueueBind
    (
        queue: queueName,
        exchange: "topic-exchange-edu",
        routingKey: topics
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);


consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};



Console.Read();