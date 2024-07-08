using MYP_RatesProvider.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYP_RatesProvider.Core
{
    public class ConfigurationMessage
    {
        public ServiceType ServiceType { get; set; }
        public Dictionary<string, string> Configurations { get; set; }
    }
}
