using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


// Create Connection
ConnectionFactory factory = new()
{
    Uri = new("amqps")
};

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


Console.ReadLine();