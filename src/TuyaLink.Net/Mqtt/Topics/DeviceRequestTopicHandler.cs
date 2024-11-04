using System;
using System.Collections;

using TuyaLink.Communication;

namespace TuyaLink.Mqtt.Topics
{
    internal abstract class DeviceRequestTopicHandler : MqttTopicHandler
    {
        private readonly Hashtable _handlersStore = [];

        protected DeviceRequestTopicHandler(Type responseType, MqttCommunicationProtocol communication) : base(responseType, communication)
        {
        }

        public override void HandleMessage(byte[] message)
        {
            var response = DeserializeMessage(message);
            if (!_handlersStore.TryGetValue(response.MessageId, out object handler))
            {
                throw new TuyaMqttException($"No response handler found for message id {response.MessageId}");
            }
            _handlersStore.Remove(response.MessageId);

            var responseHandler = (DevieRequestHandler)handler;
            responseHandler.HandleMessage(response);
        }

        public ResponseHandler RegisterMessage(FunctionMessage message, bool acknowlage)
        {
            var responseHandler = CreateResponseHandler(message.MessageId, acknowlage);
            var handler = CreateRequestHandler(responseHandler);
            _handlersStore.Add(message.MessageId, handler);
            return responseHandler;
        }

        protected virtual ResponseHandler CreateResponseHandler(string messageId, bool acknowledgment)
        {
            return new ResponseHandler(messageId, acknowledgment);
        }

        protected abstract DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler);
    }
}
