using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

<<<<<<< HEAD:Api/Middlewares/Extensions.cs
namespace Api.Middlewares
=======
namespace Infrastructure.Frameworks
>>>>>>> master:Infrastructure/Frameworks/Extensions.cs
{
    public static class Extensions
    {
        public static IApplicationBuilder ConfigureExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(ExceptionHandlerMiddleware));
        }
        public static IApplicationBuilder ConfigureResponseTime(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(ResponseTimeLoggerMiddleware));
        }
<<<<<<< HEAD:Api/Middlewares/Extensions.cs
        public static IApplicationBuilder ConfigureBuffer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(BufferMiddleware));
=======
        public static IApplicationBuilder ConfigureRequest(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(ConfigureRequestMiddleware));
>>>>>>> master:Infrastructure/Frameworks/Extensions.cs
        }
    }
}
