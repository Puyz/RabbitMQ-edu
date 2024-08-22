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
channel.ExchangeDeclare(exchange: "fanout-exchange-edu",type: ExchangeType.Fanout, durable: false, autoDelete: false);


// fanout exchange'de bind olmuş tüm kuyruklara göndereceğimiz için routing key kullanmıyoruz.
while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine()!;
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "fanout-exchange-edu", routingKey: string.Empty, body: byteMessage);
}
