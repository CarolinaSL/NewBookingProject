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
       
        public static IServiceCollection AddCustomMassTransit(this IServiceCollection services, IConfiguration config, Assembly assembly, string connection = null)
        {
            services.AddMassTransit(configure =>
            {
                configure.AddConsumers(assembly);
                configure.UsingRabbitMq((context, configurator) =>
                {
                    var rabbitMqOptions = services.GetOptions<RabbitMqOptions>("RabbitMq");
                   
                   var host = rabbitMqOptions.HostName;
                    var virtualHost = rabbitMqOptions.VirtualHost;
                    configurator.Host(host, virtualHost, h =>
                    {
                        h.Username(rabbitMqOptions.UserName);
                        h.Password(rabbitMqOptions.Password);
                        
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
