namespace MYP_RatesProvider.Strategies;

public interface ICurrencyStrategy
{
    public Task<Dictionary<string, object>> GetData();
    public Dictionary<string, object> ConvertDataToDictionary(Dictionary<string, object> data);
}
