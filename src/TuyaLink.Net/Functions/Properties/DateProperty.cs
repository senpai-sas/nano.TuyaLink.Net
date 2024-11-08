using System;


using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public class DateProperty : DeviceProperty
    {
        public static readonly DateTime Default = DateTime.FromUnixTimeSeconds(0);

        public DateProperty(string name, TuyaDevice device) : base(name, device, PropertyDataType.Date)
        {
            Value = Default;
        }

        public new DateTime Value
        {
            get => (DateTime)(base.Value ?? Default);
            private set => Update(value);
        }

        protected override object ParseCloudValue(object value)
        {
            if (value is long unixTime)
            {
                return DateTime.FromUnixTimeSeconds(unixTime);
            }

            throw new ArgumentException($"The value {value} is not a valid Unix time");
        }

        protected override bool IsValidCloudValue(object? value)
        {
            return value is null or int or long;
        }

        protected override object? ParseLocalValue()
        {
            return Value.ToUnixTimeSeconds();
        }

        public static implicit operator DateTime(DateProperty property)
        {
            return property.Value;
        }

        public static DateTime operator +(DateProperty property, TimeSpan value)
        {
            return property.Value + value;
        }

        public static TimeSpan operator -(DateProperty property, DateTime value)
        {
            return value - property.Value;
        }

        public static DateTime operator -(DateProperty property, TimeSpan value)
        {
            return property.Value - value;
        }
    }
}
