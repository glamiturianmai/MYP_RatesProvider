using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

public class DataCurrency
{
    private ICurrencyStrategy _strategy;

    public DataCurrency()
    { }
    public DataCurrency(ICurrencyStrategy strategy)
    {
        this._strategy = strategy;
    }

    public void SetStrategy(ICurrencyStrategy strategy)
    {
        this._strategy = strategy;
    }

    public async void GetDataCurrency()
    {
        var a = await this._strategy.GetData();
        this._strategy.ConvertDataToDictionary(a);
    }
}
