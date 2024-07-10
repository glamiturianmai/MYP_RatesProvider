using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MYP_RatesProvider.Core.Configuration;

public static class ConfigureServices
{
    public static void ConfigureRatesService(this IServiceCollection services, IConfiguration configuration) //беется из программа 
    {

        services.AddMassTransit(x =>
        {

            x.AddConsumer<SettingsConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {

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

        services.AddConfigurationServicesFromJson(configuration);
    }
}
