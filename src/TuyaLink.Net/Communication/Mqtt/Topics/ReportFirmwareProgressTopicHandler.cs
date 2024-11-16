using TuyaLink.Communication;
using TuyaLink.Communication.Mqtt;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class ReportFirmwareProgressTopicHandler : MqttTopicHandler
    {
        public const string FirmwareReportTopicTemplate = "tylink/{0}/ota/progress/report";
        public ReportFirmwareProgressTopicHandler(MqttCommunicationProtocol communication) : base(typeof(FunctionResponse), communication)
        {
        }

        protected override string SubscribableTopicTemplate => string.Empty;

        protected override string PublishableTopicTemplate => FirmwareReportTopicTemplate;

        public override void HandleMessage(byte[] data)
        {
            throw new System.NotImplementedException("Thsi method does't receive any data");
        }
    }
}
