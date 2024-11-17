using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class HistoryReportTopicHandler(MqttCommunicationProtocol communication)
        : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string HistoryReportTopicTemplate = "tylink/{0}/thing/data/history_report";
        public const string HistoryReportResponseTopicTemplate = "tylink/{0}/thing/data/history_report_response";

        protected override string SubscribableTopicTemplate => HistoryReportResponseTopicTemplate;
        protected override string PublishableTopicTemplate => HistoryReportTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
