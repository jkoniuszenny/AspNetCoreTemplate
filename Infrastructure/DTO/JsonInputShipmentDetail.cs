namespace Infrastructure.DTO
{
    public class JsonInputShipmentDetail
    {
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public double ShippingCost { get; set; }
        public string OrderDisplayNumber { get; set; }
    }


}
