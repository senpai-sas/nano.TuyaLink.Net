
using System.Collections;

using nano.SmartEnum;

namespace TuyaLink
{
    public class DataCenter : SmartEnum
    {
        private static readonly Hashtable _store = new(6);

        public int Port { get; }
        public string Url { get; }

        private DataCenter(string name, string url, int port, int value) : base(name, value)
        {
            Url = url;
            Port = port;
            _store[value] = this;
        }

        public static readonly DataCenter China = new("China", "m1.tuyacn.com", 8883, 1);
        public static readonly DataCenter CentralEurope = new("Central Europe", "m1.tuyaeu.com", 8883, 2);
        public static readonly DataCenter WensterAmerica = new("Western America", "m1.tuyaus.com", 8883, 3);
        public static readonly DataCenter EasternAmerica = new("Eastern America", "m1-ueaz.tuyaus.com", 8883, 4);
        public static readonly DataCenter WesternEurope = new("Western Europe", "m1-weaz.tuyaeu.com", 8883, 5);
        public static readonly DataCenter India = new("India", "m1.tuyain.com", 8883, 6);

        public static DataCenter FromValue(int value)
        {
            return (DataCenter)GetFromValue(value, typeof(DataCenter), _store);
        }
    }
}
