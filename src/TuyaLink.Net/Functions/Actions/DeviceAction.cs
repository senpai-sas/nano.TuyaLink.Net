using System;
using System.Collections;

using TuyaLink.Model;

namespace TuyaLink.Functions.Actions
{
    /// <summary>
    /// Represents an abstract base class for device actions.
    /// </summary>
    public abstract class DeviceAction : DeviceFunction
    {
        private static readonly Hashtable _empty = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceAction"/> class.
        /// </summary>
        /// <param name="code">The code representing the action.</param>
        /// <param name="device">The device associated with the action.</param>
        /// <exception cref="ArgumentException">Thrown when the code is null or whitespace.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the device is null.</exception>
        public DeviceAction(string code, TuyaDevice device) : base(code, FunctionType.Action, device)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException($"'{nameof(code)}' cannot be null or whitespace.", nameof(code));
            }

            if (device is null)
            {
                throw new ArgumentNullException(nameof(device));
            }
        }

        /// <summary>
        /// Gets the action model associated with this action.
        /// </summary>
        public new ActionModel? Model { get => (ActionModel?)base.Model; }

        /// <summary>
        /// Executes the action with the specified input parameters.
        /// </summary>
        /// <param name="inputParams">The input parameters for the action.</param>
        /// <returns>The result of the action execution.</returns>
        protected virtual ActionExecuteResult OnExecute(Hashtable inputParams)
        {
            return ActionExecuteResult.Success(_empty);
        }

        /// <summary>
        /// Binds the specified action model to this action.
        /// </summary>
        /// <param name="model">The action model to bind.</param>
        internal void BindModel(ActionModel model)
        {
            base.BindModel(model);
            OnBindModel(model);
        }

        /// <summary>
        /// Called when the action model is bound to this action.
        /// </summary>
        /// <param name="model">The action model that was bound.</param>
        protected virtual void OnBindModel(ActionModel model)
        {

        }

        /// <summary>
        /// Executes the action with the specified input parameters.
        /// </summary>
        /// <param name="inputParams">The input parameters for the action.</param>
        /// <returns>The result of the action execution.</returns>
        internal ActionExecuteResult Execute(Hashtable inputParams)
        {
            ActionExecuteResult result = OnExecute(inputParams);

            CheckModel(() =>
            {
                Hashtable outputParameters = result.OutputParameters;

                if (Model!.OutputParams.Length == 0 && outputParameters == null)
                {
                    return;
                }

                if (Model.OutputParams.Length > 0 && outputParameters == null)
                {
                    throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The action {Code} returned no output parameters, expected parameters count are: {Model.OutputParams.Length}");
                }

                if (Model.OutputParams.Length != outputParameters.Count)
                {
                    throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The action {Code} returned an invalid number of output parameters: {outputParameters.Count}, expected parameters count are: {Model.OutputParams.Length}");
                }

                CheckOutputParameters(outputParameters);
            });

            return result;
        }

        protected virtual void CheckOutputParameters(Hashtable outputParameters)
        {
            foreach (ParameterModel model in outputParameters)
            {
                if (!outputParameters.TryGetValue(model.Code, out object? value))
                {
                    throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The action {Code} does't return");
                }

                if (!model.TypeSpec.Type.IsValidCloudValue(value))
                {
                    throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The action {Code} returned an invalid paramter: {model.Code} =  {value}, expected value is a {model.TypeSpec.Type}");
                }

                try
                {
                    model.TypeSpec.CheckCouldValue(value);
                }
                catch (ArgumentException ex)
                {
                    throw new FunctionRuntimeException(
                        StatusCode.FunctionOutputParameterMismatch,
                        $"The action {Code} returned an invalid paramter: {model.Code} =  {value}, error: {ex.Message}",
                        ex);
                }
            }
        }
    }
}
