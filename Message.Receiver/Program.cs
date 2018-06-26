using MassTransit;
using Newtonsoft.Json;
using System;

namespace Message.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(rabbitMqConfig =>
            {
               var host = rabbitMqConfig.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                rabbitMqConfig.ReceiveEndpoint(host, "product_added_queue", e =>
                {
                    e.Consumer<ProductAddedConsumer>();
                });

                rabbitMqConfig.ConfigureJsonSerializer(settings =>
                {
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    return settings;
                });
            });
            busControl.Start();
            Console.ReadLine();
        }
    }
}
