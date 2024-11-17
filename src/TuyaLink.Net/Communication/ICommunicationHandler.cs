using System;
using System.Collections;

using TuyaLink.Communication.Events;
using TuyaLink.Communication.Firmware;
using TuyaLink.Events;
using TuyaLink.Functions.Actions;
using TuyaLink.Functions.Properties;
using TuyaLink.Model;
using TuyaLink.Properties;

namespace TuyaLink.Communication
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="args"></param>
    public delegate void PropertyUdpatedEventHandler(PropertyUpdatedEventArgs args);

    public delegate void PerformActionEventHandler(PerformActionEventArgs args);

    public delegate void ModelUpdatedEventHandler(ModelUpdatedEventArgs args);


    public interface ICommunicationHandler
    {
        public void Connect();

        public void Disconnect();


        /// <summary>
        /// The device requests the device model from the cloud.
        /// </summary>
        /// <para>A <see cref="ResponseHandler"/> object that can be used to wait for the response from the cloud.</para>
        public ResponseHandler GetDeviceModel();

        /// <summary>
        /// The device proactively reports its property value to the cloud.
        /// </summary>
        /// <param name="property"></param>
        /// <para>A <see cref="ResponseHandler"/> object that can be used to wait for the response from the cloud.</para>
        public ResponseHandler ReportProperty(DeviceProperty property);


        /// <summary>
        /// The device proactively reports the event-triggered message to the cloud.
        /// </summary>
        /// <param name="deviceEvent"></param>
        /// <param name="parameters"></param>
        /// <param name="time"></param>
        /// <para>A <see cref="ResponseHandler"/> object that can be used to wait for the response from the cloud.</para>
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
        /// When the device wakes up, it can proactively request cached commands from the cloud.
        /// </item>
        /// </list>
        /// </summary>
        /// <param name="properties"></param>
        /// <para>A <see cref="ResponseHandler"/> object that can be used to wait for the response from the cloud.</para>
        public ResponseHandler GetProperties(DeviceProperty[] properties);

        /// <summary>
        /// <para>
        /// The device reports historical data to the cloud in bulk, or the gateway device reports the historical data of sub-devices.
        /// </para>
        /// Usage scenarios:
        /// <list type="bullet">
        ///     <item>
        ///         When a device has a lot of data to report within a short time period, it can report them in one request for better efficiency.
        ///     </item>
        ///     <item>
        ///         A device reports data periodically or reports the stranded data in case of device offline, while retaining the historical data. The device can stage the historical process data in the local storage and report the staged data to the cloud in bulk.
        ///     </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Limitations:
        /// <list type="bullet">
        ///     <item>
        ///         A gateway device can report data of up to 20 sub-devices in one go.
        ///     </item>
        ///     <item>
        ///         Due to the limitation of the MQTT gateway, the payload size of a message cannot exceed 64 KB.
        ///     </item>
        ///     <item>Up to 500 pieces of historical data can be reported in one request.<br/>
        ///     That is, the size of property arrays + the size of event arrays + the size of sub-devices (the size of property arrays + the size of event arrays) must be less than 500.</item>
        ///     <item>If some data fails to be verified or processed, the code 2121 is returned. <br/>If all the data fails to be processed, the code 2122 is returned. For more information about errors, see the description of status codes.</item>
        /// </list>
        /// Things to note: 
        /// <list type="bullet">
        /// <item>The historical data is stored in a property or event array in ascending order by time.</item>
        /// <item>The cloud traverses the property or event arrays in order and then processes data</item>
        /// <item>In the same request, the latest status of a property or event prevails.<br/> The timestamp should be included in the historical data if possible.</item>
        /// </list>
        /// </remarks>
        /// <param name="events"></param>
        /// <param name="properties"></param>
        /// <para>A <see cref="ResponseHandler"/> object that can be used to wait for the response from the cloud.</para>
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
