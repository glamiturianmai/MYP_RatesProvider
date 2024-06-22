
using MassTransit;
using Microsoft.Extensions.Options;
using MYP_RatesProvider;
using MYP_RatesProvider.Core;
using MYP_RatesProvider.Core.Services;
using MYP_RatesProvider.RabbitMq;
using MYP_RatesProvider.Strategies;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "RatesProvider Service";
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IConfiguration>(hostContext.Configuration); //service.Configure
        services.AddHostedService<Worker>();
        services.AddSingleton<RatesManager>();
        services.AddSingleton<HttpService>();
        services.AddSingleton<DataProvider>();
        services.AddScoped<IRabbitMqService, RabbitMqService>();
        services.Configure<List<CurrencyProviderSettings>>(hostContext.Configuration.GetSection("CurrencyProviderSettings"));

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq();
        });
    })
    .Build();




await host.RunAsync();