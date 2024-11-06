using System.Collections;

using nano.SmartEnum;

namespace TuyaLink.Functions
{
    public class AccessMode : SmartEnum
    {
        private static readonly Hashtable _store = [];

        /// <summary>
        /// The property can be readed from the could
        /// </summary>
        public bool CanSend { get; private set; }

        /// <summary>
        /// The property can be written to the cloud
        /// </summary>
        public bool CanReport { get; private set; }

        private AccessMode(string name, string value) : base(name, value)
        {

        }

        public static readonly AccessMode SendAndReport = new("Send And Report", "rw")
        {
            CanReport = true,
            CanSend = true,
        };

        public static readonly AccessMode ReportOnly = new("Report Only", "wr")
        {
            CanSend = false,
            CanReport = true
        };

        public static readonly AccessMode SendOnly = new("Send Only", "ro")
        {
            CanSend = true,
            CanReport = false
        };

        static AccessMode RegisterEnumValue(AccessMode accessMode)
        {
            _store.Add(accessMode.EnumValue, accessMode);
            return accessMode;
        }

        public static AccessMode FromValue(string value)
        {
            return (AccessMode)GetFromValue(value, typeof(AccessMode), _store);
        }
    }
}
