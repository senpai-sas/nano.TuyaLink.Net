using TuyaLink.Communication;
using TuyaLink.Communication.Firmware;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class GetFirmwareVersionTopicHandler : DeviceRequestTopicHandler
    {
        public const string GetFirmwareVersionRequestTopic = "tylink/{0}/ota/get";
        public const string GetFirmwareVersionResponseTopic = "tylink/{0}/ota/get_response";

        public GetFirmwareVersionTopicHandler(MqttCommunicationProtocol communication) : base(typeof(GetFirmwareVersionResponse), communication)
        {
        }

        protected override string SubscribableTopicTemplate => GetFirmwareVersionRequestTopic;
        protected override string PublishableTopicTemplate => GetFirmwareVersionResponseTopic;

        protected override DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new GetFirmwareVersionRequestHandler(Communication, responseHandler);
        }

        protected override ResponseHandler CreateResponseHandler(string messageId, bool acknowledgment)
        {
            return new GetFirmwareVersionResponseHandler(messageId, acknowledgment);
        }
    }
}
