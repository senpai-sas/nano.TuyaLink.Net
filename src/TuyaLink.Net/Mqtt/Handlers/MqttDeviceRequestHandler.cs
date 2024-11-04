using System.Diagnostics;
using TuyaLink.Communication;

namespace TuyaLink.Mqtt.Handlers
{
    internal abstract class MqttDeviceRequestHandler(MqttCommunicationProtocol communication, ResponseHandler responseHandler)
        : DevieRequestHandler(responseHandler)
    {
        protected MqttCommunicationProtocol Communication { get; } = communication;

        public virtual void Published(ulong messageId)
        {
            Debug.WriteLine($"Request with MessageId: {ResponseHandler.MessageId} has been published with MQTT id: {messageId}");
        }
    }
}
