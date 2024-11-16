using TuyaLink.Communication.Firmware;
using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class FirmwareReportTopicHandler(MqttCommunicationProtocol communication)
        : DeviceRequestTopicHandler(typeof(FirmwareIssueRequest), communication)
    {
        public const string FirmwareReportTopicTemplate = "tylink/{0}/ota/firmware/report";

        public const string FirmwareIssueTopicTemplate = "tylink/{0}/ota/firmware/issue";
        protected override string SubscribableTopicTemplate { get; } = string.Empty;

        protected override string PublishableTopicTemplate => FirmwareReportTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new FirmwareIssueRequestHandler(Communication, responseHandler);
        }

    }
}
