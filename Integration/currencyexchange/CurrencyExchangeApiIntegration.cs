using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lafacustorev2.Integration.currencyexchange
{
    public class CurrencyExchangeApiIntegration
    {
        private readonly ILogger<CurrencyExchangeApiIntegration> _logger;

        private const string API_URL="https://currency-exchange.p.rapidapi.com/exchange"; //https://rapidapi.com/fyhao/api/currency-exchange/

        public CurrencyExchangeApiIntegration(ILogger<CurrencyExchangeApiIntegration> logger)
        {
            _logger = logger;
        }

        public async Task<double> GetExchangeRate(string fromCurrency, string toCurrency){
            string requestUrl = $"{API_URL}?from={fromCurrency}&to={toCurrency}";
            double exchangeRate = 0.0;
            using (HttpClient client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(requestUrl),
                    Headers =
                    {
                        { "X-RapidAPI-Key", "0480f8b7a5mshe85c70b898a64c9p12b6eajsn98b3968305bb" }
                    },
                };

                using (var response = await client.SendAsync(request))
                {
	                response.EnsureSuccessStatusCode();
	                var body = await response.Content.ReadAsStringAsync();
                    _logger.LogDebug($"Response: {body}");
	                exchangeRate = Convert.ToDouble(body);
                }
            }
            exchangeRate = Math.Round(exchangeRate, 2);
            return exchangeRate;
        }
    }
}