

using System;
using TuyaLink.Communication;

namespace TuyaLink.Communication.Mqtt
{
    [Serializable]
    public class TuyaMqttException : TuyaCommunicationException
    {
        public TuyaMqttException()
        {
        }

        public TuyaMqttException(string message) : base(message)
        {
        }

        public TuyaMqttException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
