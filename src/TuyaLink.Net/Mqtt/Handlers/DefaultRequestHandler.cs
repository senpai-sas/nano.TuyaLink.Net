﻿using TuyaLink.Communication;

namespace TuyaLink.Mqtt.Handlers
{
    internal class DefaultRequestHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler) : MqttDeviceRequestHandler(communication, responseHandler)
    {
        public override void HandleMessage(FunctionMessage message)
        {
            if (message is not FunctionResponse response)
            {
                throw new TuyaMqttException($"Received unexpected message type {message.GetType().Name}");
            }

            AcknowledgeResponse(response);
        }
    }
}