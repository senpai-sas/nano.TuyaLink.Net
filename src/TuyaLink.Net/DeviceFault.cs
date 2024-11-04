using System;

namespace TuyaLink
{
    public readonly struct DeviceFault(string value)
    {
        public string Value { get; } = value ?? throw new ArgumentNullException(nameof(value));

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator DeviceFault(string value)
        {
            return new DeviceFault(value);
        }

        public static DeviceFault FromValue(string value)
        {
            return new DeviceFault(value);
        }
    }
}
