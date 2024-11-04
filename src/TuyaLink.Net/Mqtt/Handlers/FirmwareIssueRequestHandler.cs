using System;

using TuyaLink.Communication;
using TuyaLink.Communication.Firmware;

namespace TuyaLink.Mqtt.Handlers
{
    internal class FirmwareIssueRequestHandler : MqttDeviceRequestHandler
    {
        public FirmwareIssueRequestHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler) : base(communication, responseHandler)
        {

        }

        public override void HandleMessage(FunctionMessage message)
        {
            if(message is not FirmwareIssueRequest request)
            {
                throw new TuyaMqttException($"Received unexpected message type {message.GetType().Name}");
            }

            Communication.IssueFirmware(request.Data);
        }

        public override void Published(ulong messageId)
        {
            base.Published(messageId);
            AcknowledgeResponse(new FunctionResponse() { MessageId = ResponseHandler.MessageId, Time = DateTime.UtcNow.ToUnixTimeSeconds() });
        }
    }
}
