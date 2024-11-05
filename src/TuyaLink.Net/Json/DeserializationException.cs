using System;

namespace TuyaLink.Json
{
    [Serializable]
    public class DeserializationException : Exception
    {
        public DeserializationException(string message) : base(message)
        {
        }
    }
}
