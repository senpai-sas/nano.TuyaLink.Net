using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class RawProperty : DeviceProperty
    {
        public RawProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Raw)
        {
            Value = null;
        }

        public new byte[]? Value
        {
            get => (byte[]?)base.Value;
            private set => Update(value);
        }

        public static implicit operator byte[]?(RawProperty property)
        {
            return property.Value;
        }
    }
}
