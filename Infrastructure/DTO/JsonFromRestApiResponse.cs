using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class JsonFromRestApiResponse
    {
        public string ReturnCode { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
        public string ErrorGuid { get; set; }
    }
}
