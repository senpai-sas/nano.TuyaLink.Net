using System;

using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class StringProperty : DeviceProperty
    {
        public StringProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.String)
        {
            Value = "";
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

        public static string operator -(StringProperty property, string value)
        {
            return value.Replace(property.Value, "");
        }

        public static string operator *(StringProperty property, string value)
        {
            return value.Replace(property.Value, "");
        }

        public static string operator /(StringProperty property, string value)
        {
            return value.Replace(property.Value, "");
        }
    }
}
