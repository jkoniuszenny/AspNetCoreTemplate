using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Interfaces
{
    public interface IFailureService : IService
    {
        Task SaveErrrorToTable(string json, string errorMessage);
    }
}
