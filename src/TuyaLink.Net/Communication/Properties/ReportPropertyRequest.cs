using System.Collections;

using TuyaLink.Communication.History;

namespace TuyaLink.Communication.Properties
{
    internal class ReportPropertyRequest : FunctionRequest
    {
        public PropertyHashtable Data { get; set; }

        public SystemParameters Sys { get; set; } = new() { Ack = false };
    }

    public class PropertyValue
    {
        public long Time { get; set; }

        public object Value { get; set; }
    }
}
