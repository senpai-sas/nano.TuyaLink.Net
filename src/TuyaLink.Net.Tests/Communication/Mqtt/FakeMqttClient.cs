using System;
using System.Collections;

using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;

namespace TuyaLink.Communication.Mqtt
{

    public delegate MqttReasonCode MqttConnectDelegate(string clientId, string username, string password, bool willRetain, MqttQoSLevel willQosLevel, bool willFlag, string willTopic, string willMessage, bool cleanSession, ushort keepAlivePeriod);

    public delegate ushort PublishDelegate(string topic, byte[] message, string contentType, ArrayList userProperties, MqttQoSLevel qosLevel, bool retain);

    public delegate ushort SubscribeDelegate(string[] topics, MqttQoSLevel[] qosLevels);

    public delegate ushort UnsubscribeDelegate(string[] topics);

    internal class FakeMqttClient : IMqttClient
    {
        public bool IsConnected { get; private set; }

        public event IMqttClient.MqttMsgPublishEventHandler MqttMsgPublishReceived;
        public event IMqttClient.MqttMsgPublishedEventHandler MqttMsgPublished;
        public event IMqttClient.MqttMsgSubscribedEventHandler MqttMsgSubscribed;
        public event IMqttClient.MqttMsgUnsubscribedEventHandler MqttMsgUnsubscribed;
        public event IMqttClient.ConnectionClosedEventHandler ConnectionClosed;


        public MqttConnectDelegate MqttConnectDelegate { get; set; }

        public PublishDelegate PublishDelegate { get; set; }

        public SubscribeDelegate SubscribeDelegate { get; set; }

        public UnsubscribeDelegate UnsubscribeDelegate { get; set; }
        public void Close()
        {
            ConnectionClosed?.Invoke(this, EventArgs.Empty);
        }


        public void OnMqttMsgPublishReceived(MqttMsgPublishEventArgs e)
        {
            MqttMsgPublishReceived?.Invoke(this, e);
        }

        public MqttReasonCode Connect(string clientId, string username, string password, bool willRetain, MqttQoSLevel willQosLevel, bool willFlag, string willTopic, string willMessage, bool cleanSession, ushort keepAlivePeriod)
        {
            if (MqttConnectDelegate is not null)
            {
                MqttReasonCode reason = MqttConnectDelegate(clientId, username, password, willRetain, willQosLevel, willFlag, willTopic, willMessage, cleanSession, keepAlivePeriod);
                if (reason != MqttReasonCode.Success)
                {
                    ConnectionClosed?.Invoke(this, EventArgs.Empty);
                }
            }
            IsConnected = true;

            return MqttReasonCode.Success;
        }

        public void Disconnect()
        {
            ConnectionClosed?.Invoke(this, EventArgs.Empty);
            IsConnected = false;
        }

        public void Init(string brokerHostName, int brokerPort, bool secure, byte[] caCert, byte[] clientCert, MqttSslProtocols sslProtocol)
        {

        }

        public ushort Publish(string topic, byte[] message, string contentType, ArrayList userProperties, MqttQoSLevel qosLevel, bool retain)
        {
            if (PublishDelegate is not null)
            {
                return PublishDelegate(topic, message, contentType, userProperties, qosLevel, retain);
            }

            LastPublishedTopic = topic;
            LastPublishedMessage = message;

            return 0;
        }

        public byte[] LastPublishedMessage { get; set; }

        public string LastPublishedTopic { get; private set; }

        public ushort Publish(string topic, byte[] message, string contentType)
        {
            return Publish(topic, message, contentType, new ArrayList(), MqttQoSLevel.AtMostOnce, true);
        }

        public ushort Publish(string topic, byte[] message)
        {
            return Publish(topic, message, "application/json");
        }

        public ushort Subscribe(string[] topics, MqttQoSLevel[] qosLevels)
        {
            if (SubscribeDelegate is not null)
            {
                return SubscribeDelegate(topics, qosLevels);
            }

            return 0;
        }

        public ushort Unsubscribe(string[] topics)
        {
            if (UnsubscribeDelegate is not null)
            {
                return UnsubscribeDelegate(topics);
            }
            return 0;
        }
    }
}
