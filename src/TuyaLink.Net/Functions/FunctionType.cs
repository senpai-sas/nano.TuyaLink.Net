using nano.SmartEnum;

namespace TuyaLink.Functions
{
    public sealed class FunctionType : SmartEnum
    {
        private FunctionType(string name, int value) : base(name, value)
        {

        }
        /// <summary>
        /// The property type is used to define the continuous and queryable states of a device, which can represent one or several feature parameters. 
        /// A property can be read-write or read-only for data update and query. When a specific feature parameter is updated, the device can update the property accordingly. 
        /// For example, a light bulb might have properties like power state and brightness.
        /// <see cref="https://developer.tuya.com/en/docs/iot/Function-Definition?id=Kb4qgfeeshz58"/>
        /// </summary>
        public static readonly FunctionType Property = new("Property", 1);

        /// <summary>
        /// The action type is used to run complex tasks. 
        /// An action command is not intended to change the device property but directs the device to return a response. 
        /// For example, face recognition and picture transmission.
        /// </summary>
        public static readonly FunctionType Action = new("Service", 2);

        /// <summary>
        /// The event type is used to define live notification reported by a device, which requires external sensing and processing.
        /// Events work with message subscription or rule engines to respond as per the predefined logic. 
        /// For example, overheating alerts and fault alerts.
        /// </summary>
        public static readonly FunctionType Event = new("Event", 3);


    }
}
