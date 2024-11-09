using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class StringProperty : DeviceProperty
    {
        public StringProperty(string code, TuyaDevice device) : base(code, device, PropertyDataType.String)
        {
            Value = string.Empty;
        }

        public new string Value
        {
            get => (string)(base.Value ?? string.Empty);
            private set => Update(value);
        }

        public static implicit operator string(StringProperty property)
        {
            return property.Value;
        }

        public static string operator +(StringProperty property, string value)
        {
            return value + property.Value;
        }
    }
}
