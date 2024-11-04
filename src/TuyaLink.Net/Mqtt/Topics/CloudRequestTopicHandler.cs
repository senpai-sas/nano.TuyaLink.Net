﻿
using System;

using TuyaLink.Communication;

namespace TuyaLink.Mqtt.Topics
{
    internal abstract class CloudRequestTopicHandler(Type responseType, MqttCommunicationProtocol communication) 
        : MqttTopicHandler(responseType, communication)
    {
        protected abstract CloudRequestHandler CreateCloudRequestHandler();

        public override void HandleMessage(byte[] message)
        {
            var request = DeserializeMessage(message);
            var handler = CreateCloudRequestHandler();
            handler.HandleMessage(request);
        }
    }
}
