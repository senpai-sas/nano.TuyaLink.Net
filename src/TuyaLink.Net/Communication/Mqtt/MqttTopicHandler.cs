
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using nanoFramework.Json;
using nanoFramework.M2Mqtt.Messages;
using TuyaLink.Communication;
using TuyaLink.Json;

namespace TuyaLink.Communication.Mqtt
{
    internal abstract class MqttTopicHandler
    {
        public string SubscribableTopic { get; }

        public string PublishableTopic { get; }

        public Type? ResponseType { get; }

        public MqttCommunicationProtocol Communication { get; }

        public MqttQoSLevel SubscribableQoSLevel { get; protected set; } = MqttQoSLevel.AtLeastOnce;

        public MqttQoSLevel PublishableQoSLevel { get; protected set; } = MqttQoSLevel.AtLeastOnce;

        protected bool RequireHandler { get; set; } = true;

        protected abstract string SubscribableTopicTemplate { get; }

        protected abstract string PublishableTopicTemplate { get; }

        public MqttTopicHandler(Type? responseType, MqttCommunicationProtocol communication, bool produceResponse = true)
        {
            ResponseType = responseType ?? (produceResponse ? throw new ArgumentNullException(nameof(responseType)) : null);
            Communication = communication ?? throw new ArgumentNullException(nameof(communication));
            SubscribableTopic = string.Format(SubscribableTopicTemplate, Communication.DeviceInfo.DeviceId);
            PublishableTopic = string.Format(PublishableTopicTemplate, Communication.DeviceInfo.DeviceId);
        }

        public abstract void HandleMessage(byte[] data);

        protected FunctionMessage DeserializeMessage(byte[] data)
        {
            Debug.WriteLine($"Message has been received from: {SubscribableTopic}");
            if (ResponseType == null)
            {
                throw new InvalidOperationException("ResponseType is not set");
            }
            using MemoryStream stream = new(data);
            FunctionMessage message = (FunctionMessage)JsonUtils.Deserialize(stream, ResponseType);
#if DEBUG
            Debug.WriteLine($"Message: {Encoding.UTF8.GetString(data, 0, data.Length)}");
#endif
            return message;
        }

    }
}
