namespace TuyaLink.Properties
{
    public delegate void UpdatePropertyDelegate(object newValue, object oldValue);
    public delegate object GetPropertyDelegate();
    public class DelegateDeviceProperty(string name, TuyaDevice device, GetPropertyDelegate getter, UpdatePropertyDelegate setter) : DeviceProperty(name, device)
    {
        public GetPropertyDelegate Getter { get; private set; } = getter;
        public UpdatePropertyDelegate Setter { get; private set; } = setter;
    }
}
