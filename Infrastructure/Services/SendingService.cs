using AutoMapper;
using Core.Models.Domain;
using Core.Repositories.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Infrastructure.Services
{
    public class SendingService : ISendingService
    {
        private readonly IDatabaseRepository _databaseRepository;

        public SendingService(
            IDatabaseRepository databaseRepository
            )
        {
            _databaseRepository = databaseRepository;
        }


        public async Task<IEnumerable<AwardsAtlantaOrders>> SaveInputJsonToTable(JsonInputRoot json)
        {
            List<AwardsAtlantaOrders> awardsAtlantaOrders = new List<AwardsAtlantaOrders>();
                    awardsAtlantaOrders.Add(new AwardsAtlantaOrders()
                    {
                        PostDateTime = null,
                        PostJson = null,
                        ReceivedXML = JsonSerializer.Serialize(json),
                        ReceivedDateTime = DateTime.Now
                    });

            await _databaseRepository.Insert(awardsAtlantaOrders);

            return awardsAtlantaOrders;
        }
        public async Task<IEnumerable<AwardsAtlantaOrders>> SaveInputJsonToTable(List<IFormFile> files)
        {
            List<AwardsAtlantaOrders> awardsAtlantaOrders = new List<AwardsAtlantaOrders>();

            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;

                    var des = JsonSerializer.Deserialize<JsonInputRoot>(Encoding.ASCII.GetString(memoryStream.ToArray()));

                    awardsAtlantaOrders.Add(new AwardsAtlantaOrders()
                    {
                        PostDateTime = null,
                        PostJson = null,
                        ReceivedXML = JsonSerializer.Serialize(des),
                        ReceivedDateTime = DateTime.Now
                    });
                }
            }

            await _databaseRepository.Insert(awardsAtlantaOrders);

            return awardsAtlantaOrders;
        }

        public async Task PrepareJsonOutputs(IEnumerable<AwardsAtlantaOrders> tabel)
        {

            tabel.All(a =>
            {
                List<JsonForRestApi> jsonForRestApi = new List<JsonForRestApi>();

                jsonForRestApi.Add(new JsonForRestApi(JsonSerializer.Deserialize<JsonInputRoot>(a.ReceivedXML)));

                a.PostJson = JsonSerializer.Serialize(jsonForRestApi);
                return true;
            });

            await Task.CompletedTask;
        }

        public async Task SaveTableChanges(IEnumerable<AwardsAtlantaOrders> tabel)
        {
            await _databaseRepository.Update(tabel);
        }
    }
}
