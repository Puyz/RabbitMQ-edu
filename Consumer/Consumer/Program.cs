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
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);


// Read Message From Queue
/*
    Ack(Acknowledgement: onay)
    autoAck: kuyruktan mesaj okunduğunda silinip silinmemesi (otomatik onaylanması)
*/
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);

/* 
    BasicQos: Mesajların işleme hızının ve teslimet sırasını belirleyebiliriz. 
    Böylece Fair Dispatch özelliği konfigüre edilebilmektedir.

    * prefetchSize: Bir consumer tarafından en büyük mesaj boyutunu byte cinsinden belirler. 0, sınırsız demektir.
    * prefetchCount: Bir consumer tarafından aynı anda işleme alınabilecek mesaj sayısını belirler.
    * global: Bu konfigürasyonun tüö consumer'lar için mi yoksa sadece çağrı yapılan consumer için mi geçerli olacağını belirler.
*/
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

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