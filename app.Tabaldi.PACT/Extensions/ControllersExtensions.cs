using app.Tabaldi.PACT.Application.ClientsModule.Commands;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace app.Tabaldi.PACT.Api.Extensions
{
    public static class ControllersExtensions
    {
        public static void AddControllersConfigure(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddFluentValidation(p =>
                {
                    p.RegisterValidatorsFromAssemblyContaining(typeof(ClientAddCommandValidator));
                });
        }
    }
}
