using System;

using TuyaLink.Communication;
using TuyaLink.Functions;
using TuyaLink.Functions.Properties;
using TuyaLink.Model;

namespace TuyaLink.Properties
{
    /// <summary>
    /// Represents an abstract base class for device properties.
    /// </summary>
    /// <param name="code">The code of the property.</param>
    /// <param name="device">The device associated with the property.</param>
    /// <param name="type">The data type of the property.</param>
    public abstract class DeviceProperty(string code, TuyaDevice device, PropertyDataType type) : DeviceFunction(code, FunctionType.Property, device), IAcknowledgeable, IReportableFunction
    {
        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        protected object? Value { get; set; }

        /// <inheritdoc/>
        public bool Acknowledge { get; set; } = false;

        /// <summary>
        /// Gets the model of the property.
        /// </summary>
        public new PropertyModel? Model { get; }

        /// <summary>
        /// Gets the data type of the property.
        /// </summary>
        public PropertyDataType DataType { get; } = type;

        /// <summary>
        /// Updates the property value from the cloud.
        /// </summary>
        /// <param name="value">The new value from the cloud.</param>
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

        /// <summary>
        /// Updates the property value.
        /// </summary>
        /// <param name="value">The new value.</param>
        protected void Update(object? value)
        {
            if (!IsValidLocalValue(value))
            {
                throw new FunctionRuntimeException(StatusCode.InvalidValueError, $"The property {Code} can't take {value} as value, expected value are {DataType.ClrType}");
            }

            object? oldValue = Value;
            Update(value, oldValue);
        }

        /// <summary>
        /// Updates the property value and triggers the update event.
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        protected void Update(object? value, object? oldValue)
        {
            Value = value;
            OnUpdate(value, oldValue);
        }

        /// <summary>
        /// Called when the property value is updated.
        /// </summary>
        /// <param name="value">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        protected virtual void OnUpdate(object? value, object? oldValue) { }

        /// <summary>
        /// Gets the cloud value of the property.
        /// </summary>
        /// <returns>The cloud value.</returns>
        public virtual object? GetCloudValue()
        {
            return ParseLocalValue();
        }

        /// <summary>
        /// Reports the property value to the cloud.
        /// </summary>
        /// <returns>The response handler.</returns>
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

        /// <summary>
        /// Validates the model of the property.
        /// </summary>
        protected override void ValidateModel()
        {
            if (Model!.TypeSpec.Type != DataType)
            {
                throw new ArgumentException($"The model type must be a {DataType} type.", nameof(Model));
            }
        }

        /// <summary>
        /// Checks if the cloud value is valid.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        protected virtual bool IsValidCloudValue(object? value)
        {
            return value is null || DataType.IsValidCloudValue(value);
        }

        /// <summary>
        /// Checks if the local value is valid.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns>True if the value is valid, otherwise false.</returns>
        protected virtual bool IsValidLocalValue(object? value)
        {
            return value is null || DataType.IsValidLocalValue(value);
        }

        /// <summary>
        /// Binds the model to the property.
        /// </summary>
        /// <param name="model">The model to bind.</param>
        internal void BindModel(PropertyModel model)
        {
            base.BindModel(model);
            OnBindModel(model);
        }

        /// <summary>
        /// Called when the model is bound to the property.
        /// </summary>
        /// <param name="model">The model that was bound.</param>
        /// <remarks>
        /// Override this method to perform custom binding logic.
        /// </remarks>
        protected virtual void OnBindModel(PropertyModel model) { }

        /// <summary>
        /// Parses the cloud value to the local value.
        /// </summary>
        /// <param name="value">The cloud value.</param>
        /// <returns>The local value.</returns>
        /// <remarks>
        /// Override this method to provide custom parsing logic.
        /// </remarks>
        protected virtual object ParseCloudValue(object value)
        {
            return value;
        }

        /// <summary>
        /// Parses the local value to the cloud value.
        /// </summary>
        /// <returns>The cloud value.</returns>
        /// <remarks>
        /// Override this method to provide custom parsing logic.
        /// </remarks>
        protected virtual object? ParseLocalValue()
        {
            return Value;
        }

        /// <summary>
        /// Checks if the cloud value is valid.
        /// </summary>
        /// <param name="value">The value to check.</param>
        private void CheckCloudValue(object value)
        {
            if (!IsValidCloudValue(value))
            {
                throw new FunctionRuntimeException(StatusCode.InvalidValueError, $"The property {Code} can't take {value} as value, expected value are {DataType.RawType}");
            }
        }
    }
}
