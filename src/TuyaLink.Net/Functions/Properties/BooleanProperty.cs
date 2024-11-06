using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class BooleanProperty : DeviceProperty
    {
        public BooleanProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Boolean)
        {
            Value = false;
        }

        public new bool Value
        {
            get => (bool)(base.Value ?? false);
            private set => Update(value);
        }

        public static implicit operator bool(BooleanProperty property)
        {
            return property.Value;
        }

        public static bool operator +(BooleanProperty property, bool value)
        {
            return value || property.Value;
        }

        public static bool operator -(BooleanProperty property, bool value)
        {
            return value && !property.Value;
        }

        public static bool operator *(BooleanProperty property, bool value)
        {
            return value && property.Value;
        }

        public static bool operator /(BooleanProperty property, bool value)
        {
            return value || !property.Value;
        }
    }
}
