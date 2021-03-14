using Microsoft.Owin.Hosting;
using System;

namespace app.Tabaldi.PACT.Service.QueuesManager
{
    public class QueuesControl
    {
        private const string Endpoint = "http://localhost:8686";
        private IDisposable _host;

        public void Start()
        {
            _host = WebApp.Start<Startup>(Endpoint);
        }

        public void Stop()
        {
            _host.Dispose();
        }
    }
}
