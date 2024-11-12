using System;
using System.Collections;

using TuyaLink.Communication;
using TuyaLink.Functions;
using TuyaLink.Model;

namespace TuyaLink.Events
{
    /// <summary>
    /// Represents a device event that can be triggered and acknowledged.
    /// </summary>
    /// <param name="code">The code of the event.</param>
    /// <param name="device">The device associated with the event.</param>
    /// <param name="acknowledge">Indicates whether the event requires acknowledgment.</param>
    public class DeviceEvent(string code, TuyaDevice device, bool acknowledge) : DeviceFunction(code, FunctionType.Event, device), IAcknowledgeable, IReportableFunction
    {

        /// <inheritdoc/>
        public bool Acknowledge { get; protected set; } = acknowledge;

        /// <summary>
        /// Gets the model associated with the event.
        /// </summary>
        public new EventModel? Model { get; private set; }

        /// <summary>
        /// Called when the event is triggered.
        /// </summary>
        /// <param name="parameters">The parameters associated with the event.</param>
        /// <param name="time">The time the event was triggered.</param>
        protected virtual void OnTriggering(Hashtable parameters, DateTime time)
        {
          
        }

        /// <summary>
        /// Triggers the event with the specified parameters and time.
        /// </summary>
        /// <param name="parameters">The parameters associated with the event.</param>
        /// <param name="time">The time the event is triggered.</param>
        /// <returns>A <see cref="ResponseHandler"/> that handles the response of the event.</returns>
        public ResponseHandler Trigger(Hashtable parameters, DateTime time)
        {
            CheckModel(() =>
            {
                Hashtable outputParameters = parameters;

                if (Model!.OutputParams.Length == 0 && outputParameters == null)
                {
                    return;
                }

                if (Model.OutputParams.Length > 0 && outputParameters == null)
                {
                    throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The Event {Code}  requires {Model.OutputParams.Length} parameters but none parameters was given, expected {Model.OutputParams.Length}, expected parameters are:\n\t{Model.OutputParams.Join("t\\")}");
                }

                if (Model.OutputParams.Length != outputParameters.Count)
                {
                    throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The Event {Code} receive an invalid number of output parameters: {outputParameters.Count}, expected {Model.OutputParams.Length}, expected parameters are:\n\t{Model.OutputParams.Join("t\\")}");
                }

                foreach (ParameterModel outputParam in Model!.OutputParams)
                {
                    if (!outputParameters.TryGetValue(outputParam.Code, out object? value))
                    {
                        throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The action {Code} does't return");
                    }

                    if (!outputParam.TypeSpec.Type.IsValidCloudValue(value))
                    {
                        throw new FunctionRuntimeException(StatusCode.FunctionOutputParameterMismatch, $"The action {Code} returned an invalid paramter: {outputParam.Code} =  {value}, expected value is a {outputParam.TypeSpec.Type}");
                    }

                    try
                    {
                        outputParam.TypeSpec.CheckCouldValue(value);
                    }
                    catch (ArgumentException ex)
                    {
                        throw new FunctionRuntimeException(
                            StatusCode.FunctionOutputParameterMismatch,
                            $"The Event {Code} receive an invalid paramter: {outputParam.Code} =  {value}, error: {ex.Message}",
                            ex);
                    }

                }
            });
            OnTriggering(parameters, time);
            return Device.TriggerEvent(this, parameters, time);
        }

        /// <summary>
        /// Binds the specified model to the event.
        /// </summary>
        /// <param name="model">The model to bind to the event.</param>
        public virtual void BindModel(EventModel model)
        {
            Model = model;
            base.BindModel(model);
            OnBindModel(model);
        }

        /// <summary>
        /// Called when the model is bound to the event.
        /// </summary>
        /// <param name="model">The model bound to the event.</param>
        protected virtual void OnBindModel(EventModel model)
        {

        }
    }
}
