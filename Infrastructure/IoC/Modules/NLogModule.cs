using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Core.NLog;
using Core.NLog.Interfaces;
using Core.Repositories.Interfaces;

namespace Infrastructure.IoC.Modules
{
    public class NLogModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NLogLogger>()
                    .As<INLogLogger>()
                    .SingleInstance();

            builder.RegisterType<NLogTimeLogger>()
                    .As<INLogTimeLogger>()
                    .SingleInstance();
        }
    }
}
