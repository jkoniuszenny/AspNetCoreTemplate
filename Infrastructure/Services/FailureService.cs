using Core.Models.Domain;
using Core.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class FailureService : IFailureService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public FailureService(
            IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task SaveErrrorToTable(string json, string errorMessage)
        {
            AwardsAtlantaPostFailures awardsAtlantaPostFailures = new AwardsAtlantaPostFailures()
            {
                ApiErrorMessage = errorMessage,
                PostFailDateTime = DateTime.Now,
                PostJson = json
            };

            await _databaseRepository.Insert(new[] { awardsAtlantaPostFailures });
        }
    }
}
