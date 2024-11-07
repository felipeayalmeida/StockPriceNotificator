using StockPriceNotificator.Models;
using System.Net.Mail;
using System.Net;

namespace StockPriceNotificator.ExternalServices.EmailService
{
    public class SMTPService
    {
        private readonly ConfigurationModel _config;

        public SMTPService(ConfigurationModel config)
        {
            _config = config;
        }
        public void SendEmail()
        {
            using var smtpClient = new SmtpClient(_config.SMTPServer, _config.SMTPPort)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_config.SMTPUsername, _config.SMTPPassword),
            };
        }
    }
}
