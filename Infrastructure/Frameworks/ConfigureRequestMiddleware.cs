using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Frameworks
{
    public class ConfigureRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public ConfigureRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await HandleRequestModificationAsync(context);

            await _next(context);
        }

        private async Task HandleRequestModificationAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            await Task.CompletedTask;
        }
    }
}
