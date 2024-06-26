using Messaging.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MYP_RatesProvider.Core.Services;
using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

public class DataProvider
{
    private ICurrencyStrategy _strategy;
    ILogger<PrimaryCurrencyProvider> logger1;
    ILogger<SecondaryCurrencyProvider> logger2;

    private List<ICurrencyStrategy> _availableProviders = [];



    public DataProvider(HttpService httpService, IOptions<List<CurrencyProviderSettings>> settings, ILogger<PrimaryCurrencyProvider> logger1, ILogger<SecondaryCurrencyProvider> logger2)
    {

        _availableProviders.Add(new PrimaryCurrencyProvider(httpService, settings.Value, logger1));
        _availableProviders.Add(new SecondaryCurrencyProvider(httpService, settings.Value, logger2));
        _strategy = _availableProviders[0];

    }

    public void SetNextStrategy()
    {
        _strategy = _availableProviders.Find(i => i.GetId() != _strategy.GetId());
    }

    public async Task<RatesInfo> GetDataCurrency()
    {
        return await this._strategy.GetData();
    }


}
