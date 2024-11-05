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
            if (!_handlersStore.TryGetValue(response.MsgId, out object handler))
            {
                throw new TuyaMqttException($"No response handler found for message id {response.MsgId}");
            }
            _handlersStore.Remove(response.MsgId);

            var responseHandler = (DeviceRequestHandler)handler;
            responseHandler.HandleMessage(response);
        }

        public ResponseHandler RegisterMessage(FunctionMessage message, bool acknowlage)
        {
            var responseHandler = CreateResponseHandler(message.MsgId, acknowlage);
            var handler = CreateRequestHandler(responseHandler);
            _handlersStore.Add(message.MsgId, handler);
            return responseHandler;
        }

        protected virtual ResponseHandler CreateResponseHandler(string messageId, bool acknowledgment)
        {
            return new ResponseHandler(messageId, acknowledgment);
        }

        protected abstract DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler);
    }
}
