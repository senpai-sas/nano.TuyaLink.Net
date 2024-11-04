using System;
using System.Collections;

using TuyaLink.Model;

namespace TuyaLink.Functions.Actions
{
    public abstract class DeviceAction : DeviceFunction
    {
        public DeviceAction(string name, TuyaDevice device) : base(name, FunctionType.Action, device)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }
        }

        public new ActionModel Model { get => (ActionModel)base.Model; }

        protected virtual ActionExecuteResult OnExecute(Hashtable inputParams)
        {
            return ActionExecuteResult.Success([]);
        }

        internal void BindModel(ActionModel model)
        {
            base.BindModel(model);
            OnBindModel(model);
        }

        protected virtual void OnBindModel(ActionModel model)
        {

        }

        internal ActionExecuteResult Execute(IDictionary inputParams)
        {
            return Execute(inputParams);
        }
    }


}
