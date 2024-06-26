using Messaging.Shared;
using Microsoft.Extensions.Logging;
using MYP_RatesProvider.Core;
using MYP_RatesProvider.Core.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYP_RatesProvider.Strategies
{

    public class PrimaryCurrencyProvider : ICurrencyStrategy
    {
        private readonly HttpService _httpService;
        private readonly string _urlFromAppSettings;
        private readonly ILogger<PrimaryCurrencyProvider> _logger;

        public PrimaryCurrencyProvider(HttpService httpService, List<CurrencyProviderSettings> сurrencySources, ILogger<PrimaryCurrencyProvider> logger)
        {
            _logger = logger;
            _httpService = httpService;
            var setting = сurrencySources.Find(x => x.Id == GetId());
            _urlFromAppSettings = setting.Site + setting.Url + setting.Key;
        }

        public async Task<RatesInfo> GetData()
        {
            var dataFromSource = await _httpService.GetDataFromSource(_urlFromAppSettings);
            _logger.LogInformation("Get informations from site (Primary strategy)");
            return ConvertDataToDictionary(dataFromSource);
        }

        public RatesInfo ConvertDataToDictionary(Dictionary<string, object> data)
        {
            var jsonCurrency = JsonConvert.SerializeObject(data["rates"], Formatting.Indented);

            JObject objectCurrency = JObject.Parse(jsonCurrency);
            var currencyInf = new RatesInfo();
            currencyInf.Date = DateTime.Now;
            var currencyRates = new Dictionary<string, decimal>();

            foreach (var item in objectCurrency)
            {
                currencyRates.Add((string)"USD" + item.Key, (decimal)item.Value);
            }
            currencyInf.Rates = currencyRates;

            _logger.LogInformation("Convert informations from site (Primary strategy)");
            return currencyInf;
            
        }

        public int GetId() => 1;
    }
}
