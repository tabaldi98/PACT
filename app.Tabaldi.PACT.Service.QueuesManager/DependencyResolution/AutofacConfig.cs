using Autofac;
using Hangfire;
using System;
using System.Linq;
using System.Reflection;

namespace app.Tabaldi.PACT.Service.QueuesManager.DependencyResolution
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
                    //Assembly.GetAssembly(typeof(ICallendarRecurrenceQueue)),
                    //Assembly.GetAssembly(typeof(Client)),
                    Assembly.GetAssembly(typeof(ICallendarRecurrenceQueueAppService)))
                       .Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
                       .AsImplementedInterfaces()
                       .InstancePerBackgroundJob();
        }
    }
}
