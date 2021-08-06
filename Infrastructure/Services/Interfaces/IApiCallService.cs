
using Infrastructure.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IApiCallService : IService
    {
        Task<JsonFromRestApiResponse> SendJson(List<JsonForRestApi> jsonToSend);
    }
}
