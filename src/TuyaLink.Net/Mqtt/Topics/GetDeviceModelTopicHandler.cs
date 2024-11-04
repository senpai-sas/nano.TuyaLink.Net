
using TuyaLink.Communication;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class GetDeviceModelTopicHandler(MqttCommunicationProtocol communication) 
        : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string GetDeviceModelTopicTemplate = "tylink/{0}/thing/model/get";
        public const string GetDeviceModelResponseTopicTemplate = "tylink/{0}/thing/model/get_response";

        protected override string SubscribableTopicTemplate => GetDeviceModelResponseTopicTemplate;

        protected override string PublishableTopicTemplate => GetDeviceModelTopicTemplate;

        protected override DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new GetDeviceModelHandler(Communication, responseHandler);
        }
    }

}
