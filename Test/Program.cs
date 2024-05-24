using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
class Program
{
    static async Task Main()
    {
        using (HttpClient client = new HttpClient())
        {

            

            //url

            HttpResponseMessage response1 = await client.GetAsync(url1);


            if (response1.IsSuccessStatusCode)
            {
                string json1 = await response1.Content.ReadAsStringAsync();
                var data1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(json1);

                var jsonNew1 = JsonConvert.SerializeObject(data1["rates"], Newtonsoft.Json.Formatting.Indented);


                JObject obj1 = JObject.Parse(jsonNew1);

                var newDict1 = new Dictionary<string, object>();
                newDict1.Add("timestamp", DateTime.Now);
                newDict1.Add("base", "USD");

                var ratesDict1 = new Dictionary<string, double>();

                foreach (var item in obj1)
                {
                    ratesDict1.Add(item.Key, (double)item.Value);
                }

                newDict1.Add("rates", ratesDict1);

                var json11 = JsonConvert.SerializeObject(newDict1, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json11);

            }


            HttpResponseMessage response2 = await client.GetAsync(url2);


            if (response2.IsSuccessStatusCode)
            {

                string json2 = await response2.Content.ReadAsStringAsync();
                var data2 = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(json2);

                var jsonNew2 = JsonConvert.SerializeObject(data2["data"], Newtonsoft.Json.Formatting.Indented);

                JObject obj2 = JObject.Parse(jsonNew2);

                var newDict2 = new Dictionary<string, object>();
                newDict2.Add("timestamp", DateTime.Now);
                newDict2.Add("base", "USD");

                var ratesDict2 = new Dictionary<string, double>();

                foreach (var item in obj2)
                {
                    string currencyCode = item.Key;
                    double currencyValue = (double)item.Value["value"];
                    ratesDict2.Add(currencyCode, currencyValue);
                }

                newDict2.Add("rates", ratesDict2);

                var json22 = JsonConvert.SerializeObject(newDict2, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(json22);
            }
        }

    }
}