

using System;

namespace TuyaLink.Firmware
{
    internal class FirmwareUpdateException : TuyaLinkException
    {
        public FirmwareUpdateException(FirmwareUdpateError error)
        {
            Error = error;
        }

        public FirmwareUpdateException(FirmwareUdpateError error, string message) : base(message)
        {
            Error = error;
        }

        public FirmwareUpdateException(FirmwareUdpateError error, string message, Exception innerException) : base(message, innerException)
        {
            Error = error;
        }

        public FirmwareUdpateError Error { get; }
    }
}
