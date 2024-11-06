using System;


using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class DateProperty : DeviceProperty
    {
        public DateProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Date)
        {
            Value = DateTime.MinValue;
        }

        public new TuyaDateTime Value
        {
            get => (long)(base.Value ?? 0);
            private set => Update(value);
        }

        public static implicit operator DateTime(DateProperty property)
        {
            return property.Value;
        }

        public static DateTime operator +(DateProperty property, TimeSpan value)
        {
            return property.Value.Value + value;
        }

        public static TimeSpan operator -(DateProperty property, DateTime value)
        {
            return value - property.Value;
        }

        public static DateTime operator -(DateProperty property, TimeSpan value)
        {
            return property.Value.Value - value;
        }
    }
}
