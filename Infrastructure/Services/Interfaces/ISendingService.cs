using Core.Models.Domain;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface ISendingService : IService
    {
        Task PrepareJsonOutputs(IEnumerable<AwardsAtlantaOrders> tabel);
        Task<IEnumerable<AwardsAtlantaOrders>> SaveInputJsonToTable(List<IFormFile> files);
        Task<IEnumerable<AwardsAtlantaOrders>> SaveInputJsonToTable(JsonInputRoot json);
        Task SaveTableChanges(IEnumerable<AwardsAtlantaOrders> tabel);
    }
}
