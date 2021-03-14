using Microsoft.Owin;
using System;
using Topshelf;

namespace app.Tabaldi.PACT.Service.QueuesManager
{
    static class Program
    {
        static void Main()
        {
            var exit = HostFactory.Run(config =>
            {
                config.Service<QueuesControl>(service =>
                {
                    service.ConstructUsing(() => new QueuesControl());
                    service.WhenStarted(p => p.Start());
                    service.WhenStopped(p => p.Stop());
                });

                config.SetServiceName("app.Tabaldi.PACT.Service.QueuesManager");
                config.SetDisplayName("APP Service QueuesManager");
                config.SetDescription("Gerenciamento de filas");

                config.RunAsLocalSystem();
                config.StartAutomatically();

                config.EnableServiceRecovery(r =>
                {
                    var oneMinute = 1;
                    var oneDay = 1;

                    r.OnCrashOnly();
                    r.RestartService(oneMinute);
                    r.SetResetPeriod(oneDay);
                });
            });

            Environment.ExitCode = exit.GetHashCode();
        }
    }

}
