using System;

using nano.SmartEnum;

using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public abstract class EnumProperty : DeviceProperty
    {
        public EnumProperty(string name, TuyaDevice device, Type smartEnumType) : base(name, device, PropertyDataType.Enum)
        {
            Value = GetDefaultValue();
        }

        protected abstract PropertySmartEnum GetDefaultValue();

        protected override object ParseCloudValue(object value)
        {
            return ParseEnumValue(value);
        }

        protected abstract SmartEnum ParseEnumValue(object value);

        protected override object? ParseLocalValue()
        {
            return Value.EnumValue;
        }

        public new PropertySmartEnum Value
        {
            get => (PropertySmartEnum)(base.Value ?? 0);
            private set => Update(value);
        }
    }

    public abstract class PropertySmartEnum : SmartEnum
    {
        protected PropertySmartEnum(string name, string value) : base(name, value)
        {

        }
    }
}
