using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Infrastructure.DTO
{
	[Serializable]
	[XmlRoot(ElementName = "InvoiceList")]
	public class XmlInvoiceList
	{
		[XmlElement(ElementName = "InvoiceLine")]
		public XmlInvoiceLine InvoiceLine { get; set; }
	}
}
