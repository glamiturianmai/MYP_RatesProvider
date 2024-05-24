using MYP_RatesProvider.Strategies;

namespace MYP_RatesProvider;

class Context
{
    private IStrategy _strategy;

    public Context()
    { }
    public Context(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    public void SetStrategy(IStrategy strategy)
    {
        this._strategy = strategy;
    }

    public void GetDataCurrency()
    {
        this._strategy.GetDictionary();
    }
}
