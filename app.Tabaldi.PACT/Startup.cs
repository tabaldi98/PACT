using app.Tabaldi.PACT.Api.Extensions;
using app.Tabaldi.PACT.Application.AttendanceRecurrenceAgg;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace app.Tabaldi.PACT.Api
{
    public class Startup
    {
        private Lazy<IJwtOptions> JwtOptions { get; }

        public Startup(IConfiguration configuration)
        {
            JwtOptions = new Lazy<IJwtOptions>(() => new JwtOptions());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersConfigure();

            services.AddDependencies();

            services.AddSwagger();

            services.AddHangFire();

            services.AddJwtBearerAuthentication(JwtOptions.Value);

            services.AddAuthorization(p =>
            {
                p.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseHangfireDashboard();

            app.AddRecurringJobs();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            app.UseSwaggerMiddleware();
        }
    }
}
