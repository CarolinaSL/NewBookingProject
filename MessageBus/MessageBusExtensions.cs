using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewBookingApp.Core.Options;
using NewBookingApp.Core.WebExtensions;
using System.Reflection;

namespace MessageBus
{
    public static class MessageBusExtensions
    {
        public static IServiceCollection AddMessageBusEasyNet(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }

        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration config, Assembly assembly, string connection = null)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(assembly);
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");
                    //  var connection = config.GetSection("MessageQueueConnection")?["MessageBus"];
                    var host = "jackal.rmq.cloudamqp.com";
                    configurator.Host(host, "rcxeicbn", h =>
                    {
                        h.Username("rcxeicbn");
                        h.Password("4dagw5KMJa67cktjr4QvVcRO627VevGP");
                        
                    });
                    var consumers = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                            .Where(x => x.IsAssignableTo(typeof(IConsumer<>))).ToList();

                    if (consumers.Any())
                    {
                        configurator.ReceiveEndpoint($"{rabbitMqOptions.ExchangeName}", e =>
                        {
                            foreach(var consumer in consumers)
                            {
                                configurator.ConfigureEndpoints(context);
                            }
                        });
                    }
                    
                   
                    
                });
                
            });


            return services;
        }
    }
}
