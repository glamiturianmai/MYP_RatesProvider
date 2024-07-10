using MYP_RatesProvider.Core.Configuration;

namespace MYP_RatesProvider.Core
{
    public class ConfigurationMessage
    {
        public ServiceType ServiceType { get; set; }
        public Dictionary<string, string> Configurations { get; set; }
    }
}
