namespace MYP_RatesProvider;

public class Options
{
    public static string UrlFirst
    {
        get
        {
            //вот тут 10000 запросов 
            return "https://openexchangerates.org/api/latest.json?app_id=";

        }
    }

    public static string UrlSecond
    {
        get
        {
            return "https://api.currencyapi.com/v3/latest?apikey=";
        }
    }
}
