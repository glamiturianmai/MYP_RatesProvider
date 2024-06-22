using Messaging.Shared;

namespace MYP_RatesProvider.Strategies;

public interface ICurrencyStrategy
{
    public Task<RatesInfo> GetData();
    public RatesInfo ConvertDataToDictionary(Dictionary<string, object> data);
    public int GetId();
}
