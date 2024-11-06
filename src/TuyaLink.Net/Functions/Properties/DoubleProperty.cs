using System;

using TuyaLink.Properties;


namespace TuyaLink.Functions.Properties
{
    public class DoubleProperty : DeviceProperty
    {
        public DoubleProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Double)
        {
            Value = 0.0;
        }

        public new double Value
        {
            get => (double)(base.Value ?? 0);
            private set => Update(value);
        }

        public static implicit operator double(DoubleProperty property)
        {
            return property.Value;
        }

        public static double operator +(DoubleProperty property, double value)
        {
            return value + property.Value;
        }

        public static double operator -(DoubleProperty property, double value)
        {
            return value - property.Value;
        }

        public static double operator *(DoubleProperty property, double value)
        {
            return value * property.Value;
        }

        public static double operator /(DoubleProperty property, double value)
        {
            return value / property.Value;
        }
    }
}
