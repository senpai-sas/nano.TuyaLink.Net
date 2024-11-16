using TuyaLink.Communication;
using TuyaLink.Communication.Mqtt;
using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class DeleteDesiredPropertiesTopicHandler : DeviceRequestTopicHandler
    {
        public const string DeleteDesiredPropertiesTopicTemplate = "tylink/{0}/thing/property/desired/delete";
        public const string DeleteDesiredPropertiesResponseTopicTemplate = "tylink/{0}/thing/property/desired/delete_response";

        public DeleteDesiredPropertiesTopicHandler(MqttCommunicationProtocol communication) : base(typeof(FunctionResponse), communication)
        {
        }

        protected override string SubscribableTopicTemplate => DeleteDesiredPropertiesResponseTopicTemplate;

        protected override string PublishableTopicTemplate => DeleteDesiredPropertiesTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
