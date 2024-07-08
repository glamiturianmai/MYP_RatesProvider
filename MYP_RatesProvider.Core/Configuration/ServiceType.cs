using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYP_RatesProvider.Core.Configuration
{
    public enum ServiceType
    {
        unknown = 0,
        crm = 1,
        transactionStore = 2,
        ratesProvider = 3,
        leadStatusUpdater = 4,
        reportingService = 5,
        emailSender = 6
    }

}
