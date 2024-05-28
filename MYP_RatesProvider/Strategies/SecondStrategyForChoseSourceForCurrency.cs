using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace MYP_RatesProvider.Strategies
{
    class SecondStrategyForChoseSourceForCurrency : IStrategyForChoseSourceForCurrency
    {
        public async Task<Dictionary<string, object>> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = Options.urlSecond;
                HttpResponseMessage response = await client.GetAsync(url);

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
        public async Task<Dictionary<string, object>> ConvertDataToDictionary(Dictionary<string, object> data)
        {


            var jsonNew = JsonConvert.SerializeObject(data["data"], Newtonsoft.Json.Formatting.Indented);

            JObject obj = JObject.Parse(jsonNew);
            var a = obj.Values();

            var newDict = new Dictionary<string, object>();
            newDict.Add("timestamp", DateTime.Now);
            var ratesDict = new Dictionary<string, double>();

            foreach (var item in a)
            {
                string currencyCode = (string)"USD" + item["code"];
                double currencyValue = (double)item["value"];
                ratesDict.Add(currencyCode, currencyValue);
            }

            newDict.Add("rates", ratesDict);
            return newDict;
        }
    }
}
