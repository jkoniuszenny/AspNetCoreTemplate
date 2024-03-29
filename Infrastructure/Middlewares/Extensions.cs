﻿using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Middlewares
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
        public static IApplicationBuilder ConfigureBuffer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware(typeof(BufferMiddleware));
        }
    }
}
