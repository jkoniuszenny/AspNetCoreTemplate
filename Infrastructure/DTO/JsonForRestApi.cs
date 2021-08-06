using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class JsonForRestApi
    {
        public JsonForRestApi()
        {

        }

        public JsonForRestApi(JsonInputRoot jsonInputRoot)
        {
            Id = jsonInputRoot.Id;
            OrderNumber = jsonInputRoot.OrderNumber;
            ShipDate =jsonInputRoot.ShipDate;
            IsTransmitted = true; 
            DoNotSendConfirmation = true; 

            ShipmentDetails = new List<JsonShipmentDetail>();
            jsonInputRoot.ShipmentDetails.All(a =>
            {
                ShipmentDetails.Add(new JsonShipmentDetail()
                {
                    Carrier = a.Carrier,
                    CarrierManifestCode = null,
                    OrderDisplayNumber = a.OrderDisplayNumber,
                    ShippingCost = a.ShippingCost,
                    TrackingNumber = a.TrackingNumber,
                    TrackingNumberUrl = null
                });
                return true;
            });
            


            ShipmentLines = new List<JsonShipmentLine>();
            jsonInputRoot.ShipmentLines.All(a =>
            {
                ShipmentLines.Add(new JsonShipmentLine()
                {

                    OrderLineId = a.OrderLineId,
                    QuantityShipped = Convert.ToInt32(a.QuantityShipped),
                    Sku = a.Sku
                });
                return true;
            });

        }

        public int Id { get; set; }
        public List<JsonShipmentDetail> ShipmentDetails { get; set; }
        public List<JsonShipmentLine> ShipmentLines { get; set; }
        public string OrderNumber { get; set; }
        public DateTime ShipDate { get; set; }
        public bool IsTransmitted { get; set; }
        public bool DoNotSendConfirmation { get; set; }
    }
}
