using TuyaLink.Communication;
using TuyaLink.Communication.Actions;
using TuyaLink.Communication.Mqtt;
using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class ActionExecuteTopicHandler(MqttCommunicationProtocol communication)
        : CloudRequestTopicHandler(typeof(ActionExecuteRequest), communication)
    {
        public const string ActionExecuteTopicTemplate = "tylink/{0}/thing/action/execute";

        public const string ActionExecuteResponseTopicTemplate = "tylink/{0}/thing/action/execute_response";

        protected override string SubscribableTopicTemplate => ActionExecuteTopicTemplate;

        protected override string PublishableTopicTemplate => ActionExecuteResponseTopicTemplate;

        protected override CloudRequestHandler CreateCloudRequestHandler()
        {
            return new ActionExecuteRequestHandler(this, Communication);
        }
    }
}
