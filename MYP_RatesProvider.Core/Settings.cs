using MassTransit;
using Microsoft.Extensions.Configuration;
using MYP_RatesProvider.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MYP_RatesProvider.Core
{
    public class SettingsConsumer(IConfiguration configuration) : IConsumer<ConfigurationMessage>
    {
        public Task Consume(ConsumeContext<ConfigurationMessage> context)
        {
           
            if (context.Message.ServiceType != ServiceType.ratesProvider)
            {
                return Task.CompletedTask;
            }
            var jsonMessage = JsonSerializer.Serialize(context.Message.Configurations);

            configuration.UpdateSettingsFromConfigurationManager(context.Message.Configurations);

            return Task.CompletedTask;
        }
    }
}
