using TuyaLink.Communication.Mqtt.Handlers;
using TuyaLink.Communication.Properties;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class PropertyGetTopicHandler(MqttCommunicationProtocol communication)
        : DeviceRequestTopicHandler(typeof(GetPropertiesResponse), communication)
    {
        public const string PropertyGetTopicTemplate = "tylink/{0}/thing/property/desired/get";
        public const string PropertyGetResponseTopicTemplate = "tylink/{0}/thing/property/desired/get_response";

        protected override string SubscribableTopicTemplate => PropertyGetResponseTopicTemplate;

        protected override string PublishableTopicTemplate => PropertyGetTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new PropertyGetRequestHandler(Communication, responseHandler);
        }
    }
}
