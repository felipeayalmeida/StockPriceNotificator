using StockPriceNotificator.ExternalServices.QuotationService;
using StockPriceNotificator.Models;
using System.Text.Json;

namespace StockPriceNotificator.Application
{
    public class StockPriceNotificatorApplication(string asset, decimal sellPrice, decimal buyPrice)
    {
        private readonly string _asset = asset;
        private readonly decimal _sellPrice = sellPrice;
        private readonly decimal _buyPrice = buyPrice;
        private readonly ConfigurationModel _config = JsonSerializer.Deserialize<ConfigurationModel>(File.ReadAllText("config.json")) ?? new ConfigurationModel();

        public async Task StartMonitoringAsync()
        {
            Console.WriteLine($"Monitoring {_asset}, with sellPrice = {_sellPrice} and buyPrice = {_buyPrice}...");
            while (true)
            {
                var yahooExternalService = new YahooFinanceService(_asset, _config);

                var currentPrice = await yahooExternalService.GetCurrenPriceAsync();
                Console.WriteLine($"Current price of {_asset}: {currentPrice}");

                Thread.Sleep(5000);
            }
        }
    }
}
