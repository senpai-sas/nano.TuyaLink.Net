
using System;
using System.Collections;

using nano.SmartEnum;

using nanoFramework.Runtime.Native;

namespace TuyaLink.Firmware
{
    public class UpdateChannel : SmartEnum
    {
        private static readonly Hashtable _store = new(19);
        private UpdateChannel(string name, int value, RebootOption rebootOption) : base(name, value)
        {
            _store.Add(value, this);
            RebootOption = rebootOption;
        }

        public static readonly UpdateChannel Main = new(nameof(Main), 0, RebootOption.EnterProprietaryBooter);

        public static readonly UpdateChannel Bluetooht = new(nameof(Bluetooht), 1, RebootOption.ClrOnly);

        public static readonly UpdateChannel Zigbee = new(nameof(Zigbee), 3, RebootOption.ClrOnly);

        public static readonly UpdateChannel MCU = new(nameof(MCU), 9, RebootOption.ClrOnly);

        public RebootOption RebootOption { get; }

        public static UpdateChannel FromValue(int value)
        {
            return GetFromValue(value, typeof(UpdateChannel), _store) as UpdateChannel;

        }
        public static UpdateChannel RegisterChannel(string name, int value, RebootOption rebootOption)
        {
            if (value < 9 || value > 19)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Value {value} must be greater than or equal to 9 and less than or equal to 19.");
            }
            return new UpdateChannel(name, value, rebootOption);
        }
    }
}
