
using TuyaLink.Communication;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
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
