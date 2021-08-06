using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IMailSendingService : IService
    {
        Task Send(List<JsonInputInvoiceDetail> jsonInputInvoiceDetails, string receiverEmail, string receiveerName);
    }
}
