using MYP_RatesProvider.Core.Exceptions;
using Newtonsoft.Json;

namespace MYP_RatesProvider.Core.Services;

public class HttpService
{

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
            throw new HttpResponseException($"request to { urlFromAppSettings} failed, response status is: {response.StatusCode}");

        }

    }
}
