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


// Create Queue 
/*
    exclusive: eğer true olursa başka bağlantı ile bu kuyruğa (başka channel'dan) ulaşamayız. Consumer ile tüketeceğimiz için false olması gerekiyor.
    durable: kuyruğun kalıcılık durumu
 */
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);


// Send Message to Queue
/*
    RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir. 
    
    BasicPublish: 
    * exchange belirtmezsek default olarak direct seçili olur.
    * direct exchange'de routing key, kuyruk ismine denk geliyor.
*/

// Mesajları kalıcı olarak ayarladık.
IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 5; i++)
{
    await Task.Delay(2000);
    byte[] message = Encoding.UTF8.GetBytes("test message " + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);
}


Console.Read();