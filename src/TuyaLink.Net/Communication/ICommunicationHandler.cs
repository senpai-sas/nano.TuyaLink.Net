using System;
using System.Collections;

using TuyaLink.Communication.Events;
using TuyaLink.Communication.Firmware;
using TuyaLink.Events;
using TuyaLink.Properties;

namespace TuyaLink.Communication
{
    public interface ICommunicationHandler
    {
        public void Connect(DeviceInfo deviceInfo);

        public void Disconnect();

        public ResponseHandler GetDeviceModel();

        /// <summary>
        /// The device proactively reports its property value to the cloud.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public ResponseHandler ReportProperty(DeviceProperty property);





        /// <summary>
        /// The device proactively reports the event-triggered message to the cloud.
        /// </summary>
        /// <param name="deviceEvent"></param>
        /// <param name="parameters"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        ResponseHandler TriggerEvent(DeviceEvent deviceEvent, Hashtable parameters, DateTime time);

        /// <summary>
        /// <para>
        /// Get the desired properties from the cloud.
        /// </para>
        /// Usage scenarios:
        /// <list type="bullet">
        /// <item>
        /// When the device is offline, you cannot send a command to it directly. 
        /// In this case, commands can be cached in the cloud so that the device can proactively request and execute them when it goes online.
        /// </item>
        /// <item>
        /// Low power devices sleep and wake up regularly. 
        /// When the device wakes up, it can proactively request cached commands from the cloud.</item>
        /// </list>
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public ResponseHandler GetProperties(DeviceProperty[] properties);


        ResponseHandler HistoryReport(TriggerEventData[][] events, DeviceProperty[][] properties);

        /// <summary>
        /// This feature allows a device to report multiple events or properties in one go. 
        /// For a gateway device, it can report data of multiple sub-devices at the same time. 
        /// This way, transfer efficiency is increased.
        /// </summary>
        /// <remarks>
        /// Limitations:
        /// <list type="bullet">
        ///     <item>
        ///        A gateway device can report data of up to 20 sub-devices in one go.
        ///     </item>
        ///     <item>
        ///         Due to the limitation of the MQTT gateway, the payload size of a message cannot exceed 64 KB.
        ///     </item>
        ///     <item>
        ///         If some data fails to be verified or processed, the code 2121 is returned.<br/>
        ///         If all the data fails to be processed, the code 2122 is returned. For more information about errors, see the description of status codes.
        ///      </item>
        /// </list>
        /// </remarks>
        /// <param name="reportables"></param>
        /// <returns>
        /// <para>A <see cref="ResponseHandler"/> object that can be used to wait for the response from the cloud.</para>
        /// </returns>
        ResponseHandler BatchReport(DeviceProperty[] properties, TriggerEventData[] triggerEventData);

        GetFirmwareVersionResponseHandler GetFirmwareVersion();
    }
}
