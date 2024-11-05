using TuyaLink.Functions;

namespace TuyaLink.Communication
{
    public class FunctionResponse : FunctionMessage
    {
        public StatusCode Code { get; set; } = StatusCode.Success;

        public override string ToString()
        {
            return $"{{\"messageId\":\"{MsgId}\",\"time\":{Time},\"code\":{Code}}}";
        }
    }
}
