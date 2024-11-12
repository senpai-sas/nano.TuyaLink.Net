using System;
using System.Diagnostics;

namespace TuyaLink
{
    public readonly struct TuyaDateTime : IComparable
    {
        // ticks value corresponding to 3000/12/31:23:59:59.999 (nanoFramework maximum date time)
        private const long MaxTicks = 946708127999999999;

        // Number of 100ns ticks per time unit
        private const long TicksPerMillisecond = 10000;
        internal const long TicksPerSecond = TicksPerMillisecond * 1000;
        private const long TicksPerMinute = TicksPerSecond * 60;
        private const long TicksPerHour = TicksPerMinute * 60;
        private const long TicksPerDay = TicksPerHour * 24;

        // Number of milliseconds per time unit
        private const int MillisPerSecond = 1000;
        private const int MillisPerMinute = MillisPerSecond * 60;
        private const int MillisPerHour = MillisPerMinute * 60;
        private const int MillisPerDay = MillisPerHour * 24;

        // Unix Epoch constants
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const long UnixEpochTicks = (TicksPerDay * 719162); // 621355968000000000
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const long UnixEpochSeconds = UnixEpochTicks / TicksPerSecond; // 62135596800

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const long UnixEpochMilliseconds = UnixEpochTicks / TicksPerMillisecond; // 62135596800000

        public DateTime Value { get; }

        public TuyaDateTime(DateTime dateTime) => Value = dateTime;

        public override string ToString()
        {
            return Value.ToString("yyyy-MM-ddTHH:mm:ss:fff");
        }

        public static implicit operator TuyaDateTime(DateTime dateTime)
        {
            return new TuyaDateTime(dateTime);
        }

        //public static implicit operator DateTime(TuyaDateTime dateTime)
        //{
        //    return dateTime.Value;
        //}

        public static implicit operator TuyaDateTime(long time)
        {
            return FromUnixTime(time);
        }



        public static TuyaDateTime FromUnixTime(long time)
        {
            int lengt = time.ToString().Length;

         
            if (lengt == 10)
            {
                return new TuyaDateTime(DateTime.FromUnixTimeSeconds(time));
            }

            const long MaxMilliseconds = (MaxTicks / TicksPerMillisecond) - UnixEpochMilliseconds;

            if (time < 0 || time > MaxMilliseconds)
            {
                throw new ArgumentOutOfRangeException(nameof(time));
            }

            long ticks = (time * TicksPerMillisecond) + UnixEpochTicks;
            return new DateTime(ticks);
        }

        public long ToUnixTimeMilliseconds()
        {
            // Truncate sub - second precision before offsetting by the Unix Epoch to avoid
            // the last digit being off by one for dates that result in negative Unix times.
            //
            // For example, consider the DateTime 12/31/1969 12:59:59.001 +0
            //   ticks            = 621355967990010000
            //   ticksFromEpoch   = ticks - UnixEpochTicks                   = -9990000
            //   millisecondsFromEpoch = ticksFromEpoch / UnixEpochMilliseconds = 0
            //
            // Notice that millisecondsFromEpoch is rounded *up* by the truncation induced by integer division,
            // whereas we actually always want to round *down* when converting to Unix time. This happens
            // automatically for positive Unix time values. Now the example becomes:
            //   milliseconds          = ticks / TicksPerSecond = 62135596800000
            //   millisecondsFromEpoch = milliseconds - TicksPerMillisecond      = -1
            //
            // In other words, we want to consistently round toward the time 1/1/0001 00:00:00,
            // rather than toward the Unix Epoch (1/1/1970 00:00:00).
            long seconds = Value.Ticks / TicksPerMillisecond;
            return seconds - UnixEpochMilliseconds;
        }

        public int CompareTo(object obj)
        {
            if (obj is TuyaDateTime other)
            {
                return Value.CompareTo(other.Value);
            }
            if (obj is DateTime otherDateTime)
            {
                return Value.CompareTo(otherDateTime);
            }
            return -1;
        }

        public override bool Equals(object obj)
        {
            if (obj is TuyaDateTime time)
            {
                return Value == time.Value;
            }

            if (obj is DateTime dateTime)
            {
                return Value == dateTime;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}
