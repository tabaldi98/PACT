using app.Tabaldi.PACT.Service.QueuesManager.DependencyResolution;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(app.Tabaldi.PACT.Service.QueuesManager.Startup))]
namespace app.Tabaldi.PACT.Service.QueuesManager
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            UseSqlServerStorage();
            GlobalConfiguration.Configuration
                .UseAutofacActivator(AutofacConfig.Container.Value)
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings();

            app.UseHangfireDashboard();
            app.UseHangfireServer(new BackgroundJobServerOptions()
            {
                ServerName = string.Format("{0}: Queues Manager", Environment.MachineName),
                WorkerCount = Environment.ProcessorCount * 5,
                StopTimeout = TimeSpan.FromSeconds(10),
                Queues = new string[]
                {
                    CallendarRecurrenceQueueAppService.QUEUE_NAME,
                }
            });

            GlobalJobFilters.Filters.Add(
                new AutomaticRetryAttribute
                {
                    Attempts = 10,
                    DelaysInSeconds = new int[10] { 60, 300, 600, 1800, 3600, 7200, 10800, 14400, 18000, 21600 }
                });

            RecurringJob.AddOrUpdate<ICallendarRecurrenceQueueAppService>("execute-recurrence", p => p.ExecuteRecurrence(), Cron.Daily(23));
        }

        private void UseSqlServerStorage()
        {
            var connectionString = "User ID=sa;Password=P@ssw0rd;Initial Catalog=HangFireQueue;Server=.\\sql";

            GlobalConfiguration.Configuration
                .UseSqlServerStorage(connectionString,
                    new SqlServerStorageOptions
                    {
                        QueuePollInterval = TimeSpan.FromSeconds(15),
                        UsePageLocksOnDequeue = true,
                        DisableGlobalLocks = true,
                        EnableHeavyMigrations = false
                    });
        }
    }

}
