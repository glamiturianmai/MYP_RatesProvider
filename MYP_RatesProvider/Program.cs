
using MYP_RatesProvider;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "RatesProvider Service";
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IConfiguration>(hostContext.Configuration);
        services.AddHostedService<Worker>();
    })
    .Build();



await host.RunAsync();