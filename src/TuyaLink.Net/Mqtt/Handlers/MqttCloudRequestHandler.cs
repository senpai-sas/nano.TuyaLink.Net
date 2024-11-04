using TuyaLink.Communication;

namespace TuyaLink.Mqtt.Handlers
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
