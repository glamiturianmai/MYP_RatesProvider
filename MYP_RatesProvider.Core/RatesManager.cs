using MassTransit;
using Messaging.Shared;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace MYP_RatesProvider.Core
{
    public class RatesManager
    {
        private readonly ILogger<RatesManager> _logger;
        private readonly DataProvider _dataProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        private RatesInfo _dictData;


        public RatesManager(ILogger<RatesManager> logger, DataProvider dataProvider, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _dataProvider = dataProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task GetData()
        {
            var numberAttempts = 0;
            while (_dictData == null)
            {
                try
                {
                    numberAttempts++;
                    _dictData = await _dataProvider.GetDataCurrency(); 
                    _logger.LogInformation("Successfully received the data");
                    await _publishEndpoint.Publish<RatesInfo>(_dictData);
                    _logger.LogInformation("Sent the data to RabbitMQ");
                    Task.Delay(20000);
                }
                catch (Exception ex1)
                {
                    _dataProvider.SetNextStrategy();
                    _logger.LogInformation("There is a problem with the data source, it has been changed");
                }

                if (numberAttempts > 4)
                {
                    _logger.LogInformation("There are too many attempts to get data, there is a problem with currency sources");
                    break;
                }


            }

        }
    }
}
