using System;

namespace TuyaLink.Firmware
{
    public class FirmwareLoadException : TuyaLinkException
    {
        public FirmwareLoadException()
        {
        }

        public FirmwareLoadException(string message) : base(message)
        {
        }

        public FirmwareLoadException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
