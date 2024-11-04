using TuyaLink.Properties;

namespace TuyaLink.Functions
{
    public delegate void OnUpdateAction(object newValue, object oldValue);
    public class DelegateProperty : DeviceProperty
    {
        private readonly OnUpdateAction _onUpdate;

        public DelegateProperty(string name, TuyaDevice device, OnUpdateAction onUpdate, bool aknowlage = false) : base(name, device)
        {
            _onUpdate = onUpdate;
            Acknowledge = aknowlage;
        }

        protected override void OnUpdate(object value, object oldValue)
        {
            _onUpdate(value, oldValue);
        }

    }
}
