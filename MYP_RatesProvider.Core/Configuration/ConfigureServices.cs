using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace MYP_RatesProvider.Core.Configuration;

public static class ConfigureServices
{
    public static void ConfigureRatesService(this IServiceCollection services, ConfigurationManager configurationManager) //беется из программа 
    {

        services.AddMassTransit(x =>
        {
           
            x.AddConsumer<SettingsConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.ReceiveEndpoint("currency_rates", e =>
                {
                    e.ConfigureConsumer<RatesInfoConsumer>(context);
                });
                cfg.ReceiveEndpoint("settings_queue", e =>
                {
                    e.Bind("configurations-exchange", x =>
                    {
                        x.ExchangeType = "fanout";
                    });
                    e.ConfigureConsumer<SettingsConsumer>(context);
                });
            });
        });

        services.AddConfigurationServicesFromJson(configurationManager);
    }
}
