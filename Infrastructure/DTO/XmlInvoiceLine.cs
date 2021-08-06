using System;
using System.Xml.Serialization;

namespace Infrastructure.DTO
{
	[Serializable]
	[XmlRoot(ElementName = "InvoiceLine")]
	public class XmlInvoiceLine
	{
		[XmlElement(ElementName = "LineID")]
		public string LineID { get; set; }
		[XmlElement(ElementName = "OrderID")]
		public string OrderID { get; set; }
		[XmlElement(ElementName = "OrderLineId")]
		public string OrderLineId { get; set; }
		[XmlElement(ElementName = "Sku")]
		public string Sku { get; set; }
		[XmlElement(ElementName = "QuantityShipped")]
		public int QuantityShipped { get; set; }
		[XmlElement(ElementName = "PO_Number")]
		public string PO_Number { get; set; }
		[XmlElement(ElementName = "Tracknumber")]
		public string Tracknumber { get; set; }
		[XmlElement(ElementName = "Shipvia")]
		public string Shipvia { get; set; }
		[XmlElement(ElementName = "OrdDate")]
		public DateTime OrdDate { get; set; }
		[XmlElement(ElementName = "ShipDate")]
		public string ShipDate { get; set; }
		[XmlElement(ElementName = "Total")]
		public string Total { get; set; }
		[XmlElement(ElementName = "ShippingCost")]
		public decimal ShippingCost { get; set; }
	}
}
