using System;

namespace TuyaLink.Communication
{
    [Serializable]
    public class TuyaCommunicationException : TuyaLinkException
    {
        public TuyaCommunicationException()
        {
        }

        public TuyaCommunicationException(string message) : base(message)
        {
        }

        public TuyaCommunicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
