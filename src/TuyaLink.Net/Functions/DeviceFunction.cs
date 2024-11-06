using System;

using TuyaLink.Model;

namespace TuyaLink.Functions
{
    public abstract class DeviceFunction
    {
        public string Code { get; private set; }

        public FunctionType Type { get; private set; }

        protected TuyaDevice Device { get; }

        internal FunctionModel Model { get; private set; }

        public DeviceFunction(string name, FunctionType type, TuyaDevice device)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or empty.", nameof(name));
            }

            Code = name;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        protected void BindModel(FunctionModel model)
        {
            Model = model;
            CheckModel();
        }

        internal virtual void CheckModel(Action? action = null)
        {
            if (!Device.Settings.ValdiateModel)
            {
                return;
            }
            if (Model == null)
            {
                throw new TuyaLinkException($"The function {Code} has no model");
            }

            if (Model.Code != Code)
            {
                throw new TuyaLinkException($"The function {Code} has a model with code {Model.Code}");
            }

            ValidateModel();

            action?.Invoke();
        }

        protected virtual void ValidateModel()
        {

        }
    }
}
