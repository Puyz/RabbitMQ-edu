using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


// Create Connection
ConnectionFactory factory = new()
{
    Uri = new("")
};


// Connection Activate and Open Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


// Create Queue 
/*
    publisher'daki ile birebir aynı yapılandırmada tanımlanmalıdır.
*/
channel.QueueDeclare(queue: "example-queue", exclusive: false);


// Read Message From Queue
/*
    autoAck: kuyruktan mesaj okunduğunda silinip silinmemesi
*/
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", false, consumer);

consumer.Received += (sender, args) =>
{
    // kuyruğa gelen mesajın işlendiği yerdir.
    // args.Body: kuyruktaki mesajın verisini bütünsel olarak getirir.
    // args.Body.Span veya args.Body.ToArray(): kuyruktaki mesajın byte verisini getirir.
    Console.WriteLine(Encoding.UTF8.GetString(args.Body.Span));
};

Console.Read();