using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using RestSharp;
using System.Text.Json;


namespace MYP_RatesProvider.Core.Configuration
{
    public class HttpClientService
    {
        public async Task<T> Get<T>(string urlForRequest, CancellationToken cancellationToken)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true
            };

            var options = new RestClientOptions()
            {
                ConfigureMessageHandler = _ => handler,
                Timeout = TimeSpan.FromSeconds(380)
            };

            var client = new RestClient(options);
            var request = new RestRequest(urlForRequest);

            var response = await client.ExecuteAsync(request, Method.Get, cancellationToken);

            if (!response.IsSuccessful)
            {
                throw new HttpRequestException($"Request failed with status code {response.StatusCode} and message: {response.ErrorMessage}, Content: {response.Content}");
            }

            try
            {
                var result = JsonSerializer.Deserialize<T>(response.Content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
                });

                if (result == null)
                {
                    throw new HttpRequestException("Deserialized response is null");
                }

                return result;
            }
            catch (JsonException ex)
            {
                throw new HttpRequestException("Failed to deserialize response", ex);
            }
        }
    }
}
