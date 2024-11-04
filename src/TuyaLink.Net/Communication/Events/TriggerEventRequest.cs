using System.Collections;

using TuyaLink.Functions.Events;

namespace TuyaLink.Communication.Events
{
    internal class TriggerEventRequest : FunctionRequest
    {
        public TriggerEventData Data { get; set; }

        public SystemParameters Sys { get; set; } = new() { Ack = false };
    }

    public class TriggerEventData : EventData
    {
        public string EventCode { get; set; }
    }
}
