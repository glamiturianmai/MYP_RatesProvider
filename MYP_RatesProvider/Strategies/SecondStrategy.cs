using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYP_RatesProvider.Strategies
{
    class SecondStrategy : IStrategy
    {
        public async Task GetDictionary()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = Options.urlSecond;
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json);
                    var jsonNew = JsonConvert.SerializeObject(data["data"], Newtonsoft.Json.Formatting.Indented);

                    JObject obj = JObject.Parse(jsonNew);
                    var newDict = new Dictionary<string, object>();
                    newDict.Add("timestamp", DateTime.Now);
                    var ratesDict = new Dictionary<string, double>();

                    foreach (var item in obj)
                    {
                        string currencyCode = (string)"USD" + item.Key;
                        double currencyValue = (double)item.Value["value"];
                        ratesDict.Add(currencyCode, currencyValue);
                    }

                    newDict.Add("rates", ratesDict);

                    var jsonResult = JsonConvert.SerializeObject(newDict, Newtonsoft.Json.Formatting.Indented);
                    Console.WriteLine(jsonResult);
                }
            }

        }
    }
}
