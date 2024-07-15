using Messaging.Shared;
using MYP_RatesProvider.Core.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYP_RatesProvider.Strategies
{
    public class SecondaryCurrencyProvider : ICurrencyStrategy
    {
        private readonly HttpService _httpService;
        private readonly string _urlFromAppSettings;

        public SecondaryCurrencyProvider(HttpService httpService, List<CurrencyProviderSettings> сurrencySources)
        {
            _httpService = httpService;
            var setting = сurrencySources.Find(x => x.Id == GetId());
            _urlFromAppSettings = setting.Site + setting.Url + setting.Key;
        }


        public async Task<RatesInfo> GetData()
        {
            var dataFromSource = await _httpService.GetDataFromSource(_urlFromAppSettings);
            return ConvertDataToDictionary(dataFromSource);
        }

        public RatesInfo ConvertDataToDictionary(Dictionary<string, object> data)
        {
            var jsonCurrency = JsonConvert.SerializeObject(data["data"], Formatting.Indented);

            JObject objectCurrency = JObject.Parse(jsonCurrency);

            var currencyInf = new RatesInfo();
            currencyInf.Date = DateTime.Now;
            var currencyRates = new Dictionary<string, decimal>();

            foreach (var item in objectCurrency.Values())
            {
                string currencyCode = (string)"USD" + item["code"];
                decimal currencyValue = (decimal)item["value"];
                currencyRates.Add(currencyCode, currencyValue);
            }
            currencyInf.Rates = currencyRates;
            return currencyInf;
        }

        public int GetId() => 2;
    }
}
