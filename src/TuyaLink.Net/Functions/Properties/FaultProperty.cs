
using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class FaultProperty : DeviceProperty
    {
        public FaultProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Fault)
        {
            Value = string.Empty;
        }

        public new DeviceFault Value
        {
            get => DeviceFault.FromValue(base.Value?.ToString() ?? string.Empty);
            private set => Update(value);
        }

        public static implicit operator DeviceFault(FaultProperty property)
        {
            return property.Value;
        }
    }
}
