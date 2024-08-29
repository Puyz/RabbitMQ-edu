using MassTransit;
using Shared.ESB.MassTransit.Messages;

namespace Consumer.ESB.MassTransit.WorkerService.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Gelen mesaj: {context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
