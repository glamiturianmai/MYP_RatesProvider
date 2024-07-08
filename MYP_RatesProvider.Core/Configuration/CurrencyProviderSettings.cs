using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYP_RatesProvider.Core.Configuration;

public class CurrencyProviderSettings
{
    public string Url { get; private set; }

    public string Withdraw { get; private set; }
}
