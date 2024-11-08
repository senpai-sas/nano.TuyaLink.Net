using System.Collections;

using nano.SmartEnum;

namespace TuyaLink.Firmware
{
    public class BizType : SmartEnum
    {
        private static readonly Hashtable _store = new(2);

        private BizType(string name, string value) : base(name, value)
        {
            _store[value] = this;
        }

        public static readonly BizType Initial = new(nameof(Initial), "INIT");

        public static readonly BizType Update = new(nameof(Update), "UPDATE");

        public static BizType FromValue(string value)
        {
            return GetFromValue(value, typeof(BizType), _store) as BizType;
        }
    }
}
