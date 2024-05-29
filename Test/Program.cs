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
            
            string url2 = "";
            string url3 = "";
            HttpResponseMessage response = await client.GetAsync(url3);

            Console.WriteLine(response.StatusCode);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content); //вот такой вид и хотим 
            }

            //теперь надо преобразовать также: 
            HttpResponseMessage response2 = await client.GetAsync(url2);

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var data1 = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonResponse);

            var jsonResponse1 = JsonConvert.SerializeObject(data1, Newtonsoft.Json.Formatting.Indented);
            // Console.WriteLine(jsonResponse1); 


            var a = data1["data"];
            foreach (var item in a)
            {
                Console.WriteLine(item.Value);
                var outputJsond3 = JsonConvert.SerializeObject(item.Value, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(outputJsond3);

                var data3 = JsonConvert.DeserializeObject<Dictionary<string, object>>(outputJsond3);
                var q = JsonConvert.SerializeObject(data3, Newtonsoft.Json.Formatting.Indented);
                Console.WriteLine(q);
            }

            var data2 = new Dictionary<string, object>
            {
                { "link_to_data", url2 },
                { "base", "USD" },
                { "rates",  data1["data"]}
            };


            var outputJsond = JsonConvert.SerializeObject(data2, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(outputJsond);




        }
    }
    
}
//}
//{ "rates", ((Dictionary<string, object>)data1.Keys.Any["data"]).ToDictionary(kv => kv.Key, kv => (double)kv.Value) }
//                };
//{ "rates", data1.ToDictionary(kv => kv.Key, kv => (double)kv.Value["value"]) }
//};