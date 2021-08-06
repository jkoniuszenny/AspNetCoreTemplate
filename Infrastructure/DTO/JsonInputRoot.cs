using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class JsonInputRoot
    {
        public int Id { get; set; }
        public List<JsonInputShipmentDetail> ShipmentDetails { get; set; }
        public List<JsonInputShipmentLine> ShipmentLines { get; set; }
        public List<JsonInputInvoiceDetail> InvoiceDetail { get; set; }
        public string OrderNumber { get; set; }
        public DateTime ShipDate { get; set; }
    }

}
