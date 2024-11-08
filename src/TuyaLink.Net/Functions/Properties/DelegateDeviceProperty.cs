using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public delegate void UpdatePropertyDelegate(object? newValue, object? oldValue);

    public class DelegateDeviceProperty : DeviceProperty
    {
        public UpdatePropertyDelegate Setter { get; private set; }

        public DelegateDeviceProperty(string code, TuyaDevice device, PropertyDataType dataType, UpdatePropertyDelegate setter, bool acknowledge = true) : base(code, device, dataType)
        {
            Acknowledge = acknowledge;
            Setter = setter ?? throw new System.ArgumentNullException(nameof(setter));
        }

        protected override void OnUpdate(object? value, object? oldValue)
        {
            Setter(value, oldValue);
        }
    }
}
