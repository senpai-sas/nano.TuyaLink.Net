using System;
using System.Collections;

using TuyaLink.Events;

namespace TuyaLink.Functions.Events
{

    public delegate void DeviceEventDelegate(Hashtable parameters, DateTime time);
    internal class DelegateDeviceEvent(string code, DeviceEventDelegate action, TuyaDevice device, bool acknowledge)
        : DeviceEvent(code, device, acknowledge)
    {
        private readonly DeviceEventDelegate _action = action ?? throw new ArgumentNullException(nameof(action));

        protected override void OnTriggering(Hashtable parameters, DateTime time)
        {
            _action(parameters, time);
        }
    }
}
