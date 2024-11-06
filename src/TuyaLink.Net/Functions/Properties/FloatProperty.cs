using TuyaLink.Functions;
using TuyaLink.Functions.Properties;

namespace TuyaLink.Properties
{
    public class FloatProperty : DeviceProperty
    {


        public FloatProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Float)
        {
            Value = 0f;
        }

        public new float Value
        {
            get => (float)(base.Value ?? 0);
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
