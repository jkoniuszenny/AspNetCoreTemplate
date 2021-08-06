using System;

namespace Infrastructure.DTO
{
    public class JsonInputInvoiceDetail
    {
        public string PropagoOrderID { get; set; }
        public double AmountToPay { get; set; }
        public DateTime DateShipped { get; set; }
    }

}
