using MassTransit;
using Shared.ESB.MassTransit.RequestResponseMessages;

string rabbitMQUri = "amqps";
string queueName = "example-mass-transit-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitMQUri);
});

await bus.StartAsync();

IRequestClient<RequestMessage> request = bus.CreateRequestClient<RequestMessage>(new Uri($"{rabbitMQUri}/{queueName}"));

int i = 1;

while (true)
{
    await Task.Delay(200);

    // isteği atıyoruz ve bir response bekliyoruz.
    Response<ResponseMessage> response = await request.GetResponse<ResponseMessage>(new()
    {
        MessageNo = i++,
        Text = $"{i}. request"
    });

    Console.WriteLine($"Response received: {response.Message.Text}");
}