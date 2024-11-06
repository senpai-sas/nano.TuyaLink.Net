using System;
using TuyaLink.Functions.Properties;
using TuyaLink.Model;

namespace TuyaLink.Properties
{
    public class ValueProperty(string name, TuyaDevice device) : DeviceProperty(name, device, PropertyDataType.Value)
    {
        public ValuePropertySettings Settings { get; set; } = ValuePropertySettings.DefaultSettings;

        public new double Value
        {
            get => (double)(base.Value ?? 0);
            private set => Update(value);
        }

        public static implicit operator double(ValueProperty property)
        {
            return property.Value;
        }

        public static double operator +(ValueProperty property, double value)
        {
            return value + property.Value;
        }

        public static double operator -(ValueProperty property, double value)
        {
            return value - property.Value;
        }

        public static double operator *(ValueProperty property, double value)
        {
            return value * property.Value;
        }

        public static double operator /(ValueProperty property, double value)
        {
            return value / property.Value;
        }

        public void SetValue(double value)
        {
            value = Settings.ClampValue(value);
            value = Settings.RoundValue(value);
            value = Settings.StepValue(value);
            Update(value);
        }

        public void SetValue(string value)
        {
            var parsedValue = Settings.ParseValue(value);
            SetValue(parsedValue);
        }

        public string GetValueString()
        {
            return Settings.FormatValue(Value);
        }

        public double NormalizeValue()
        {
            return Settings.NormalizeValue(Value);
        }

        public void NormalizeValue(double value)
        {
            value = Settings.DenormalizeValue(value);
            SetValue(value);
        }

        public void BindSettings(ValuePropertySettings settings)
        {
            Settings = settings;
        }

        public void BindSettings(TypeSpecifications specifications)
        {
            Settings = ValuePropertySettings.FromTypeSpecification(specifications);
        }
    }

    public class ValuePropertySettings
    {

        public ValueRange Range { get; set; }

        public string Unit { get; set; } = string.Empty;

        public float Scale { get; set; }

        public float Step { get; private set; }


        public string FormatValue(double value)
        {
            return string.Format("{0}{1}", value * Scale, Unit);
        }

        public double ParseValue(string value)
        {
            return double.Parse(value.Replace(Unit, "")) / Scale;
        }

        public double ScaleValue(double value)
        {
            return value * Scale;
        }

        public double UnscaledValue(double value)
        {
            return value / Scale;
        }

        public double RoundValue(double value)
        {
            return Math.Round(value / Step) * Step;
        }

        public double ClampValue(double value)
        {
            return Math.Clamp(value, Range.Min, Range.Max);
        }

        public double NormalizeValue(double value)
        {
            return (value - Range.Min) / (Range.Max - Range.Min);
        }

        public double DenormalizeValue(double value)
        {
            return value * (Range.Max - Range.Min) + Range.Min;
        }

        public double StepValue(double value)
        {
            return Math.Round(value / Step) * Step;
        }



        public static ValuePropertySettings DefaultSettings => new()
        {
            Range = ValueRange.WhideRange,
            Unit = "",
            Scale = 1,
            Step = 1,
        };


        public static ValuePropertySettings FromTypeSpecification(TypeSpecifications specifications)
        {
            if (specifications.Type != PropertyDataType.Value)
            {
                throw new ArgumentException("The type specification must be a Value type.", nameof(specifications));
            }

            return new ValuePropertySettings
            {
                Range = new ValueRange
                {
                    Min = specifications.Min,
                    Max = specifications.Max
                },
                Unit = specifications.Unit,
                Scale = specifications.Scale,
                Step = specifications.Step,
            };
        }
        public struct ValueRange
        {
            public double Min { get; set; }
            public double Max { get; set; }

            public static readonly ValueRange WhideRange = new() { Min = double.MinValue, Max = double.MaxValue };

            public bool InRange(double value)
            {
                return value >= Min && value <= Max;
            }
        }
    }
}
