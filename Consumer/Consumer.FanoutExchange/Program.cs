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

channel.ExchangeDeclare(exchange: "fanout-exchange-edu", type: ExchangeType.Fanout, durable: false, autoDelete: false);

Console.Write("Kuyruk adı giriniz: ");
string queueName = Console.ReadLine()!;

channel.QueueDeclare(queue: queueName, exclusive: false);

channel.QueueBind(queue: queueName, exchange: "fanout-exchange-edu", routingKey: string.Empty);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();