using MassTransit;
using Shared.ESB.MassTransit.RequestResponseMessages;

namespace Consumer.ESB.MassTransit.RequestResponsePattern.Consumers
{
    public class RequestResponseConsumer : IConsumer<RequestMessage>
    {
        public async Task Consume(ConsumeContext<RequestMessage> context)
        {
            //await Console.Out.WriteLineAsync(context.Message.Text);
            Console.WriteLine(context.Message.Text);

            await context.RespondAsync<ResponseMessage>(new() 
            { 
                Text = $"{context.Message.MessageNo}. response to request"
            });
        }
    }
}
