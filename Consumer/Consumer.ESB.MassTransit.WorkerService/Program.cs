using Consumer.ESB.MassTransit.WorkerService.Consumers;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<ExampleMessageConsumer>();
            configurator.UsingRabbitMq((context, _configurator) =>
            {
                _configurator.Host("amqps");

                // consumer bir kuyruðu dinlemek zorunda
                _configurator.ReceiveEndpoint("example-mass-transit-queue", e =>
                {
                    e.ConfigureConsumer<ExampleMessageConsumer>(context);

                });
            });
        });
    })
    .Build();

await host.RunAsync();
