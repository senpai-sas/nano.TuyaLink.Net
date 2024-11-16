using TuyaLink.Communication;
using TuyaLink.Communication.Mqtt;

namespace TuyaLink.Communication.Mqtt.Handlers
{
    internal abstract class MqttCloudRequestHandler(
        MqttTopicHandler mqttTopicHandler,
        MqttCommunicationProtocol communication)
        : CloudRequestHandler
    {
        public MqttTopicHandler TopicHandler { get; } = mqttTopicHandler;

        public MqttCommunicationProtocol Communication { get; } = communication;
    }
}
