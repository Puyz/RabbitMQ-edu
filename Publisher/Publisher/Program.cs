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


// Create Exchange
channel.ExchangeDeclare(exchange: "direct-exchange-edu",type: ExchangeType.Direct, durable: false, autoDelete: false);


// direct exchange olduğu için mesajın hangi kuyruğa gideceğini routuingKey ile bildiriyoruz.
while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine()!;
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "direct-exchange-edu", routingKey: "direct-queue-edu", body: byteMessage);
}
