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

channel.ExchangeDeclare(exchange: "header-exchange-edu", type: ExchangeType.Headers);

Console.Write("Header value değerini belirleniyiz: ");
string headerValues = Console.ReadLine()!;

IBasicProperties basicProperties = channel.CreateBasicProperties();

// ["no"]: özel belirteç değildir. publisher ve consumer eşleştirmek için rastgele isimlendirme yapıyoruz.
basicProperties.Headers = new Dictionary<string, object>
{
    ["no"] = headerValues
};


while (true)
{
    Console.Write("Mesaj yazınız: ");
    string message = Console.ReadLine()!;
    channel.BasicPublish
        (
            exchange: "header-exchange-edu",
            routingKey: string.Empty,
            body: Encoding.UTF8.GetBytes(message),
            basicProperties: basicProperties
        );
}