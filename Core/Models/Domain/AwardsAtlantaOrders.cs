using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Domain
{
    [Table("AwardsAtlantaOrders", Schema = "dbo")]
    public class AwardsAtlantaOrders
    {
        [Key]
        public int Id { get; set; }
        public string PostJson { get; set; }
        public string ReceivedXML { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime? ReceivedDateTime { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime? PostDateTime { get; set; }
    }
}
