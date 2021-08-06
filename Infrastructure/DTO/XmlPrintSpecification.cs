using System;
using System.Xml.Serialization;

namespace Infrastructure.DTO
{
	[Serializable]
	[XmlRoot(ElementName = "PrintSpecification")]
	public class XmlPrintSpecification
	{
		[XmlElement(ElementName = "InvoiceList")]
		public XmlInvoiceList InvoiceList { get; set; }
		[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsi { get; set; }
		[XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string Xsd { get; set; }
	}
}
