using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class BatchReportTopicHandler(MqttCommunicationProtocol communication) : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string BatchReportTopicTemplate = "tylink/{0}/thing/data/batch_report";

        public const string BatchReportResponseTopicTemplate = "tylink/{0}/thing/data/batch_report_response";

        protected override string SubscribableTopicTemplate => BatchReportResponseTopicTemplate;

        protected override string PublishableTopicTemplate => BatchReportTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
