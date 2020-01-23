using Common.Enums;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Common.Localization
{


    public class CurrencyProvider
    {
        public CurrencyProvider()
        {

        }

        Dictionary<Currency, float> Rates;
        private float RoundUp(float input, int places)
        {
            double multiplier = Math.Pow(10, System.Convert.ToDouble(places));
            return (float)(Math.Ceiling(input * multiplier) / multiplier);
        }

        public async Task<float> Convert(string currencystring, float value)
        {
            Currency currency;
            if (!Enum.TryParse(currencystring, out currency)) throw new ArgumentException($"Unsupported currency  {currencystring}");
            await ResolveRates();
            if (!Rates.ContainsKey(currency)) throw new ArgumentException($"Currency rates couldn't be loaded for {currency}");
            return RoundUp(value / Rates[currency], 2);
        }

        private async Task ResolveRates()
        {
            if (Rates == null) {
                Rates = await new InecobankCurrencyRateService().GetRates();
                Rates[Currency.AMD] = 1; 
            }
            await Task.CompletedTask;
        }
    }
}
