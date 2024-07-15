using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var context = new DataCurrency();

            Console.WriteLine("FirstStrategy");
            context.SetStrategy(new PrimaryCurrencyProvider(_configuration));
            context.GetDataCurrency();


            Console.WriteLine();

            //Console.WriteLine("SecondStrategy");
            //context.SetStrategy(new SecondaryCurrencyProvider(_configuration));
            //context.GetDataCurrency();


            _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);

        }
    }
}