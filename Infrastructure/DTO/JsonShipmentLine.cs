using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{

    public class JsonShipmentLine
    {
        public string Sku { get; set; }
        public int QuantityShipped { get; set; }
        public string OrderLineId { get; set; }

    }
}
