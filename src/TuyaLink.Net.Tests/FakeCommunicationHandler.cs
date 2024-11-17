using System;
using System.Collections;

using TuyaLink.Communication;
using TuyaLink.Communication.Events;
using TuyaLink.Communication.Firmware;
using TuyaLink.Communication.Properties;
using TuyaLink.Events;
using TuyaLink.Properties;

namespace TuyaLink
{

    public delegate ResponseHandler FakeReportPropertyDelegate(DeviceProperty property);

    public delegate ResponseHandler FakeTriggerEventDelegate(DeviceEvent deviceEvent, Hashtable parameters, DateTime time);
    internal class FakeCommunicationHandler : ICommunicationHandler
    {
        public static FakeCommunicationHandler Default => new();

        public FakeReportPropertyDelegate ReportPropertyDelegate { get; set; }

        public FakeTriggerEventDelegate TriggerEventDelegate { get; set; }

        public ResponseHandler BatchReport(DeviceProperty[] properties, TriggerEventData[] triggerEventData)
        {
            throw new NotImplementedException();
        }

        public void Connect()
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
            ReportPropertyRequest request = new()
            {
                MsgId = FunctionMessage.GetNextMessageId(),
                Data = new Communication.History.PropertyHashtable()
                {
                    [property.Code] = new PropertyValue()
                    {
                        Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                        Value = property.GetCloudValue(),
                    }
                }
            };
            return ReportPropertyDelegate?.Invoke(property) ?? throw new NotImplementedException();
        }

        public ResponseHandler TriggerEvent(DeviceEvent deviceEvent, Hashtable parameters, DateTime time)
        {
            TriggerEventRequest request = new()
            {
                MsgId = FunctionMessage.GetNextMessageId(),
                Data = new TriggerEventData
                {
                    EventCode = deviceEvent.Code,
                    EventTime = time.ToUnixTimeSeconds(),
                    OutputParams = parameters,
                }
            };

            return TriggerEventDelegate?.Invoke(deviceEvent, parameters, time) ?? throw new NotImplementedException();
        }
    }

}
