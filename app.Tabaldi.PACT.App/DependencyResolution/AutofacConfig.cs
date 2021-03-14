using app.Tabaldi.PACT.Infra.Data.HttpClient.ClientAgg;
using Autofac;
using System;
using System.Reflection;

namespace app.Tabaldi.PACT.App.DependencyResolution
{
    public static class AutofacConfig
    {
        public static readonly Lazy<IContainer> Container = new Lazy<IContainer>(() =>
        {
            var builder = new ContainerBuilder();

            RegisterTypes(builder);

            return builder.Build();
        });

        private static void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(
               Assembly.GetAssembly(typeof(IClientClientRepository)))
                  .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
                  .AsImplementedInterfaces()
                  .InstancePerLifetimeScope();
        }
    }
}
