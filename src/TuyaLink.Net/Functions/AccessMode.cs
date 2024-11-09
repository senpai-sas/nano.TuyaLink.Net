using System;
using System.Collections;

using nano.SmartEnum;

namespace TuyaLink.Functions
{
    public class AccessMode : SmartEnum
    {
        private static readonly Hashtable _store = [];

        /// <summary>
        /// The property can be send/set from the cloud.
        /// </summary>
        public bool CanSend { get; private set; }

        /// <summary>
        /// The property can be reported to the cloud.
        /// </summary>
        public bool CanReport { get; private set; }

        private AccessMode(string name, string value) : base(name, value)
        {
            _store[name] = this;
        }

        /// <summary>
        /// Access mode where the property can be both sent from the cloud and reported from the device.
        /// </summary>
        public static readonly AccessMode SendAndReport = new("Send And Report", "rw")
        {
            CanReport = true,
            CanSend = true,
        };

        /// <summary>
        /// Access mode where the property can only be reported from the device to the cloud.
        /// </summary>
        public static readonly AccessMode ReportOnly = new("Report Only", "wr")
        {
            CanSend = false,
            CanReport = true
        };

        /// <summary>
        /// Access mode where the property can only be sent from the cloud to the device.
        /// </summary>
        public static readonly AccessMode SendOnly = new("Send Only", "ro")
        {
            CanSend = true,
            CanReport = false
        };

        /// <summary>
        /// Retrieves an AccessMode from its value.
        /// </summary>
        /// <param name="value">The value of the AccessMode.</param>
        /// <returns>The corresponding AccessMode.</returns>
        public static AccessMode FromValue(string value)
        {
            object? result = GetFromValue(value, typeof(AccessMode), _store);
            if (result is null)
            {
                throw new NotImplementedException($"Unknown AccessMode value: {value}");
            }

            return (AccessMode)result;
        }
    }
}
