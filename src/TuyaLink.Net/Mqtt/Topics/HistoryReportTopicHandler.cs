using TuyaLink.Communication;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
{
    internal class HistoryReportTopicHandler(MqttCommunicationProtocol communication)
        : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string HistoryReportTopicTemplate = "tylink/{0}/thing/data/history_report";
        public const string HistoryReportResponseTopicTemplate = "tylink/{0}/thing/data/history_report_response";

        protected override string SubscribableTopicTemplate => HistoryReportTopicTemplate;
        protected override string PublishableTopicTemplate => HistoryReportResponseTopicTemplate;

        protected override DevieRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new DefaultRequestHandler(Communication, responseHandler);
        }
    }
}
