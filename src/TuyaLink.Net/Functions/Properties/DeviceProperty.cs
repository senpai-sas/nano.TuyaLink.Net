using System;

using TuyaLink.Communication;
using TuyaLink.Functions;


using TuyaLink.Model;

namespace TuyaLink.Properties
{
    public abstract class DeviceProperty(string name, TuyaDevice device) : DeviceFunction(name, FunctionType.Property, device), IAcknowledgeable, IReportableFunction
    {
        protected object Value { get; set; }

        /// <inheritdoc/>
        public bool Acknowledge { get; set; } = false;

        public new PropertyModel Model { get; private set; }

        internal virtual void CloudUpdate(object value)
        {
            CheckModel();
            if (!Model.AccessMode.CanRead)
            {
                throw new InvalidOperationException($"The property {Code} can't be updated from the cloud");
            }
            Update(value);
        }

        protected void Update(object value)
        {
            CheckModel();
            if (!IsValidValue(value))
            {
                throw new ArgumentException($"The property {Code} can't take {value} as value");
            }
            var oldValue = Value;
            Update(value, oldValue);
        }

        protected void Update(object value, object oldValue)
        {
            Value = value;
            OnUpdate(value, oldValue);
        }

        protected virtual void OnUpdate(object value, object oldValue) { }

        public virtual object GetValue()
        {
            return Value;
        }

        public ResponseHandler Report()
        {
            CheckModel();
            if (!Model.AccessMode.CanWrite)
            {
                throw new InvalidOperationException($"The property {Code} can't be reported to the cloud");
            }
            return Device.ReportProperty(this);
        }

        protected virtual bool IsValidValue(object value) => Model.TypeSpec.Type.IsValidValue(value);
        internal void BindModel(PropertyModel model)
        {
            Model = model;
            base.BindModel(model);
            OnBindModel(model);
        }

        protected virtual void OnBindModel(PropertyModel model)
        {
           
        }
    }
}
