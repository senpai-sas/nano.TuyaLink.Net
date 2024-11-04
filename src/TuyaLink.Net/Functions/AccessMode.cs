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
        public bool CanRead { get; private set; }

        /// <summary>
        /// The property can be written to the cloud
        /// </summary>
        public bool CanWrite { get; private set; }

        private AccessMode(string name, string value) : base(name, value)
        {

        }

        public static readonly AccessMode ReadWrite = new("ReadWrite", "rw")
        {
            CanWrite = true,
            CanRead = true,
        };

        public static readonly AccessMode WriteOnly = new("WriteOnly", "wr")
        {
            CanRead = false,
            CanWrite = true
        };

        public static readonly AccessMode ReadOnly = new("ReadOnly", "ro")
        {
            CanRead = true,
            CanWrite = false
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
