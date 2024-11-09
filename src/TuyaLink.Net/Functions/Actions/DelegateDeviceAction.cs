

using System.Collections;

using TuyaLink.Functions.Actions;

namespace TuyaLink.Actions
{
    /// <summary>
    /// Delegate for executing an action with the provided input parameters.
    /// </summary>
    /// <param name="inputParams">The input parameters for the action.</param>
    /// <returns>The result of the action execution.</returns>
    public delegate ActionExecuteResult ActionExecuteDelegate(Hashtable inputParams);


    /// <summary>
    /// And delegate to execute the action.
    /// </summary>
    /// <param name="code">The code representing the action.</param>
    /// <param name="device">The device associated with the action.</param>
    /// <param name="executeDelegate">The delegate to execute the action.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="executeDelegate"/> is null.</exception>
    public class DelegateDeviceAction(string code, TuyaDevice device, ActionExecuteDelegate executeDelegate) : DeviceAction(code, device)
    {
        private readonly ActionExecuteDelegate _actionDelegate = executeDelegate ?? throw new System.ArgumentNullException(nameof(executeDelegate));

        /// <inheritdoc/>
        protected override ActionExecuteResult OnExecute(Hashtable inputParams)
        {
            return _actionDelegate(inputParams);
        }
    }
}
