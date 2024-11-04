using System;
using System.Runtime.CompilerServices;

namespace nano.TuyaLink.Bootloader
{
    public class TuyaBootloader
    {
        [MethodImpl(MethodImplOptions.InternalCall)]
        public void Update()
        {
            Console.WriteLine("Updating Tuya Bootloader");
        }

        public void Reboot()
        {
            Console.WriteLine("Rebooting Tuya Bootloader");
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public static extern string EnterProprietaryBooter();
    }
}
