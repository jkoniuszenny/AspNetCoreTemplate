using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO.GlobalResponseModel
{
    public class ResponseDto<T> where T : class
    {
        public bool Success { get; set; }
        public PayloadDto<T> Payload { get; set; }
        public ErrorDto Error { get; set; }
    }


    public class ErrorDto
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class PayloadDto<T> where T : class
    {
        public T Data { get; set; }
    }

    public class NullClassDto
    {

    }
}
