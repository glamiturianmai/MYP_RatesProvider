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

            var a = data["rates"];
            //Dictionary<string, double> convertedDictionary = new Dictionary<string, double>();

            //foreach (var kvp in a)
            //{
            //    if (kvp.Value is double)
            //    {
            //        convertedDictionary.Add(kvp.Key, (double)kvp.Value);
            //    }
            //}

            //var jsonNew = JsonConvert.SerializeObject(a, Newtonsoft.Json.Formatting.Indented);

            //JObject obj = JObject.Parse(jsonNew);
            var newDict = new Dictionary<string, object>();
            newDict.Add("timestamp", DateTime.Now);
            var ratesDict = new Dictionary<string, double>();


            //foreach (var item in a) 
            //{

            //    string currencyCode = (string)"USD" + item.Key;
            //    double currencyValue = (double)item.Value;
            //    ratesDict.Add(currencyCode, currencyValue);
            //}

            newDict.Add("rates", a);
            var b = newDict;
            return newDict;


            var jsonResult = JsonConvert.SerializeObject(newDict, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(jsonResult);

        }
    }
}
