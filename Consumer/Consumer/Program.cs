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

// 1.Adım
channel.ExchangeDeclare(exchange: "direct-exchange-edu", type: ExchangeType.Direct, durable: false, autoDelete: false);

// 2.Adım
string queueName = channel.QueueDeclare().QueueName;

// 3.Adım
channel.QueueBind(queue: queueName, exchange: "direct-exchange-edu", routingKey: "direct-queue-edu");



EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();


/*
    1.adım: Publisher'da ki exchange ile birebir ayın isim ve type' a sahi pbir exchange tanımlanmalıdır.

    2.adım: Publisher tarafından routing key'de bulunan değerdeki kuyruğa gönderilen mesajları kendi oluşturduğumuz
    kuyruğa yönlendirerek tüketmemiz gerekmektedir. Bunun için öncelikle bir kuyruk oluşturulmalıdır.

    3.adım: Publisher tarafından gönderilen mesajı consume edebilmek için oluşturduğumuz bu rastgele kuyruğu
    routing key ile bağlamamız gerekiyor.
 
 
 
*/