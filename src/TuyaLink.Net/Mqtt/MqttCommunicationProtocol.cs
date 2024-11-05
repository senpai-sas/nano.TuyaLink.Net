using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;

using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;

using TuyaLink.Communication;
using TuyaLink.Communication.Actions;
using TuyaLink.Communication.Events;
using TuyaLink.Communication.Firmware;
using TuyaLink.Communication.History;
using TuyaLink.Communication.Model;
using TuyaLink.Communication.Properties;
using TuyaLink.Events;
using TuyaLink.Functions;
using TuyaLink.Functions.Actions;
using TuyaLink.Functions.Events;
using TuyaLink.Json;
using TuyaLink.Model;
using TuyaLink.Mqtt.Topics;
using TuyaLink.Properties;

namespace TuyaLink.Mqtt
{
    public class MqttCommunicationProtocol : ICommunicationHandler
    {

        private readonly TuyaMqttSign _tuyaMqttSign = new();

        private readonly MqttClient _mqttClient;
        private readonly Hashtable _topicHandlers = new(16);

        private readonly TuyaDevice _device;
        private readonly DeviceSettings _deviceSettings;
        private DeviceRequestTopicHandler _batchReportTopicHandler;
        private DeviceRequestTopicHandler _eventTriggerTopicHandler;
        private DeviceRequestTopicHandler _getDeviceModelTopicHandler;
        private DeviceRequestTopicHandler _propertyGetTopicHandler;
        private DeviceRequestTopicHandler _reportPropertyTopicHandler;
        private DeviceRequestTopicHandler _historyReportTopicHandler;
        private DeviceRequestTopicHandler _getFirmwareVersionTopicHandler;
        private ReportFirmwareProgressTopicHandler _reportFirewareProgressTopic;
        private DeviceRequestTopicHandler _deleteDesiredPropertiesTopicHandler;

        internal DeviceInfo DeviceInfo { get; private set; }

        public MqttCommunicationProtocol(TuyaDevice tuyaDevice, DeviceSettings deviceSettings)
        {
            _deviceSettings = deviceSettings;
            _mqttClient = new MqttClient(_deviceSettings.DataCenter.Url, _deviceSettings.DataCenter.Port, true, GetCACertificate(), _deviceSettings.ClientCertificate, MqttSslProtocols.TLSv1_2);
            _mqttClient.MqttMsgPublishReceived += MqttMsgPublishReceived;
            _mqttClient.MqttMsgPublished += MqttMsgPublished;
            _mqttClient.ConnectionOpened += ConnectionOpened;
            _mqttClient.ConnectionClosed += ConnectionClosed;
            _mqttClient.ConnectionClosedRequest += ConnectionClosedRequest;
            _mqttClient.ProtocolVersion = MqttProtocolVersion.Version_3_1;
            _mqttClient.MqttMsgSubscribed += MqttMessageSubscribed;
            _mqttClient.MqttMsgUnsubscribed += MqttMessageUnsubscribed;

            _device = tuyaDevice;
        }

        private void MqttMessageUnsubscribed(object sender, MqttMsgUnsubscribedEventArgs e)
        {
            Debug.WriteLine("Message has been unsubscripted");
        }

        private void MqttMessageSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            Debug.WriteLine("Message has been subscribed");
        }

        static X509Certificate GetCACertificate()
        {
            return new X509Certificate(Constants.TuyaCACertificate);
        }

        private void ConnectionClosedRequest(object sender, ConnectionClosedRequestEventArgs e)
        {
            Debug.WriteLine("Connection closed request");
        }

        private void ConnectionClosed(object sender, EventArgs e)
        {
            Debug.WriteLine("Connection closed");
        }

        private void ConnectionOpened(object sender, ConnectionOpenedEventArgs e)
        {
            Debug.WriteLine("Connection opened");
        }

        private void Initialize()
        {
            JsonUtils.Initialize();
            _batchReportTopicHandler = RegisterDeviceRequestTopicHandler(new BatchReportTopicHandler(this));
            _eventTriggerTopicHandler = RegisterDeviceRequestTopicHandler(new EventTriggerTopicHandler(this));
            _getDeviceModelTopicHandler = RegisterDeviceRequestTopicHandler(new GetDeviceModelTopicHandler(this));
            _propertyGetTopicHandler = RegisterDeviceRequestTopicHandler(new PropertyGetTopicHandler(this));
            _reportPropertyTopicHandler = RegisterDeviceRequestTopicHandler(new ReportPropertyTopicHandler(this));
            _historyReportTopicHandler = RegisterDeviceRequestTopicHandler(new HistoryReportTopicHandler(this));
            _getFirmwareVersionTopicHandler = RegisterDeviceRequestTopicHandler(new GetFirmwareVersionTopicHandler(this));
            _deleteDesiredPropertiesTopicHandler = RegisterDeviceRequestTopicHandler(new DeleteDesiredPropertiesTopicHandler(this));
            RegisterCloudTopicHandler(new ActionExecuteTopicHandler(this));
            RegisterCloudTopicHandler(new PropertySetTopicHandler(this));
            _reportFirewareProgressTopic = new ReportFirmwareProgressTopicHandler(this);

        }

        private DeviceRequestTopicHandler RegisterDeviceRequestTopicHandler(DeviceRequestTopicHandler handler)
        {
            _topicHandlers.Add(handler.SubscribableTopic, handler);
            return handler;
        }

        private void RegisterCloudTopicHandler(CloudRequestTopicHandler handler)
        {
            _topicHandlers.Add(handler.SubscribableTopic, handler);
        }

        private void SubscribeHandlers()
        {
            int length = _topicHandlers.Count;
            string[] topics = new string[length];
            MqttQoSLevel[] qosLevels = new MqttQoSLevel[length];
            int index = 0;
            foreach (MqttTopicHandler item in _topicHandlers.Values)
            {
                topics[index] = item.SubscribableTopic;
                qosLevels[index] = item.SubscribableQoSLevel;
                index++;
            }
            _mqttClient.Subscribe(topics, qosLevels);
        }

        private void MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Debug.WriteLine($"Message has been published {e.MessageId}");
        }

        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            if (e.DupFlag)
            {
                Debug.WriteLine("Duplicated message");
                return;
            }

            if (!_topicHandlers.TryGetValue(e.Topic, out object? handler))
            {
                Debug.WriteLine($"Unknown topic {e.Topic}");
                return;
            }

            MqttTopicHandler topicHandler = (MqttTopicHandler)handler;
            topicHandler.HandleMessage(e.Message);
        }


        public void Connect(DeviceInfo deviceInfo)
        {
            DeviceInfo = deviceInfo;
            Initialize();
            try
            {
                InternalConnect();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while connecting to the server {ex}");
                throw new TuyaMqttException("Error while connecting to the server", ex);
            }
        }

        private void InternalConnect()
        {
            _tuyaMqttSign.Calculate(DeviceInfo.ProductId, DeviceInfo.DeviceId, DeviceInfo.DeviceSecret);
            MqttReasonCode reason = _mqttClient.Connect(_tuyaMqttSign.ClientId, _tuyaMqttSign.Username, _tuyaMqttSign.Password);

            if (reason != MqttReasonCode.Success)
            {
                throw new TuyaMqttConnectionException("Failed to connect to the server", reason);
            }

            Debug.WriteLine("Connected");

            SubscribeHandlers();
        }

        public void Disconnect()
        {
            _mqttClient.Disconnect();
        }

        public ResponseHandler ReportProperty(DeviceProperty property)
        {
            Debug.WriteLine($"Reporting property {property.Code}");
            string messageId = GetMessageId();

            ReportPropertyRequest request = new()
            {
                MsgId = messageId,
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                Sys = new SystemParameters()
                {
                    Ack = property.Acknowledge,
                },
                Data = new PropertyHashtable()
                {
                    [property.Code] = new PropertyValue()
                    {
                        Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                        Value = property.GetValue(),
                    }
                }
            };


            return PublishRequest(_reportPropertyTopicHandler, request, property.Acknowledge);
        }

        public ResponseHandler BatchReport(DeviceProperty[] properties, TriggerEventData[] triggerEventData)
        {
            if (properties.Length == 0 && triggerEventData.Length == 0)
            {
                throw new ArgumentException("At least one property or event must be reported");
            }

            string messageId = GetMessageId();

            PropertyHashtable propertiesData = new(properties.Length);

            EventDataHashtable events = new(properties.Length);

            foreach (DeviceProperty property in properties)
            {
                propertiesData[property.Code] = new PropertyValue()
                {
                    Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                    Value = property.GetValue(),
                };
            }

            foreach (TriggerEventData deviceEvent in triggerEventData)
            {
                events[deviceEvent.EventCode] = deviceEvent;
            }

            BatchReportRequest request = new()
            {
                MsgId = messageId,
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                Sys =
                {
                    Ack = true,
                },
                Data = new BatchReportRequestData()
                {
                    Properties = propertiesData,
                    Events = events
                }
            };

            return PublishRequest(_batchReportTopicHandler, request, true);
        }

        public ResponseHandler TriggerEvent(DeviceEvent deviceEvent, Hashtable parameters, DateTime time)
        {
            string messageId = GetMessageId();
            TriggerEventData eventData = new()
            {
                EventCode = deviceEvent.Code,
                EventTime = time.ToUnixTimeSeconds(),
                OutputParams = parameters,
            };
            TriggerEventRequest request = new()
            {
                MsgId = messageId,
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                Data = eventData,
                Sys =
                {
                    Ack = deviceEvent.Acknowledge
                }
            };

            return PublishRequest(_eventTriggerTopicHandler, request, deviceEvent.Acknowledge);
        }

        public ResponseHandler GetDeviceModel()
        {
            string messageId = GetMessageId();
            GetDeviceModelRequest request = new()
            {
                MsgId = messageId,
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                Data =
                {
                    Format = DeviceModelDataFormat.Simple,
                }
            };

            return PublishRequest(_getDeviceModelTopicHandler, request, true);
        }

        public ResponseHandler HistoryReport(TriggerEventData[][] events, DeviceProperty[][] properties)
        {
            EventDataHashtable[] eventsData = new EventDataHashtable[events.Length];
            int index = 0;
            foreach (TriggerEventData[] eventBatch in events)
            {
                EventDataHashtable eventData = new(eventBatch.Length);
                foreach (TriggerEventData triggerEvent in eventBatch)
                {
                    eventData[triggerEvent.EventCode] = triggerEvent;
                }
                eventsData[index] = eventData;
                index++;
            }
            index = 0;
            PropertyHashtable[] propertiesData = new PropertyHashtable[properties.Length];
            foreach (DeviceProperty[] propertyBatch in properties)
            {
                PropertyHashtable propertyData = new(propertyBatch.Length);
                foreach (DeviceProperty property in propertyBatch)
                {
                    propertyData[property.Code] = new PropertyValue()
                    {
                        Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                        Value = property.GetValue(),
                    };
                }
                propertiesData[index] = propertyData;
                index++;
            }
            HistoryReportData data = new(propertiesData, eventsData);
            HistoryReportRequest historyReportRequest = new(data)
            {
                MsgId = GetMessageId(),
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
            };
            return PublishRequest(_historyReportTopicHandler, historyReportRequest, true);
        }

        public ResponseHandler GetProperties(params DeviceProperty[] properties)
        {
            if (properties.Length == 0)
            {
                throw new ArgumentException("At least one property must be specified");
            }

            string[] names = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                names[i] = properties[i].Code;
            }

            GetPropertiesRequest request = new()
            {
                MsgId = GetMessageId(),
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
                Data =
                {
                    Properties = names,
                },
            };
            return PublishRequest(_propertyGetTopicHandler, request, true);
        }

        private ResponseHandler PublishRequest(DeviceRequestTopicHandler topicHandler, FunctionRequest request, bool acknowlage)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            ResponseHandler response = topicHandler.RegisterMessage(request, acknowlage);
            string content = JsonUtils.Serialize(request);
            Debug.WriteLine($"Publish MQTT message, topic: {topicHandler.PublishableTopic},  request :\n\t" + content);

            ushort mqttMessageId = _mqttClient.Publish(topicHandler.PublishableTopic, Encoding.UTF8.GetBytes(content), null, null, topicHandler.PublishableQoSLevel, false);

            Debug.WriteLine($"Mqstt messageId: {mqttMessageId}");
            return response;
        }

        public GetFirmwareVersionResponseHandler GetFirmwareVersion()
        {
            GetFirmwareVersionRequest request = new()
            {
                MsgId = Guid.NewGuid().ToString(),
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
            };
            return (GetFirmwareVersionResponseHandler)PublishRequest(_getFirmwareVersionTopicHandler, request, true);
        }

        internal void PublishResponse(MqttTopicHandler topic, FunctionResponse response)
        {
            string content = JsonUtils.Serialize(response);
            Debug.WriteLine("Sending response:\n" + content);
            _mqttClient.Publish(topic.PublishableTopic, Encoding.UTF8.GetBytes(content), null, null, topic.PublishableQoSLevel, false);
        }

        private static string GetMessageId()
        {
            return Guid.NewGuid().To32String();
        }

        internal ActionExecuteResult ActionExecute(ActionExecuteRequest request)
        {
            return _device.ActionExecute(request.Data.ActionCode, request.Data.InputParams);
        }

        internal void UpdateProperties(DesiredPropertiesHashtable properties)
        {
            _device.UpdateProperties(properties);

            if (!_deviceSettings.AutoDeleteDesiredProperties)
            {
                return;
            }

            var deleteProperties = new DeleteDesiredPropertiesHashtable(properties.Count);


            foreach (DictionaryEntry property in properties)
            {
                deleteProperties[property.Key] = new DeleteDesiredProperty()
                {
                    Version = ((DesiredProperty)property.Value).Version,
                };
            }
            var request = new DeleteDesiredPropertyRequest()
            {
                Data = new()
                {
                    Properties = deleteProperties,
                }
            };
            PublishRequest(_deleteDesiredPropertiesTopicHandler, request, true);
        }

        internal void UpdateModel(DeviceModel data)
        {
            _device.UpdateModel(data);
        }

        internal StatusCode PropertySet(PropertySetRequest request)
        {
            return _device.PropertySet(request.Data);
        }

        internal void IssueFirmware(FirmwareUpdateData data)
        {
            _device.IssueFirmware(data, ReportFirmwareProgress);
        }

        private void ReportFirmwareProgress(ProgressReportData progress)
        {
            FirmwareProgressReportRequest request = new(progress)
            {
                MsgId = GetMessageId(),
                Time = DateTime.UtcNow.ToUnixTimeSeconds(),
            };

            PublishResponse(_reportFirewareProgressTopic, request);
        }
    }
}
