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

#region P2P Tasarımı
// Genellikle Direct exchange kullanılır.
//string queueName = "example-p2p-queue";
//channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

//byte[] message = Encoding.UTF8.GetBytes("Mesaj P2P");
//channel.BasicPublish(
//    exchange: "",
//    routingKey: "example-p2p-queue",
//    body: message
//    );

#endregion

#region Pub/Sub Tasarımı
// Genellikle Fanout exchange kullanılır.

//string exchangeName = "example-pub-sub";
//channel.ExchangeDeclare(
//    exchange: exchangeName, 
//    type: ExchangeType.Fanout,
//    durable: false,
//    autoDelete: false
//    );

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(500);
//    byte[] message = Encoding.UTF8.GetBytes($"Mesaj Pub/Sub {i}");
//    channel.BasicPublish(
//        exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message
//        );
//}

#endregion

#region Work Queue Tasarımı
// Genellikle direct exchange kullanılmaktadır.
//string queueName = "example-work-queue";
//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false
//    );

//// isteğe bağlı prop
//IBasicProperties basicProperties = channel.CreateBasicProperties();
//basicProperties.Persistent = true;

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(500);
//    byte[] message = Encoding.UTF8.GetBytes($"Mesaj Work Queue {i}");
//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: "example-work-queue",
//        body: message
//        );
//}

#endregion

#region Request/Response Tasarımı
string requestQueueName = "example-request-response";
channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

// Consumer'dan dönecek olan sonucu elde edeceğimiz kuyruğun adını tanımlıyoruz.
string responseQueueName = channel.QueueDeclare().QueueName;

// Gönderilen mesajı ifade eden bir korelasyon değeri oluşturuyoruz.
string correlationId = Guid.NewGuid().ToString();


#region Request Mesajını Oluşturma Ve Gönderme

IBasicProperties basicProperties = channel.CreateBasicProperties();

//Bu kolerasyon değerini ilgili mesajla consumer'a gönderiyoruz.
basicProperties.CorrelationId = correlationId;

//Bir yandan da mesajın ReplyTo özelliğine dönüş kuyruğunun adını yazıp consumer'a gönderiyoruz.
basicProperties.ReplyTo = responseQueueName;

for (int i = 0; i < 10; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Mesaj Request-Response {i}");
    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: requestQueueName,
        body: message,
        basicProperties: basicProperties
        );
}

#endregion

#region Response Kuyruğu Dinleme
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue:responseQueueName,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, e) => 
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        //....
        Console.WriteLine($"Response: {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};
#endregion

#endregion

Console.ReadLine();