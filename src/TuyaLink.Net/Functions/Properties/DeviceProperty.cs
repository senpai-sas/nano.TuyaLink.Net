using System;

using TuyaLink.Communication;
using TuyaLink.Functions;
using TuyaLink.Functions.Properties;
using TuyaLink.Model;

namespace TuyaLink.Properties
{
    public abstract class DeviceProperty(string name, TuyaDevice device, PropertyDataType type) : DeviceFunction(name, FunctionType.Property, device), IAcknowledgeable, IReportableFunction
    {
        protected object? Value { get; set; }

        /// <inheritdoc/>
        public bool Acknowledge { get; set; } = false;

        public new PropertyModel? Model { get; }

        public PropertyDataType DataType { get; } = type;

        internal virtual void CloudUpdate(object value)
        {
            CheckModel(() =>
            {
                if (!Model!.AccessMode.CanSend)
                {
                    throw new InvalidOperationException($"The property {Code} can't be reported to the cloud");
                }
            });

            CheckCloudValue(value);
            Update(ParseCloudValue(value));
        }

        protected void Update(object? value)
        {
            if (!IsValidLocalValue(value))
            {
                throw new FunctionRuntimeException(StatusCode.InvalidValueError, $"The property {Code} can't take {value} as value, expected value are {DataType.ClrType}");
            }

            object? oldValue = Value;
            Update(value, oldValue);
        }

        protected void Update(object? value, object? oldValue)
        {
            Value = value;
            OnUpdate(value, oldValue);
        }

        protected virtual void OnUpdate(object? value, object? oldValue) { }

        public virtual object? GetCloudValue()
        {
            return ParseLocalValue();
        }

        public ResponseHandler Report()
        {
            CheckModel(() =>
            {
                if (!Model!.AccessMode.CanReport)
                {
                    throw new InvalidOperationException($"The property {Code} can't be reported to the cloud");
                }
            });

            return Device.ReportProperty(this);
        }


        protected override void ValidateModel()
        {
            if (Model!.TypeSpec.Type != DataType)
            {
                throw new ArgumentException($"The model type must be a {DataType} type.", nameof(Model));
            }

        }

        protected virtual bool IsValidCloudValue(object? value)
        {
            return value is null || DataType.IsValidCloudValue(value);
        }

        protected virtual bool IsValidLocalValue(object? value)
        {
            return value is null || DataType.IsValidLocalValue(value);
        }

        internal void BindModel(PropertyModel model)
        {
            base.BindModel(model);
            OnBindModel(model);
        }

        protected virtual void OnBindModel(PropertyModel model)
        {

        }

        /// <summary>
        /// Parse the could value to the local value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual object ParseCloudValue(object value)
        {
            return value;
        }

        /// <summary>
        /// Parse the local value to the cloud value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual object? ParseLocalValue()
        {
            return Value;
        }

        private void CheckCloudValue(object value)
        {
            if (!IsValidCloudValue(value))
            {
                throw new FunctionRuntimeException(StatusCode.InvalidValueError, $"The property {Code} can't take {value} as value, expected value are {DataType.RawType}");
            }
        }
    }
}
