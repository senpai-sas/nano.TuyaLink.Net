using System;
namespace TuyaLink
{
    [Serializable]
    public class TuyaLinkException : Exception
    {
        public TuyaLinkException()
        {
        }

        public TuyaLinkException(string message) : base(message)
        {
        }

        public TuyaLinkException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
