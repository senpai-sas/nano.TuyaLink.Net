using System;
using System.Collections;

using TuyaLink.Communication;
using TuyaLink.Communication.Events;
using TuyaLink.Functions;
using TuyaLink.Model;

namespace TuyaLink.Events
{
    public class DeviceEvent(string code, TuyaDevice device, bool acknowledge) : DeviceFunction(code, FunctionType.Event, device), IAcknowledgeable, IReportableFunction
    {

        ///<inheritdoc/>
        public bool Acknowledge { get; protected set; } = acknowledge;

        public new EventModel Model { get; private set; }

        protected virtual void OnTriggering(Hashtable parameters, DateTime time)
        {

        }

        public ResponseHandler Trigger(Hashtable parameters, DateTime time)
        {
            OnTriggering(parameters, time);
            return Device.TriggerEvent(this, parameters, time);
        }

        public virtual void BindModel(EventModel model)
        {
            Model = model;
            base.BindModel(model);
            OnBindModel(model);
        }

        protected virtual void OnBindModel(EventModel model)
        {

        }
    }
}
