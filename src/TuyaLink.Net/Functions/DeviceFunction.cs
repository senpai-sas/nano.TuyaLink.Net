using System;

using TuyaLink.Model;

namespace TuyaLink.Functions
{
    public abstract class DeviceFunction(string name, FunctionType type, TuyaDevice device)
    {
        public string Code { get; private set; } = name;

        public FunctionType Type { get; private set; } = type;

        protected TuyaDevice Device { get; } = device;

        internal FunctionModel Model { get; private set; }

        internal void BindModel(FunctionModel model)
        {
            Model = model;
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

            action?.Invoke();
        }
    }
}
