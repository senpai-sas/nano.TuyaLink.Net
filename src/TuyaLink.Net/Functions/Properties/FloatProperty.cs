using TuyaLink.Functions;

namespace TuyaLink.Properties
{
    public class FloatProperty(string name, AccessMode transferType, TuyaDevice device) : DeviceProperty(name, device)
    {
        public new float Value
        {
            get => (float)base.Value;
            private set => Update(value);
        }

        public static implicit operator float(FloatProperty property)
        {
            return property.Value;
        }

        public static float operator +(FloatProperty property, float value)
        {
            return value + property.Value;
        }

        public static float operator -(FloatProperty property, float value)
        {
            return value - property.Value;
        }

        public static float operator *(FloatProperty property, float value)
        {
            return value * property.Value;
        }

        public static float operator /(FloatProperty property, float value)
        {
            return value / property.Value;
        }

    }

}
