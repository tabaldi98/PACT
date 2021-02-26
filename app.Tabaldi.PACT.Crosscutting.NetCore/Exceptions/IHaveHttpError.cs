using System.Collections.Generic;
using System.Net;

namespace app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions
{
    public interface IHaveHttpError
    {
        Dictionary<string, object> HttpErrorList { get; }
        HttpStatusCode HttpStatusCode { get; }
    }
}
