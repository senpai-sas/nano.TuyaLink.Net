﻿using TuyaLink.Communication.Mqtt.Handlers;

namespace TuyaLink.Communication.Mqtt.Topics
{
    internal class GetDeviceModelTopicHandler(MqttCommunicationProtocol communication)
        : DeviceRequestTopicHandler(typeof(FunctionResponse), communication)
    {
        public const string GetDeviceModelTopicTemplate = "tylink/{0}/thing/model/get";
        public const string GetDeviceModelResponseTopicTemplate = "tylink/{0}/thing/model/get_response";

        protected override string SubscribableTopicTemplate => GetDeviceModelResponseTopicTemplate;

        protected override string PublishableTopicTemplate => GetDeviceModelTopicTemplate;

        protected override DeviceRequestHandler CreateRequestHandler(ResponseHandler responseHandler)
        {
            return new GetDeviceModelHandler(Communication, responseHandler);
        }
    }

}
