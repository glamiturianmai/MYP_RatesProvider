using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYP_RatesProvider.Strategies
{
    public class PrimaryCurrencyProvider : ICurrencyStrategy
    {
        private readonly IConfiguration _configuration;
        public PrimaryCurrencyProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Dictionary<string, object>> GetData()
        {
            using var client = new HttpClient();

            string urlFromAppSettings = _configuration["CurrencySources:UrlFirst:name"] + _configuration["CurrencySources:UrlFirst:app_id"];
            HttpResponseMessage response = await client.GetAsync(urlFromAppSettings);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            }
            else
            {
                return new Dictionary<string, object> { }; 
            }

        }

        public Dictionary<string, object> ConvertDataToDictionary(Dictionary<string, object> data)
        {
            var jsonCurrency = JsonConvert.SerializeObject(data["rates"], Newtonsoft.Json.Formatting.Indented);

            JObject objectCurrency = JObject.Parse(jsonCurrency);
            var currencyInf = new Dictionary<string, object>();
            currencyInf.Add("timestamp", DateTime.Now);
            var currencyRates = new Dictionary<string, decimal>();

            foreach (var item in objectCurrency)
            {
                currencyRates.Add((string)"USD" + item.Key, (decimal)item.Value);
            }

            currencyInf.Add("rates", currencyRates);
            return currencyInf;
        }
    }
}
