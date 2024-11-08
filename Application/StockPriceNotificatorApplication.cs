using StockPriceNotificator.ExternalServices.EmailService;
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
            Console.WriteLine($"Monitoring {_asset}, with sellPrice = ${_sellPrice} and buyPrice = ${_buyPrice}...");
            while (true)
            {
                var isValid = IsValidInputs();
                if (!isValid)
                {
                    Console.WriteLine($"Check the inputs again! Buy: ${_buyPrice}, Sell: ${_sellPrice}");
                    return;
                }
                var yahooExternalService = new YahooFinanceService(_asset, _config);

                var currentPrice = await yahooExternalService.GetCurrenPriceAsync();
                Console.WriteLine($"Current price of {_asset}: {currentPrice}");

                var order = BuildEmailOrder(currentPrice);

                var emailService = new SMTPService(_config, _asset);

                emailService.SendEmail(order, currentPrice);
            }
        }

        public bool IsValidInputs()
        {
            if (string.IsNullOrEmpty(_asset) ||
                _buyPrice < 0 ||
                _sellPrice < 0 ||
                _buyPrice > _sellPrice ||
                (_buyPrice  == 0 && _sellPrice == 0)
                )
                return false;
            else
                return true;
        }

        private string BuildEmailOrder(decimal currentPrice)
        {
            if (currentPrice <= _buyPrice)
            {
                Console.WriteLine("Must Buy!");
                return "buy";
            }
            if (currentPrice >= _sellPrice)
            {
                Console.WriteLine("Must Sell!");
                return "sell";
            }
            if (currentPrice < _sellPrice && currentPrice > _buyPrice)
            {
                Console.WriteLine("Hold On!");
            }
            return "";
        }
    }
}
