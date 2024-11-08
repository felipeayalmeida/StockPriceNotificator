using StockPriceNotificator.Application;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {

        string asset = args.Length > 0 ? args[0] : "AAPL"; 
        decimal sellPrice = args.Length > 1 ? decimal.Parse(args[1]) : 220m; 
        decimal buyPrice = args.Length > 2 ? decimal.Parse(args[2]) : 210m;
        var stockNotificatiorApplication = new StockPriceNotificatorApplication(asset, sellPrice, buyPrice);
        await stockNotificatiorApplication.StartMonitoringAsync();
    }
}
