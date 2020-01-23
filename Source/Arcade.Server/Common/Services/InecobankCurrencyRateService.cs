using Common.Enums;
using System.Collections.Generic;
using Flurl.Http;
using System.Threading.Tasks;
using System;

namespace Common.Services
{

    public class InecobankCurrencyRateService
    {

        class InecobankResponseItemRates
        {
            public float? Buy { get; set; }
            public float? Sell { get; set; }
        }

        class InecobankResponseItem
        {
            public string Code { get; set; }

            public InecobankResponseItemRates Online { get; set; }
        }

        class InecobankResponse
        {
            public bool Success { get; set; }
            public List<InecobankResponseItem> Items { get; set; }
        }

        public async Task<Dictionary<Currency, float>> GetRates()
        {
            InecobankResponse response = await("https://www.inecobank.am/api/rates").GetJsonAsync<InecobankResponse>();
            Dictionary<Currency, float> result = new Dictionary<Currency, float>();
            foreach (var item in response.Items)
            {
                Currency currency;
                if (Enum.TryParse(item.Code, out currency) && item.Online.Sell.HasValue)
                    result.Add(currency, item.Online.Sell.Value);
            }
            return result;
        }
    }
}
