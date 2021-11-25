using Core.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class ExampleService : IExampleService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public ExampleService(
            IDatabaseRepository databaseRepository)
        {
            _databaseRepository = databaseRepository;
        }

        public async Task Example()
        {

            await Task.CompletedTask;
        }
    }
}
