using StockPriceNotificator.ExternalServices.QuotationService;
using StockPriceNotificator.Models;
using System.Text.Json;

namespace StockPriceNotificator.Application
{
    public class StockPriceNotificatorApplication
    {
        private readonly string _asset;
        private readonly decimal _sellPrice;
        private readonly decimal _buyPrice;
        private readonly ConfigurationModel _config;

        public StockPriceNotificatorApplication(string asset, decimal sellPrice, decimal buyPrice)
        {
            _asset = asset;
            _sellPrice = sellPrice;
            _buyPrice = buyPrice;
            _config = JsonSerializer.Deserialize<ConfigurationModel>(File.ReadAllText("config.json"));

        }

        public async Task StartMonitoringAsync()
        {
            Console.WriteLine($"Monitoring {_asset}, with sellPrice = {_sellPrice} and buyPrice = {_buyPrice}...");
            while (true)
            {
                var yahooExternalService = new YahooFinanceService(_asset,_config);

                var currentPrice = await yahooExternalService.GetCurrenPriceAsync();
                Console.WriteLine($"Current price of {_asset}: {currentPrice}");

                Thread.Sleep(5000);
            }
        }
    }
}
