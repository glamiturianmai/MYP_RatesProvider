namespace MYP_RatesProvider.Strategies;

public interface IStrategyForChoseSourceForCurrency
{
    public Task<Dictionary<string, object>> GetData();
    public Task<Dictionary<string, object>> ConvertDataToDictionary(Dictionary<string, object> data);
}
