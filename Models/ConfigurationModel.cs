namespace StockPriceNotificator.Models
{
    public class ConfigurationModel
    {
        public string YahooApiKey { get; set; }
        public string YahooServiceUrl { get; set; }
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public string RecipientEmail { get; set; }
    }

}
