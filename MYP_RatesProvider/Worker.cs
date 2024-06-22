using MYP_RatesProvider.Core;

namespace MYP_RatesProvider;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly RatesManager _manager;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, RatesManager manager)
    {
        _logger = logger;
        _configuration = configuration;
        _manager = manager; //интерфейс
       
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {


            await _manager.GetData();
            _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
            await Task.Delay(3600000, stoppingToken); 

        }
    }
}