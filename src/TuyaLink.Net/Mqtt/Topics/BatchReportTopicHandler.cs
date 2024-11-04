
using TuyaLink.Communication;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class BatchReportTopicHandler(MqttCommunicationProtocol communication) : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string BatchReportTopicTemplate = "tylink/{0}/thing/property/batch_report";

        public const string BatchReportResponseTopicTemplate = "tylink/{0}/thing/property/batch_report_response";

        protected override string SubscribableTopicTemplate => BatchReportResponseTopicTemplate;

        protected override string PublishableTopicTemplate => BatchReportTopicTemplate;

        protected override DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
