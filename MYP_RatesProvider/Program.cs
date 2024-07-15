using MassTransit;
using MYP_RatesProvider;
using MYP_RatesProvider.Core;
using MYP_RatesProvider.Core.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "RatesProvider Service";
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<RatesManager>();
        services.AddSingleton<HttpService>();
        services.AddSingleton<DataProvider>();
        services.Configure<List<CurrencyProviderSettings>>(hostContext.Configuration.GetSection("CurrencyProviderSettings"));

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq();
        });
    })
    .Build();

await host.RunAsync();