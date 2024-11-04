using System;

using TuyaLink.Communication;
using TuyaLink.Communication.Firmware;

namespace TuyaLink
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
