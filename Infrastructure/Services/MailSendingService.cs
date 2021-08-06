using Infrastructure.DTO;
using Infrastructure.Services.Interfaces;
using Infrastructure.Settings;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MailSendingService : IMailSendingService
    {
        private readonly SendGridSettings _sendGridSettings;

        public MailSendingService(
            SendGridSettings sendGridSettings
            )
        {
            _sendGridSettings = sendGridSettings;
        }

        public async Task Send(List<JsonInputInvoiceDetail> jsonInputInvoiceDetails, string receiverEmail, string receiveerName)
        {
            foreach (var item in jsonInputInvoiceDetails)
            {
                var client = new SendGridClient(_sendGridSettings.ApiKey);
                var from = new EmailAddress(_sendGridSettings.SenderEmail, _sendGridSettings.SenderName);
                var to = new EmailAddress(receiverEmail, receiveerName);

                var subject = $"Invoice {item.PropagoOrderID}";

                var plainTextContent = @$"PropagoOrderID: {item.PropagoOrderID}
                                     AmountToPay: {item.AmountToPay}
                                     DateShipped: {item.DateShipped}";

                var htmlContent = @$"PropagoOrderID: {item.PropagoOrderID}
                                     AmountToPay: {item.AmountToPay}
                                     DateShipped: {item.DateShipped}";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                await client.SendEmailAsync(msg);
            };
        }
    }
}
