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

//string queueName = "example-p2p-queue";
//channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: queueName, autoAck: true, consumer);
//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region Pub/Sub Tasarımı
//string exchangeName = "example-pub-sub";
//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout,
//    durable: false,
//    autoDelete: false
//    );

//string queueName = channel.QueueDeclare().QueueName;
//channel.QueueBind(
//    queue: queueName,
//    exchange: exchangeName,
//    routingKey: string.Empty
//    );

//// isteğe bağlı ölçeklendirme ayarı
////channel.BasicQos(
////    prefetchCount: 1,
////    prefetchSize: 0,
////    global: false
////    );

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: queueName, autoAck: true, consumer);
//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region Work Queue Tasarımı

//string queueName = "example-work-queue";
//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false
//    );

//EventingBasicConsumer consumer = new(channel);

//channel.BasicConsume(
//    queue: queueName, 
//    autoAck: true, // ya otomatik ya da manuel işlenmesi gerekiyor.
//    consumer
//    );

//channel.BasicQos(
//    prefetchCount: 1,
//    prefetchSize: 0, // sınırsız
//    global: false
//    );


//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region Request/Response Tasarımı
//string requestQueueName = "example-request-response";
//channel.QueueDeclare(queue: requestQueueName, durable: false, exclusive: false, autoDelete: false);

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: requestQueueName,
//    autoAck: true,
//    consumer: consumer
//    );

//consumer.Received += (sender, e) =>
//{
//    string receivedMessage = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(receivedMessage);
//    //....

//    byte[] responseMessage = Encoding.UTF8.GetBytes($"İşlem tamamlandı.: {receivedMessage}");
//    IBasicProperties properties = channel.CreateBasicProperties();
//    properties.CorrelationId = e.BasicProperties.CorrelationId;

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: e.BasicProperties.ReplyTo,
//        body: responseMessage,
//        basicProperties: properties
//        );

//};

#endregion

Console.ReadLine();