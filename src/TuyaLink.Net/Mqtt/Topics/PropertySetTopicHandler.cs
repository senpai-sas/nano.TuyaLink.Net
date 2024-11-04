using TuyaLink.Communication;
using TuyaLink.Communication.Properties;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class PropertySetTopicHandler(MqttCommunicationProtocol communication) 
        : CloudRequestTopicHandler(typeof(PropertySetRequest), communication)
    {
        public const string PropertySetTopicTemplate = "tylink/{0}/thing/property/set";
        public const string PropertySetResponseTopicTemplate = "tylink/{0}/thing/property/set_response";

        protected override string SubscribableTopicTemplate => PropertySetTopicTemplate;

        protected override string PublishableTopicTemplate => PropertySetResponseTopicTemplate;

        protected override CloudRequestHandler CreateCloudRequestHandler()
        {
            return new PropertySetRequestHandler(this, Communication);
        }
    }
}
