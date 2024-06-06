using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider.Core
{
    public class RatesManager
    {
        private readonly ILogger<RatesManager> _logger;
        private readonly IConfiguration _configuration;

        public RatesManager(ILogger<RatesManager> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task DataService()
        {
           var context = new DataCurrency();

            try
            {
                //Console.WriteLine("FirstStrategy"); //тут еще будем стучаться в раббит 
                context.SetStrategy(new PrimaryCurrencyProvider(_configuration));
                context.GetDataCurrency();
            }
            catch (Exception ex){
                Console.WriteLine($"{ex.Message}");
                context.SetStrategy(new SecondaryCurrencyProvider(_configuration));
                context.GetDataCurrency();
            }
        }
    }
}
