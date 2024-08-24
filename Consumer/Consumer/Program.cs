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

channel.ExchangeDeclare(exchange: "header-exchange-edu", type: ExchangeType.Headers);

Console.Write("Header value değerini belirleniyiz: ");
string headerValues = Console.ReadLine()!;

string queueName = channel.QueueDeclare().QueueName;

// x-match default olarak any olarak belirlenir.
var headers = new Dictionary<string, object>()
{
    //["x-match"] = "all",
    ["no"] = headerValues,
};

channel.QueueBind(
    queue: queueName,
    exchange: "header-exchange-edu",
    routingKey: string.Empty,
    arguments: headers
    );

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();
