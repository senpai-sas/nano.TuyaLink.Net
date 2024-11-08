using System;
using System.Collections;

using TuyaLink.Communication;
using TuyaLink.Communication.Events;
using TuyaLink.Events;
using TuyaLink.Properties;

namespace TuyaLink
{
    internal class FakeCommunicationHandler : ICommunicationHandler
    {
        public static readonly FakeCommunicationHandler Default = new();
        public ResponseHandler BatchReport(DeviceProperty[] properties, TriggerEventData[] triggerEventData)
        {
            throw new NotImplementedException();
        }

        public void Connect(DeviceInfo deviceInfo)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public ResponseHandler GetDeviceModel()
        {
            throw new NotImplementedException();
        }

        public GetFirmwareVersionResponseHandler GetFirmwareVersion()
        {
            throw new NotImplementedException();
        }

        public ResponseHandler GetProperties(DeviceProperty[] properties)
        {
            throw new NotImplementedException();
        }

        public ResponseHandler HistoryReport(TriggerEventData[][] events, DeviceProperty[][] properties)
        {
            throw new NotImplementedException();
        }

        public ResponseHandler ReportProperty(DeviceProperty property)
        {
            throw new NotImplementedException();
        }

        public ResponseHandler TriggerEvent(DeviceEvent deviceEvent, Hashtable parameters, DateTime time)
        {
            throw new NotImplementedException();
        }
    }

}
