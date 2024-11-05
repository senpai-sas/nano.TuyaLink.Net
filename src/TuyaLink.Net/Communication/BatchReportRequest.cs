

using TuyaLink.Communication.History;
using TuyaLink.Functions.Events;

namespace TuyaLink.Communication
{
    internal class BatchReportRequest : FunctionRequest
    {
        public BatchReportRequestData Data { get; set; }

        public SystemParameters Sys { get; set; } = new() { Ack = false };
    }

    internal class BatchReportRequestData
    {
        public PropertyHashtable Properties { get; set; }

        public EventDataHashtable Events { get; set; }
    }
}
