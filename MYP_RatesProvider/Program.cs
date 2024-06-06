
using MYP_RatesProvider;
using MYP_RatesProvider.Core;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "RatesProvider Service";
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IConfiguration>(hostContext.Configuration);
        services.AddHostedService<Worker>();
        services.AddSingleton<RatesManager>();
    })
    .Build();



await host.RunAsync();