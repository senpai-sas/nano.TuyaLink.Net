using TuyaLink.Communication;
using TuyaLink.Communication.Properties;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class PropertyGetTopicHandler(MqttCommunicationProtocol communication) 
        : DeviceRequestTopicHandler(typeof(GetPropertiesResponse), communication)
    {
        public const string PropertyGetTopicTemplate = "tylink/{0}/thing/property/get";
        public const string PropertyGetResponseTopicTemplate = "tylink/{0}/thing/property/get_response";

        protected override string SubscribableTopicTemplate => PropertyGetResponseTopicTemplate;

        protected override string PublishableTopicTemplate => PropertyGetTopicTemplate;

        protected override DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new PropertyGetRequestHandler(Communication, responseHandler);
        }
    }
}
