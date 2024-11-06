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
            Update(value);
        }

        protected void Update(object? value)
        {
            CheckModel(() =>
            {
                if (!IsValidValue(value))
                {
                    throw new FunctionRuntimeException(StatusCode.InvalidValueError, $"The property {Code} can't take {value} as value, expected value are {Model?.TypeSpec?.Type}");
                }
            });

            var oldValue = Value;
            Update(value, oldValue);
        }

        protected void Update(object? value, object? oldValue)
        {
            Value = value;
            OnUpdate(value, oldValue);
        }

        protected virtual void OnUpdate(object? value, object? oldValue) { }

        public virtual object? GetValue()
        {
            return Value;
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

        protected virtual bool IsValidValue(object? value) => value is null || Model!.TypeSpec.Type.IsValidValue(value);

        internal void BindModel(PropertyModel model)
        {
            base.BindModel(model);
            OnBindModel(model);
        }

        protected virtual void OnBindModel(PropertyModel model)
        {

        }
    }
}
