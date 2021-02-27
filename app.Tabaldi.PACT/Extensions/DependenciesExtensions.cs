using app.Tabaldi.PACT.Application.ClientsModule;
using app.Tabaldi.PACT.Domain.ClientsModule.ClientAgg;
using app.Tabaldi.PACT.Domain.Seedwork.Contracts.UnitOfWork;
using app.Tabaldi.PACT.Infra.Data.Context;
using app.Tabaldi.PACT.Infra.Data.Repositories.ClientsAgg;
using app.Tabaldi.PACT.Infra.Data.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Api.Extensions
{
    public static class DependenciesExtensions
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            var interfaces = new List<Type>();
            var implementations = new List<Type>();

            foreach (var assembly in GetAssemblies())
            {
                interfaces.AddRange(assembly.ExportedTypes.Where(p => p.IsInterface && PredicateRepository(p)));
                implementations.AddRange(assembly.ExportedTypes.Where(p => !p.IsInterface && !p.IsAbstract && PredicateRepository(p)));
            }

            foreach (var @interface in interfaces)
            {
                var implementation = implementations.FirstOrDefault(p => @interface.IsAssignableFrom(p) && $"I{p.Name}" == @interface.Name);

                if (implementation == null) { continue; }

                services.AddScoped(@interface, implementation);
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDatabaseContext>((serviceProvider) =>
            {
                return new DatabaseContext("User ID=sa;Password=P@ssw0rd;Initial Catalog=FisioPACTApp;Server=ndd-serv-tab01\\sql", true);
            });
        }

        private static bool PredicateRepository(Type p) => p.Namespace.StartsWith("app") && (p.FullName.EndsWith("Repository") || p.FullName.EndsWith("Service"));

        private static Assembly[] GetAssemblies()
        {
            var applicationAssembly = Assembly.GetAssembly(typeof(IClientAppService));
            var domainAssembly = Assembly.GetAssembly(typeof(IClientRepository));
            var dataAssembly = Assembly.GetAssembly(typeof(ClientRepository));

            return new[] { applicationAssembly, domainAssembly, dataAssembly };
        }
    }
}
