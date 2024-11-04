using TuyaLink.Communication;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class ReportPropertyTopicHandler(MqttCommunicationProtocol communication) 
        : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string ReportPropertyTopicTemplate = "tylink/{0}/thing/property/report";
        public const string ReportPropertyResponseTopicTemplate = "tylink/{0}/thing/property/report_response";

        protected override string SubscribableTopicTemplate => ReportPropertyResponseTopicTemplate;

        protected override string PublishableTopicTemplate => ReportPropertyTopicTemplate;

        protected override DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
