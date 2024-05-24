using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MYP_RatesProvider.Strategies
{
    class FirstStrategy : IStrategy
    {
        public async Task GetDictionary()
        {
            string url = Options.urlFirst;
            using (HttpClient client = new HttpClient())
            {

                HttpResponseMessage response = await client.GetAsync(url);


                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
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

                    var jsonResult = JsonConvert.SerializeObject(newDict, Newtonsoft.Json.Formatting.Indented);
                    Console.WriteLine(jsonResult);

                }
            }

        }
    }
}
