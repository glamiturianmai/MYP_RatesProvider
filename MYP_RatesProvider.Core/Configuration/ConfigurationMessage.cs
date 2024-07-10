namespace MYP_RatesProvider.Core.Configuration
{
    public class ConfigurationMessage
    {
        public ServiceType ServiceType { get; set; }
        public Dictionary<string, string> Configurations { get; set; }
    }
}
