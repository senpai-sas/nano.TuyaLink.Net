
using System;

using TuyaLink.Communication;
using TuyaLink.Communication.Firmware;
using TuyaLink.Mqtt.Handlers;

namespace TuyaLink.Mqtt.Topics
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
