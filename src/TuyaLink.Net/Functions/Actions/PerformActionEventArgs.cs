using System;
using System.Collections;

namespace TuyaLink.Functions.Actions
{
    public class PerformActionEventArgs : EventArgs
    {
        public string ActionCode { get; }

        public Hashtable InputParameters { get; }

        public ActionExecuteResult Result { get; private set; }

        public PerformActionEventArgs(string actionCode, Hashtable inputParameters)
        {
            ActionCode = actionCode;
            InputParameters = inputParameters;
        }

        public void SetResult(ActionExecuteResult result)
        {
            Result = result;
        }

    }
}
