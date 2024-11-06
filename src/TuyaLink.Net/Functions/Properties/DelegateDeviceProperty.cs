using TuyaLink.Properties;

namespace TuyaLink.Functions.Properties
{
    public delegate void UpdatePropertyDelegate(object newValue, object oldValue);
    public delegate object GetPropertyDelegate();
    public class DelegateDeviceProperty : DeviceProperty
    {
        public GetPropertyDelegate Getter { get; private set; }
        public UpdatePropertyDelegate Setter { get; private set; }

        public DelegateDeviceProperty(string name, TuyaDevice device, UpdatePropertyDelegate setter) : base(name, device)
        {
            Setter = setter;
        }

        protected override void OnUpdate(object value, object oldValue)
        {
            Setter(value, oldValue);
        }

        public override object GetValue()
        {
            return base.GetValue();
        }
    }
}
