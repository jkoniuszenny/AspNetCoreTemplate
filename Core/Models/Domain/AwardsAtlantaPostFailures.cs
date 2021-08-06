using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Domain
{
    [Table("AwardsAtlantaPostFailures", Schema = "dbo")]
    public class AwardsAtlantaPostFailures
    {
        [Key]
        public int Id { get; set; }
        public string PostJson { get; set; }
        public string ApiErrorMessage { get; set; }
        [Column(TypeName = "DateTime")]
        public DateTime PostFailDateTime { get; set; }
    }
}
