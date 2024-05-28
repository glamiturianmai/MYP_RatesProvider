using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;


    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var context = new Context();

            //Console.WriteLine("FirstStrategy");
            //context.SetStrategy(new FirstStrategyForChoseSourceForCurrency());
            //context.GetDataCurrency();

            Console.WriteLine();

            Console.WriteLine("SecondStrategy");
            context.SetStrategy(new SecondStrategyForChoseSourceForCurrency());
            context.GetDataCurrency();


            _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);

        }
    }
}