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

        internal void CheckModel()
        {
            if (Model == null)
            {
                throw new TuyaLinkException($"The function {Code} has no model");
            }
        }
    }
}
