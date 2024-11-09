using System.Collections;

namespace TuyaLink.Functions.Actions
{
    public class ActionExecuteResult : FunctionResult
    {
        private static readonly Hashtable _emptyOutputParameters = [];
        public Hashtable OutputParameters { get; }

        private ActionExecuteResult(StatusCode code, Hashtable outputParameters) : base(code)
        {
            OutputParameters = outputParameters;
        }

        public static ActionExecuteResult Success(Hashtable outputParameters)
        {
            return new ActionExecuteResult(StatusCode.Success, outputParameters);
        }

        public static ActionExecuteResult Failure(StatusCode code)
        {
            return new ActionExecuteResult(code, _emptyOutputParameters);
        }
    }
}
