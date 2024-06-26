using MassTransit;
using Messaging.Shared;
using Microsoft.Extensions.Logging;
using Serilog;



namespace MYP_RatesProvider.Core;

public class RatesManager
{
    private readonly Serilog.ILogger _logger = Log.ForContext<RatesManager>();
    private readonly DataProvider _dataProvider;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly MyService _myService;
    private RatesInfo _dictData;


    public RatesManager( DataProvider dataProvider, MyService myService/*, IPublishEndpoint publishEndpoint*/)
    {
        _myService = myService;
        _dataProvider = dataProvider;
        //_publishEndpoint = publishEndpoint;
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
                _logger.Information("Successfully received the data");
                //await _publishEndpoint.Publish<RatesInfo>(_dictData);
                _logger.Information("Sent the data to RabbitMQ");
                _myService.WriteLog("Sent the data to RabbitMQ");
                Task.Delay(20000);
            }
            catch (Exception ex)
            {
                _dataProvider.SetNextStrategy();
                _logger.Error("There is a problem with the data source, it has been changed");
                _logger.Error(ex.Message);
            }

            if (numberAttempts > 4)
            {
                //отпавить письмо админу
                _logger.Fatal("There are too many attempts to get data, there is a problem with currency sources");
                break;
            }


        }

    }
}
