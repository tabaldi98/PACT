using System;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient
{
    public abstract class GenericRepositoryBase : IDisposable
    {
        protected IHttpClientBase HttpClient;

        protected GenericRepositoryBase()
        {
            HttpClient = new HttpClientBase();
        }

        public void Dispose()
        {
            //
        }
    }
}
