namespace StockPriceNotificator.Application
{
    public class StockPriceNotificatorApplication
    {
        private readonly string _asset;
        private readonly decimal _sellPrice;
        private readonly decimal _buyPrice;

        public StockPriceNotificatorApplication(string asset, decimal sellPrice, decimal buyPrice)
        {
            _asset = asset;
            _sellPrice = sellPrice;
            _buyPrice = buyPrice;
        }

        public async Task StartMonitoringAsync()
        {
            Console.WriteLine($"Monitoring {_asset}, with sellPrice = {_sellPrice} and buyPrice = {_buyPrice}...");
            while (true)
            {
                Console.WriteLine("Do External Calls");
                Thread.Sleep(5000);
            }
        }
    }
}
