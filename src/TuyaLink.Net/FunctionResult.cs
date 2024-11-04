
using TuyaLink.Functions;

namespace TuyaLink
{
    public class FunctionResult 
    {
        public StatusCode Code { get; private set; }

        public FunctionResult(StatusCode code)
        {
            Code = code;
        }
    }
}
