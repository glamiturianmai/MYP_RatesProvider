using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYP_RatesProvider.Core.Configuration
{
    public class ConfigurationMissingException(string message = "fault configuration") : Exception(message)
    {
    }

}
