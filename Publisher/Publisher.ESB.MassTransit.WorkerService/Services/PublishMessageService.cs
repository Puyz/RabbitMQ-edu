using MassTransit;
using Shared.ESB.MassTransit.Messages;

namespace Publisher.ESB.MassTransit.WorkerService.Services
{
    public class PublishMessageService : BackgroundService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public PublishMessageService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (true)
            {
                i++;
                ExampleMessage message = new() { Text = $"{i}. Mesaj" };
                
                // Şu anda tüm kuyruklara publish ediyor.
                await _publishEndpoint.Publish(message, stoppingToken);
            }
        }
    }
}
