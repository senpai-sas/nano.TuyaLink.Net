using System;

namespace TuyaLink
{
    public readonly struct TuyaDateTime : IComparable
    {
        public DateTime Value { get; }

        public TuyaDateTime(DateTime dateTime) => Value = dateTime;

        public TuyaDateTime(long deconds) => Value = DateTime.FromUnixTimeSeconds(deconds);

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-ddTHH:mm:ss");
        }

        public static implicit operator TuyaDateTime(DateTime dateTime)
        {
            return new TuyaDateTime(dateTime);
        }

        public static implicit operator DateTime(TuyaDateTime dateTime)
        {
            return dateTime.Value;
        }

        public static implicit operator TuyaDateTime(long seconds)
        {
            return new TuyaDateTime(seconds);
        }

        public static TuyaDateTime FromUnixTimeSeconds(long seconds)
        {
            return new TuyaDateTime(seconds);
        }

        public int CompareTo(object obj)
        {
            if (obj is TuyaDateTime other)
            {
                return Value.CompareTo(other.Value);
            }
            return -1;
        }

        public override bool Equals(object obj)
        {
            return obj is TuyaDateTime time &&
                   Value == time.Value;
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}
