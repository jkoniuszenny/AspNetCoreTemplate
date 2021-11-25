using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public async Task<string> GetAsync() => await Task.FromResult($"I'm alive - Api _____ - {GetType().Assembly.GetName().Version}");
    }
}