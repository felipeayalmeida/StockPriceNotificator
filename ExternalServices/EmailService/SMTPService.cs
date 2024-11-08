using StockPriceNotificator.Models;
using System.Net;
using System.Net.Mail;

namespace StockPriceNotificator.ExternalServices.EmailService
{
    public class SMTPService
    {
        private readonly ConfigurationModel _config;
        private readonly string _asset;

        public SMTPService(ConfigurationModel config, string asset)
        {
            _config = config;
            _asset = asset;
        }
        public void SendEmail(string order, decimal price)
        {
            using var smtpClient = new SmtpClient(_config.SMTPServer, _config.SMTPPort)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(_config.SMTPUsername, _config.SMTPPassword),
            };

            var emailMessage = new MailMessage
            {
                From = new MailAddress(_config.SMTPUsername),
                Subject = $"Alert: {_asset} {order} at ${price}",
                Body = $"The {_asset} has hit the {order} price of ${price}.",
                IsBodyHtml = false,
            };

            emailMessage.To.Add(_config.RecipientEmail);

            try
            {
                smtpClient.Send(emailMessage);
                Console.WriteLine($"Order email sent for {_asset} - {order} at {price:C}");
                Thread.Sleep(5000);
            }
            catch (SmtpException smtpEx)
            {
                Console.Error.WriteLine($"SMTP Error: {smtpEx.Message}");
                throw new InvalidOperationException("Failed to send email. Check SMTP configuration.", smtpEx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}
