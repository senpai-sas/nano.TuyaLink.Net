using System;

namespace TuyaLink.Properties
{
    internal class ValueProperty(string name, TuyaDevice device) : DeviceProperty(name,device)
    {
    }

    public class ValuePropertySettings
    {

        public ValueRange Range { get; set; }

        /// <summary>
        /// The pitch specifies the difference between adjacent values.
        /// <example>
        /// For example, if the pitch is 1, the valid values will be 0, 1, 2… If the pitch is 3, the valid values will be 0, 3, 6…
        /// </example>
        /// </summary>

        public float Picth { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Unit { get; set; }
        public float Scale { get; set; }

        public string FormatValue(float value)
        {
            return string.Format("{0}{1}", value * Scale, Unit);
        }

        public float ParseValue(string value)
        {
            return float.Parse(value.Replace(Unit, "")) / Scale;
        }

        public float GetStepValue(float value)
        {
            return (float)Math.Round(value / Picth) * Picth;
        }

        public float GetUnitValue(float value)
        {
            return value * Scale;
        }

        public float GetScaleValue(float value)
        {
            return value / Scale;
        }

        public float GetRangeValue(float value)
        {
            return Math.Min(Range.Max, Math.Max(Range.Min, value));
        }

        public float GetClampedValue(float value)
        {
            return GetRangeValue(GetUnitValue(GetStepValue(value)));
        }

        public float GetClampedValue(string value)
        {
            return GetClampedValue(ParseValue(value));
        }

        public bool IsValidValue(float value)
        {
            return value >= Range.Min && value <= Range.Max;
        }
    }

    public struct ValueRange
    {
        public float Min { get; set; }
        public float Max { get; set; }

        public static readonly ValueRange WhideRange = new() { Min = float.MinValue, Max = float.MaxValue };
    }
}
