using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace MYP_RatesProvider.Core.Services;

public class HttpService
{
    // GetAsync<T> //получаем c url
    public async Task<Dictionary<string, object>> GetDataFromSource(string urlFromAppSettings)
    {
        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(urlFromAppSettings);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
        else
        {
            // throw some exception;
            throw new Exception(); //я не уверена

        }

    }
}
