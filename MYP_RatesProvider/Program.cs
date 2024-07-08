using MassTransit;
using MYP_RatesProvider;
using MYP_RatesProvider.Core;
using MYP_RatesProvider.Core.Services;
using MYP_RatesProvider.Core.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.Extensions.Configuration;

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
        services.AddSingleton<MyService>();
        //services.Configure<List<MYP_RatesProvider.CurrencyProviderSettings>>(hostContext.Configuration.GetSection("CurrencyProviderSettings"));




        //services.ConfigureRatesService(hostContext.Configuration.Get<Microsoft.Extensions.Configuration.ConfigurationManager>());

        services.ConfigureRatesService(hostContext.Configuration);

        hostContext.Configuration.ReadSettingsFromConfigurationManager();

    })
    .ConfigureAppConfiguration((hostContext, config) =>
    {

        config.AddJsonFile("appsettings.DefaultConfiguration.json", optional: false, reloadOnChange: true);

    })

    .Build();

await host.RunAsync();