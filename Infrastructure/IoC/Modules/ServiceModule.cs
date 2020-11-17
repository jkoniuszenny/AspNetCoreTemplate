using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Infrastructure.Services.Interfaces;

namespace Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                   .Where(x => x.IsAssignableTo<IService>())
                   .AsImplementedInterfaces()
                   .InstancePerDependency();
        }
    }
}
