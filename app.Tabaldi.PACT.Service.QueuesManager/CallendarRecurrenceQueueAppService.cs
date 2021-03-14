using Hangfire;
using System;
using System.IO;

namespace app.Tabaldi.PACT.Service.QueuesManager
{
    public interface ICallendarRecurrenceQueueAppService
    {
        [Queue(CallendarRecurrenceQueueAppService.QUEUE_NAME)]
        void ExecuteRecurrence();
    }

    public class CallendarRecurrenceQueueAppService : ICallendarRecurrenceQueueAppService
    {
        public const string QUEUE_NAME = "recurrencequeue";

        public void ExecuteRecurrence()
        {
            File.Create("Log\\" + Guid.NewGuid().ToString() + ".txt");
        }
    }
}
