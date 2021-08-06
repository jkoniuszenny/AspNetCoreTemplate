using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class JsonShipmentDetail
    {
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string TrackingNumberUrl { get; set; }
        public double ShippingCost { get; set; }
        public string OrderDisplayNumber { get; set; }
        public string CarrierManifestCode { get; set; }

 
    }
}
