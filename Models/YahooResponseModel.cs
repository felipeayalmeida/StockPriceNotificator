namespace StockPriceNotificator.Models
{

    public class YahooResponseModel
    {
        public Meta Meta { get; set; }
        public Body Body { get; set; }
    }

    public class Meta
    {
        public string Version { get; set; }
    }
    public class Body
    {
        public PrimaryData PrimaryData { get; set; }
    }

    public class PrimaryData
    {
        public string LastSalePrice { get; set; }
    }



}
