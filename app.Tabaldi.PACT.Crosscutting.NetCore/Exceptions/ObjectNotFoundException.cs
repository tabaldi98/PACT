using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace app.Tabaldi.PACT.Crosscutting.NetCore.Exceptions
{
    [Serializable]
    public class ObjectNotFoundException<T> : Exception, IHaveHttpError
    {
        public const string EXCEPTION_MESSAGE = "NotFoundException";

        public Dictionary<string, object> HttpErrorList { get; private set; }
        public HttpStatusCode HttpStatusCode => HttpStatusCode.NotFound;

        public string ObjectTypeName { get; private set; }

        public ObjectNotFoundException()
            : base(EXCEPTION_MESSAGE)
        {
            ObjectTypeName = typeof(T).Name;

            HttpErrorList = new Dictionary<string, object>()
            {
                {nameof(Message), Message },
                {nameof(ObjectTypeName), ObjectTypeName }
            };
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ObjectNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ObjectTypeName = info.GetString(nameof(ObjectTypeName));
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(ObjectTypeName), ObjectTypeName);

            base.GetObjectData(info, context);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, {nameof(ObjectTypeName)}: {ObjectTypeName}";
        }
    }
}
