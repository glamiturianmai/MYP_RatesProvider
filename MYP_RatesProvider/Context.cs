using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

public class Context
{
    private IStrategyForChoseSourceForCurrency _strategy;

    public Context()
    { }
    public Context(IStrategyForChoseSourceForCurrency strategy)
    {
        this._strategy = strategy;
    }

    public void SetStrategy(IStrategyForChoseSourceForCurrency strategy)
    {
        this._strategy = strategy;
    }

    public async void GetDataCurrency()
    {
        var a = await this._strategy.GetData();
        await this._strategy.ConvertDataToDictionary(a);
    }
}
