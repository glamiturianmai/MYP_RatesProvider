using MYP_RatesProvider.Core;

namespace MYP_RatesProvider;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;
    private readonly RatesManager _data;

    public Worker(ILogger<Worker> logger, IConfiguration configuration, RatesManager data)
    {
        _logger = logger;
        _configuration = configuration;
        _data = data;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("aaaaaaaaaaaaaa");

            await _data.DataService();
            _logger.LogInformation("Service running at: {time}", DateTimeOffset.Now);
            await Task.Delay(10000, stoppingToken);

        }
    }
}