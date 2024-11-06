using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

using TuyaLink.Actions;
using TuyaLink.Communication;
using TuyaLink.Communication.Firmware;
using TuyaLink.Communication.Properties;
using TuyaLink.Events;
using TuyaLink.Firmware;
using TuyaLink.Functions;
using TuyaLink.Functions.Actions;
using TuyaLink.Model;
using TuyaLink.Mqtt;
using TuyaLink.Properties;

namespace TuyaLink
{
    public class TuyaDevice
    {
        public DeviceInfo Info { get; }
        public DeviceSettings Settings { get; }

        private readonly ICommunicationHandler _communicationProtocol;

        public Hashtable Properties { get; private set; } = [];

        public Hashtable Events { get; private set; } = [];

        public Hashtable Actions { get; private set; } = [];

        protected DeviceModel? Model { get; private set; }

        public TuyaDevice(DeviceInfo info, DeviceSettings? settings = null) : this(info, settings, null)
        {

        }

        internal TuyaDevice(DeviceInfo info, DeviceSettings? settigns = null, ICommunicationHandler? communicationProtocol = null)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
            Settings = settigns ?? DeviceSettings.Default;
            info.Validate();
            _communicationProtocol = communicationProtocol ?? new MqttCommunicationProtocol(this, Settings);
        }

        internal ActionExecuteResult ActionExecute(string actionCode, Hashtable inputParameters)
        {
            if (!Actions.TryGetValue(actionCode, out object value))
            {
                return ActionExecuteResult.Failure(StatusCode.FunctionNotFound);
            }
            var action = (DeviceAction)value;
            return action.Execute(inputParameters);
        }

        internal StatusCode PropertySet(Hashtable properties)
        {
            foreach (DictionaryEntry propertyEntry in properties)
            {
                if (!Properties.TryGetValue(propertyEntry.Key, out object value))
                {
                    return StatusCode.FunctionNotFound;
                }

                if (value is DeviceProperty property)
                {
                    property.CloudUpdate(propertyEntry.Value);
                }
            }
            return StatusCode.Success;
        }

        internal ResponseHandler ReportProperty(DeviceProperty property)
        {
            return _communicationProtocol.ReportProperty(property);
        }

        internal ResponseHandler TriggerEvent(DeviceEvent deviceEvent, Hashtable parameters, DateTime time)
        {
            return _communicationProtocol.TriggerEvent(deviceEvent, parameters, time);
        }

        public void Connect(int millisecondsTimeout = Timeout.Infinite)
        {
            _communicationProtocol.Connect(Info);
            var handler = _communicationProtocol.GetDeviceModel();
            handler.WaitForAcknowledgeReport();
        }

        public void Disconnect()
        {
            _communicationProtocol.Disconnect();
        }

        public void AddProperty(DeviceProperty property)
        {
            Properties.Add(property.Code, property);
        }

        public void AddEvent(DeviceEvent deviceEvent)
        {
            Events.Add(deviceEvent.Code, deviceEvent);
        }

        public void AddAction(DeviceAction deviceAction)
        {
            Actions.Add(deviceAction.Code, deviceAction);
        }

        public ResponseHandler GetProperties(params DeviceProperty[] properties)
        {
            return _communicationProtocol.GetProperties(properties);
        }

        protected DeviceAction RegisterAction(string name, ActionExecuteDelegate executeDelegate)
        {
            var action = new DelegateDeviceAction(name, this, executeDelegate);
            AddAction(action);
            return action;
        }

        internal void UpdateModel(DeviceModel model)
        {
            if (Settings.BindModel)
            {
                BindModel(model);
            }
            Model = model;
        }

        internal void BindModel(DeviceModel model)
        {
            foreach (ModelService service in model.Services)
            {
                foreach (var propertyModel in service.Properties)
                {
                    if (!Properties.TryGetValue(propertyModel.Code, out object value))
                    {
                        if (Settings.ValdiateModel)
                            throw new TuyaLinkException($"Property {propertyModel} not implemented in device {Info.DeviceId}");
                    }

                    var property = (DeviceProperty)value;

                    property.BindModel(propertyModel);
                }

                foreach (var eventModel in service.Events)
                {
                    if (!Events.TryGetValue(eventModel.Code, out object value))
                    {
                        if (Settings.ValdiateModel)
                            throw new TuyaLinkException($"Event {eventModel} not implemented in device {Info.DeviceId}");
                    }
                    var deviceEvent = (DeviceEvent)value;
                    deviceEvent.BindModel(eventModel);

                }

                foreach (var actionModel in service.Actions)
                {
                    if (!Actions.TryGetValue(actionModel.Code, out object value))
                    {
                        if (Settings.ValdiateModel)
                            throw new TuyaLinkException($"Action {actionModel} not implemented in device {Info.DeviceId}");
                    }

                    var deviceAction = (DeviceAction)value;
                    deviceAction.BindModel(actionModel);
                }
            }
        }

        internal void UpdateProperties(DesiredPropertiesHashtable properties)
        {
            foreach (DictionaryEntry entry in properties)
            {
                if (!Properties.TryGetValue(entry, out object value))
                {
                    throw new TuyaLinkException($"Property {entry.Key} not found in device {Info.DeviceId}");
                }
                var desiredProperty = (DesiredProperty)entry.Value;
                Debug.WriteLine($"Desired property {entry.Key} updated, version {desiredProperty.Version}");
                var property = (DeviceProperty)value;
                property.CloudUpdate(desiredProperty.Value);
            }
        }

        internal void IssueFirmware(FirmwareUpdateData data, FirmwareUpdateProgressDelegate progressDelegate)
        {
            if (Settings.FirmwareManager == null)
            {
                Debug.WriteLine("Firmware manager not provided");
                return;
            }
            Settings.FirmwareManager.IssueFirmware(data, progressDelegate);
        }

        public GetFirmwareVersionResponseHandler GetLastFirmwareVersion()
        {

            return _communicationProtocol.GetFirmwareVersion();
        }
    }
}
