using app.Tabaldi.PACT.Application.AttendanceAgg;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace app.Tabaldi.PACT.Api.Extensions
{
    public static class HangFireExtensions
    {
        public static void AddHangFire(this IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseActivator(new HangfireActivator(services.BuildServiceProvider()))
                .UseSqlServerStorage("User ID=sa;Password=P@ssw0rd;Initial Catalog=HangFireQueue;Server=.\\sql", new SqlServerStorageOptions()
                {
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true,
                    EnableHeavyMigrations = false,
                }));

            services.AddHangfireServer(p=>
            {
                p.ServerName = string.Format("{0}: Queues Manager", Environment.MachineName);
                p.WorkerCount = Environment.ProcessorCount * 5;
                p.StopTimeout = TimeSpan.FromSeconds(10);
            });
        }

        public static void AddRecurringJobs(this IApplicationBuilder app)
        {
            RecurringJob.AddOrUpdate<IAttendanceAppService>("execute-recurrence", p => p.ExecuteQueueAsync(), Cron.Daily(23));
        }
    }

    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}
