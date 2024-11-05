using System.Collections;

using nano.SmartEnum;

namespace TuyaLink.Communication.Model
{
    internal class GetDeviceModelRequest : FunctionRequest
    {
        public GetDeviceModelData Data { get; internal set; } = new GetDeviceModelData();
    }

    public class GetDeviceModelData
    {
        public DeviceModelDataFormat Format { get; set; }
    }

    public class DeviceModelDataFormat : SmartEnum
    {
        private static readonly Hashtable _store = [];

        private DeviceModelDataFormat(string name, object value) : base(name, value)
        {
        }

        public new string EnumValue => (string)base.EnumValue;

        public static readonly DeviceModelDataFormat Simple = new("Simple", "simple");
        public static readonly DeviceModelDataFormat Complete = new("Complete", "complete");

        public static DeviceModelDataFormat FromValue(object value)
        {
            return (DeviceModelDataFormat)GetFromValue(value, typeof(DeviceModelDataFormat), _store);
        }
    }
}
