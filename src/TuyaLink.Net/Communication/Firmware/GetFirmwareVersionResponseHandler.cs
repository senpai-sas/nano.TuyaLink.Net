using System;
using TuyaLink.Communication;

namespace TuyaLink.Communication.Firmware
{
    public class GetFirmwareVersionResponseHandler : ResponseHandler
    {
        internal GetFirmwareVersionResponseHandler(string messageId, bool acknowledgment) : base(messageId, acknowledgment)
        {
        }

        public new GetFirmwareVersionResponse WaitForAcknowledgeReport(int millisecondsTimeout = -1, bool exitContext = false)
        {
            return (GetFirmwareVersionResponse)base.WaitForAcknowledgeReport(millisecondsTimeout, exitContext);
        }
    }
}
