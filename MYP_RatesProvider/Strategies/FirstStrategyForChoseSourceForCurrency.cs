using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYP_RatesProvider.Strategies
{
    class FirstStrategyForChoseSourceForCurrency : IStrategyForChoseSourceForCurrency
    {
        public async Task<Dictionary<string, object>> GetData()
        {
            string url = Options.UrlFirst;
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(url);


                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                }
                else
                {
                    return new Dictionary<string, object> { }; //bk
                }
            }

        }

        public async Task<Dictionary<string, object>> ConvertDataToDictionary(Dictionary<string, object> data)
        {
            var jsonNew = JsonConvert.SerializeObject(data["rates"], Newtonsoft.Json.Formatting.Indented);

            JObject obj = JObject.Parse(jsonNew);
            var newDict = new Dictionary<string, object>();
            newDict.Add("timestamp", DateTime.Now);
            var ratesDict = new Dictionary<string, double>();

            foreach (var item in obj)
            {
                ratesDict.Add((string)"USD" + item.Key, (double)item.Value);
            }

            newDict.Add("rates", ratesDict);
            return newDict;

            //var jsonResult = JsonConvert.SerializeObject(newDict, Newtonsoft.Json.Formatting.Indented);
            //Console.WriteLine(jsonResult);

        }
    }
}
