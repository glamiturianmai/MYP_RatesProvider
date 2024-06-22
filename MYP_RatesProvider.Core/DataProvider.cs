﻿using Messaging.Shared;
using Microsoft.Extensions.Options;
using MYP_RatesProvider.Core.Services;
using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

public class DataProvider
{
    private ICurrencyStrategy _strategy;

    private List<ICurrencyStrategy> _availableProviders = new List<ICurrencyStrategy>();



    public DataProvider(HttpService httpService, IOptions<List<CurrencyProviderSettings>> settings)
    {

        _availableProviders.Add(new PrimaryCurrencyProvider(httpService, settings.Value));
        _availableProviders.Add(new SecondaryCurrencyProvider(httpService, settings.Value));
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