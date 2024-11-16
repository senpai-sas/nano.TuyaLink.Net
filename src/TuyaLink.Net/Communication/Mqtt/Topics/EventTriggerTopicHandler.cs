using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class EventTriggerTopicHandler(MqttCommunicationProtocol communication) : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string EventTriggerTopicTemplate = "tylink/{0}/thing/event/trigger";
        public const string EventTriggerResponseTopicTemplate = "tylink/{0}/thing/event/trigger_response";

        protected override string SubscribableTopicTemplate => EventTriggerResponseTopicTemplate;

        protected override string PublishableTopicTemplate => EventTriggerTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
