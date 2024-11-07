using Newtonsoft.Json;
using StockPriceNotificator.Models;
using System.Globalization;

namespace StockPriceNotificator.ExternalServices.QuotationService
{
    public class YahooFinanceService
    {
        private readonly string _asset;
        private readonly ConfigurationModel _config;
        public YahooFinanceService(string asset, ConfigurationModel config)
        {
            _asset = asset;
            _config = config;
        }
        public async Task<decimal> GetCurrenPriceAsync()
        {
            var apiKey = _config.YahooApiKey;
            var serviceUrl = _config.YahooServiceUrl + $"{_asset}";
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("x-rapidapi-key", apiKey);
                    client.DefaultRequestHeaders.Add("x-rapidapi-host", "yahoo-finance15.p.rapidapi.com");

                    var response = await client.GetStringAsync(serviceUrl);

                    if (string.IsNullOrEmpty(response))
                        throw new Exception("Something went wrong with the Yahoo response.");

                    var result = JsonConvert.DeserializeObject<YahooResponseModel>(response);
                    var lastPriceString = result?.Body.PrimaryData.LastSalePrice.Replace("$", "");

                    if (!decimal.TryParse(lastPriceString, NumberStyles.Number, CultureInfo.InvariantCulture, out var lastPrice))
                    {
                        throw new FormatException("The price string could not be converted to a decimal.");
                    }

                    return lastPrice;
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception("There was an error connecting to the Yahoo Finance service.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Error parsing the response from Yahoo Finance.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving the stock price.", ex);
            }
        }
    }
}
