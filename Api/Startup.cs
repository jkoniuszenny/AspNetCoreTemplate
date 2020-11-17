using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using NLog.Extensions.Logging;
using NLog.Config;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Database;
using System.IO;
using Microsoft.Extensions.Logging;
using Infrastructure.Settings;
using Api.Extensions;
using Core.NLog;
using Infrastructure.IoC;
using Core.NLog.Interfaces;

namespace Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddRouting();

            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
                o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddControllers();

            services.AddDbContext<DatabaseContext>();

            var builder = new ContainerBuilder();

            builder.Populate(services);
            builder.RegisterModule(new ContainerModule(Configuration));
            builder.RegisterType<NLogLogger>()
                                .As<INLogLogger>()
                                .SingleInstance();
            builder.RegisterType<NLogTimeLogger>()
                    .As<INLogTimeLogger>()
                    .SingleInstance();
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            string nlogFilePath = "nlog" + env.EnvironmentName + ".config";

            if (File.Exists(nlogFilePath))
            {
                loggerFactory.AddNLog();
                loggerFactory.ConfigureNLog(nlogFilePath);
                LogManager.Configuration.Install(new InstallationContext());
            }


            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.ConfigureExceptionHandler();

            var responseTimeSettings = app.ApplicationServices.GetService<ResponseTimeSettings>();
            if (responseTimeSettings.Enabled)
            {
                app.ConfigureResponseTime();

            }
            app.UseRouting();

            app.Use(async (context, next) => {
                context.Request.EnableBuffering();
                await next().ConfigureAwait(false);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
