using Microsoft.Extensions.Configuration;

namespace MYP_RatesProvider.Core.Configuration
{
    public static class SettingsConfigurationManager
    {
        private static async Task<Dictionary<string, string>> GetConfigurationSettings()
        {
            var httpClientService = new HttpClientService();
            var configurationSettings = await httpClientService.Get<Dictionary<string, string>>(ConfigurationSettings.ConfigurationServiceUrl, new CancellationToken());

            return configurationSettings;
        }

        public static async Task<Dictionary<string, string>> ReadSettingsFromConfigurationManager(this IConfiguration configuration)
        {
            var configurationSettings = await GetConfigurationSettings();
            configuration.ReadSection(ConfigurationSettings.CurrencyProviderSettings, configurationSettings);


            return configurationSettings;
        }

        private static void ReadSection(this IConfiguration configuration, string keySection, Dictionary<string, string> configurationSettings)
        {
            var section = configuration.GetSection(keySection).GetChildren();
            foreach (var key in section)
            {
                SetValueFromConfigurationManager(key, configurationSettings);
            }
        }

        private static void SetValueFromConfigurationManager(IConfigurationSection key, Dictionary<string, string> configurationSettings)
        {
            var value = key.Value ?? throw new ConfigurationMissingException("Value of configuration key is null");
            if (!configurationSettings.TryGetValue(value, out var configurationSetting))
            {
                throw new ConfigurationMissingException("Configuration manager variables are not specified");
            }
            key.Value = configurationSetting;
        }

        public static void UpdateSettingsFromConfigurationManager(this IConfiguration configuration, Dictionary<string, string> settings)
        {
            var defaultSection = configuration.GetSection(ConfigurationSettings.DefaultConfigurationSection);
            configuration.UpdateSection(ConfigurationSettings.CurrencyProviderSettings, settings);
        }

        private static void UpdateSection(this IConfiguration configuration, string keySection, Dictionary<string, string> configurationSettings)
        {
            var sourceSection = configuration.GetSection(ConfigurationSettings.DefaultConfigurationSection).GetSection(keySection).GetChildren(); ;
            var destinationSection = configuration.GetSection(keySection).GetChildren();

            var sourceKeys = sourceSection.Select(x => x.Key).ToList();
            var destinationKeys = destinationSection.Select(x => x.Key).ToList();

            for (var i = 0; i < sourceKeys.Count; i++)
            {
                var sourceKey = configuration.GetSection($"{ConfigurationSettings.DefaultConfigurationSection}:{keySection}:{sourceKeys[i]}");
                var destinationKey = configuration.GetSection($"{keySection}:{destinationKeys[i]}");
                UpdateValueFromConfigurationManager(sourceKey, destinationKey, configurationSettings);
            }
        }

        private static void UpdateValueFromConfigurationManager(IConfigurationSection sourceKey, IConfigurationSection destinationKey, Dictionary<string, string> configurationSettings)
        {
            var value = sourceKey.Value ?? throw new ConfigurationMissingException("Value of configuration key is null");
            if (!configurationSettings.TryGetValue(value, out var configurationSetting))
            {
                throw new ConfigurationMissingException("Configuration manager variables are not specified");
            }
            destinationKey.Value = configurationSetting;
        }
    }
}
