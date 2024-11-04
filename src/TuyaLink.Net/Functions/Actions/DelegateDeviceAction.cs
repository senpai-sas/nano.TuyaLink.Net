

using System.Collections;

using TuyaLink.Functions.Actions;

namespace TuyaLink.Actions
{
    public delegate ActionExecuteResult ActionExecuteDelegate(Hashtable inputParams);

    public class DelegateDeviceAction(string name, TuyaDevice device, ActionExecuteDelegate executeDelegate) : DeviceAction(name, device)
    {
        private readonly ActionExecuteDelegate _actionDelegate = executeDelegate ?? throw new System.ArgumentNullException(nameof(executeDelegate));

        protected override ActionExecuteResult OnExecute(Hashtable inputParams)
        {
            return _actionDelegate(inputParams);
        }
    }
}
