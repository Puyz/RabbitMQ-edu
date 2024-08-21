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


// Create Queue 
/*
    publisher'daki ile birebir aynı yapılandırmada tanımlanmalıdır.
*/
channel.QueueDeclare(queue: "example-queue", exclusive: false);


// Read Message From Queue
/*
    Ack(Acknowledgement: onay)
    autoAck: kuyruktan mesaj okunduğunda silinip silinmemesi (otomatik onaylanması)
*/
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);

consumer.Received += (sender, e) =>
{
    // kuyruğa gelen mesajın işlendiği yerdir.
    /* 
        args.Body: kuyruktaki mesajın verisini bütünsel olarak getirir.
        args.Body.Span veya args.Body.ToArray(): kuyruktaki mesajın byte verisini getirir.
    */
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    /* 
        multiple: birden fazla mesaja dair onay bildirisi gönderir.
        Eğer true verilirse DeliveryTag değerine sahip olan bu mesajla birlikte bundan önceki 
        mesajlarında işlendiğini onaylar. Aksi taktirde false verilirse sadece bu mesaj için
        onay bildirisinde bulunacaktır.
    */

    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);


    // Eğer işlem sırasında hata meydana gelirse ve bu işlemin onaylanmadığını kuyruğa bildirmemiz gerekir.
    /* 
        requeue: Bu parametre, bu consumer tarafından işlenemeyeceği ifade edilen bu mesajın
        tekrardan kuyruğa eklenip eklenmemesinin kararını vermektedir. 
        * True değeri verilirse mesaj kuyruğa tekrardan işlenmek üzere eklenir. 
        * False değeri verilirse mesaj kuyruğa eklenmeyerek silinecektir.
     */

    //channel.BasicNack(deliveryTag: e.DeliveryTag, multiple: false, requeue: true);
};

Console.Read();