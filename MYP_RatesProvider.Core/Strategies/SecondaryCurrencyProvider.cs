using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MYP_RatesProvider.Strategies
{
    public class SecondaryCurrencyProvider : ICurrencyStrategy
    {
        private readonly IConfiguration _configuration;
        public SecondaryCurrencyProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<Dictionary<string, object>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                string urlFromAppSettings = _configuration.GetSection("UrlArray").GetSection("1")["site"] + _configuration.GetSection("UrlArray").GetSection("1")["config"] + _configuration.GetSection("UrlArray").GetSection("1")["key"];
                HttpResponseMessage response = await client.GetAsync(urlFromAppSettings);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                }
                else
                {
                    return new Dictionary<string, object> { }; //или ошибку отдавать? 
                }
            }

        }
        public Dictionary<string, object> ConvertDataToDictionary(Dictionary<string, object> data)
        {
            var jsonCurrency = JsonConvert.SerializeObject(data["data"], Newtonsoft.Json.Formatting.Indented);

            JObject objectCurrency = JObject.Parse(jsonCurrency);

            var currencyInf = new Dictionary<string, object>();
            currencyInf.Add("timestamp", DateTime.Now);
            var currencyRates = new Dictionary<string, double>();

            foreach (var item in objectCurrency.Values())
            {
                string currencyCode = (string)"USD" + item["code"];
                double currencyValue = (double)item["value"];
                currencyRates.Add(currencyCode, currencyValue);
            }

            currencyInf.Add("rates", currencyRates);
            var a = currencyInf;
            return currencyInf;
        }
    }
}
