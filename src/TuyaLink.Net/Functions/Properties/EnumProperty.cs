using System;

using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class EnumProperty : DeviceProperty
    {
        public EnumProperty(string name, TuyaDevice device, Type smartEnumType) : base(name, device, PropertyDataType.Enum)
        {
            Value = 0;
        }

        public new int Value
        {
            get => (int)(base.Value ?? 0);
            private set => Update(value);
        }

        public static implicit operator int(EnumProperty property)
        {
            return property.Value;
        }

        public static int operator +(EnumProperty property, int value)
        {
            return value + property.Value;
        }

        public static int operator -(EnumProperty property, int value)
        {
            return value - property.Value;
        }

        public static int operator *(EnumProperty property, int value)
        {
            return value * property.Value;
        }

        public static int operator /(EnumProperty property, int value)
        {
            return value / property.Value;
        }
    }
}
