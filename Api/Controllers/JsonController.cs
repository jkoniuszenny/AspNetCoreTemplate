using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.DTO;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class JsonController : ControllerBase
    {
        private readonly ISendingService _sendingService;
        private readonly IApiCallService _apiCallService;
        private readonly IFailureService _failureService;
        private readonly IMailSendingService _mailSendingService;

        public JsonController(
            ISendingService sendingService,
            IApiCallService apiCallService,
            IFailureService failureService,
            IMailSendingService mailSendingService
            )
        {
            _sendingService = sendingService;
            _apiCallService = apiCallService;
            _failureService = failureService;
            _mailSendingService = mailSendingService;
        }

        [HttpGet]
        public async Task<string> GetAsync() => await Task.FromResult($"I'm alive - - {GetType().Assembly.GetName().Version}");


        [HttpPost]
        public async Task<JsonResult> PostAsync(JsonInputRoot json)
        {
            List<string> result = new List<string>();

            //1. Save to tabele Json Input
            var table = await _sendingService.SaveInputJsonToTable(json);

            //2. Prepare Json Output
            await _sendingService.PrepareJsonOutputs(table);

            //3. Send Json
            foreach (var item in table)
            {
                try
                {
                    List<JsonForRestApi> jsonForRestApi = JsonSerializer.Deserialize<List<JsonForRestApi>>(item.PostJson);

                    var response = await _apiCallService.SendJson(jsonForRestApi);

                    item.PostDateTime = DateTime.Now;


                    //When error then send to error table                
                    if (response.ReturnCode.ToLower() != "success")
                        await _failureService.SaveErrrorToTable(JsonSerializer.Serialize(item), response.Message);
                    //When success send mail with Invoice
                    else
                    {
                        List<JsonInputRoot> jsonInputRoot = JsonSerializer.Deserialize<List<JsonInputRoot>>(item.ReceivedXML);

                        string receiverEmail = "";
                        string receiveerName = "";

                        await _mailSendingService.Send(jsonInputRoot.SelectMany(s => s.InvoiceDetail).ToList(), receiverEmail, receiveerName);
                    }

                }
                catch (Exception ex)
                {
                    await _failureService.SaveErrrorToTable(JsonSerializer.Serialize(item), ex.Message);
                    result.Add(ex.Message);
                }

            }

            await _sendingService.SaveTableChanges(table);

            if (result.Count == 0)
                result.Add("Everything is ok");

            return new JsonResult(string.Join("\n", result));
        }


        //Old endpoint. You can use it if you want 
        [Route("file")]
        [HttpPost]
        public async Task<JsonResult> PostFileAsync(List<IFormFile> files)
        {
            List<string> result = new List<string>();

            //1. Save to tabele Json Input
            var table = await _sendingService.SaveInputJsonToTable(files);

            //2. Prepare Json Output
            await _sendingService.PrepareJsonOutputs(table);

            //3. Send Json
            foreach (var item in table)
            {
                try
                {
                    List<JsonForRestApi> jsonForRestApi = JsonSerializer.Deserialize<List<JsonForRestApi>>(item.PostJson);

                    var response = await _apiCallService.SendJson(jsonForRestApi);

                    item.PostDateTime = DateTime.Now;


                    if (response.ReturnCode.ToLower() != "success")
                        await _failureService.SaveErrrorToTable(JsonSerializer.Serialize(item), response.Message);

                }
                catch (Exception ex)
                {
                    await _failureService.SaveErrrorToTable(JsonSerializer.Serialize(item), ex.Message);
                    result.Add(ex.Message);
                }

            }

            await _sendingService.SaveTableChanges(table);

            if (result.Count == 0)
                result.Add("Everything is ok");

            return new JsonResult(string.Join("\n", result));
        }
    }
}