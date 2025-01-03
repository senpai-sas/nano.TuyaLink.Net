﻿using System.Diagnostics;
using TuyaLink.Communication;
using TuyaLink.Communication.Mqtt;

namespace TuyaLink.Communication.Mqtt.Handlers
{
    internal abstract class MqttDeviceRequestHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler)
        : DeviceRequestHandler(responseHandler)
    {
        protected MqttCommunicationProtocol Communication { get; } = communication;

        public virtual void Published(ulong messageId)
        {
            Debug.WriteLine($"Request with MessageId: {ResponseHandler.MessageId} has been published with MQTT id: {messageId}");
        }
    }
}
