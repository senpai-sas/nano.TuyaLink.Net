

using System;

using nanoFramework.M2Mqtt.Messages;

namespace TuyaLink.Communication.Mqtt
{
    [Serializable]
    public class TuyaMqttConnectionException : TuyaMqttException
    {
        public MqttReasonCode ReasonCode { get; }

        public TuyaMqttConnectionException(MqttReasonCode reasonCode) => ReasonCode = reasonCode;

        public TuyaMqttConnectionException(string message, MqttReasonCode reasonCode) : base(message) => ReasonCode = reasonCode;

        public TuyaMqttConnectionException(string message, Exception innerException, MqttReasonCode reasonCode) : base(message, innerException) => ReasonCode = reasonCode;

    }
}
