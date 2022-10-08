using Microsoft.Extensions.Primitives;
using System.Text;

namespace ShedulerApp.Models;
public class CurrencyModel
{
    public class ExchangeRate
    {
        public string exchange_rate_buy { get; set; }
        public string currency { get; set; }
    }

    public class Root
    {
        public string base_currency { get; set; }
        public List<ExchangeRate> exchange_rates { get; set; }
        public string base_currency_date { get; set; }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var exchRate in exchange_rates)
            {
                builder.Append('\"');
                builder.Append(exchRate.exchange_rate_buy);
                builder.Append("\",\"");
                builder.Append(exchRate.currency);
                builder.Append("\"\n");
            }
            return builder.ToString();
        }
    }
}