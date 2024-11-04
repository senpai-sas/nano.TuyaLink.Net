using System.Collections;

namespace TuyaLink.Communication.Actions
{

    public class ActionData
    {
        public string ActionCode { get; set; }
    }

    public class InputActionData : ActionData
    {
        public Hashtable InputParams { get; set; }
    }

    public class OutputActionData : ActionData
    {
        public Hashtable OutputParams { get; set; }
    }
}
