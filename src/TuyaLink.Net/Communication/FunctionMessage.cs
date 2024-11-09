using System;

namespace TuyaLink.Communication
{
    public class FunctionMessage
    {
        internal static string GetNextMessageId()
        {
            return Guid.NewGuid().To32String();
        }

        public string MsgId { get; set; }

        public TuyaDateTime Time { get; set; }
    }
}
