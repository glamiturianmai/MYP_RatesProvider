using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MYP_RatesProvider.Core.Configuration
{
    public static class ConfigureServicesFromJson
    {
        public static void AddConfigurationServicesFromJson(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(sp => configuration.GetSection(ConfigurationSettings.CurrencyProviderSettings)
                .Get<CurrencyProviderSettings>(options => options.BindNonPublicProperties = true));
        }
    }
}
