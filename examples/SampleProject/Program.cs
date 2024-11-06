using System;
using System.Device.Wifi;
using System.Diagnostics;
using System.Threading;

using nanoFramework.Networking;
using TuyaLink.Firmware;
using TuyaLink.Functions;
using TuyaLink.Functions.Properties;

namespace TuyaLink
{
    public class Program
    {
        public static void Main()
        {

            try
            {
                FirmwareLoader.Run(new ESP32Device());
            }
            catch (FirmwareLoadException)
            {

                throw;
            }

            // Setup ESP32 I2C port.
            //Configuration.SetPinFunction(Gpio.IO06, DeviceFunction.I2C1_DATA);
            //Configuration.SetPinFunction(Gpio.IO05, DeviceFunction.I2C1_CLOCK);

            // Setup Mcp7940m device. 
            //I2cConnectionSettings i2cSettings = new I2cConnectionSettings(1, Mcp7940m.DefaultI2cAddress);
            //I2cDevice i2cDevice = new I2cDevice(i2cSettings);

            //Mcp7940m clock = new Mcp7940m(i2cDevice, ClockSource.ExternalCrystal);

            //const ushort fullStepsPerRotation = 200;

            //var motor = new A4988(48, 47, Microsteps.FullStep, fullStepsPerRotation, TimeSpan.Zero);

            //clock.SetTime(DateTime.UtcNow);

            SetupAndConnectNetwork();

            Sntp.UpdateNow();

            Debug.WriteLine("Hello from nanoFramework!" + DateTime.UtcNow);


            var info = new DeviceInfo("3lawjvgon7e7iczi", "269156232255a7a6a5et3j0", "rI42QC69TeWOgfq3");

            var tuyaDevice = new TuyaDevice(info);

            var property = new DelegateDeviceProperty("device_status", tuyaDevice, PropertyDataType.String, (newValue, oldValue) =>
            {
                Debug.WriteLine($"Property updated to {newValue} from {oldValue}");
            });

            tuyaDevice.AddProperty(property);

            tuyaDevice.Connect();

            tuyaDevice.GetProperties(property).WaitForAcknowledgeReport();

            //var response = property.Report();

            //var report = response.WaitForAcknowledgeReport();
            Thread.Sleep(Timeout.Infinite);



            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }


        /// <summary>
        /// This is a helper function to pick up first available network interface and use it for communication.
        /// </summary>
        private static void SetupAndConnectNetwork()
        {
            WifiNetworkHelper.ConnectDhcp("Red#1", "isa0124.", WifiReconnectionKind.Automatic, requiresDateTime: true);
            WifiNetworkHelper.SetupNetworkHelper(requiresDateTime: true);
            Debug.WriteLine($"Wifi network status {WifiNetworkHelper.Status}");
        }
    }

    public class ESP32Device : ITargetDevice
    {
        public ESP32Device()
        {
        }

        public bool HasLowBattery()
        {
            return false;
        }

        public void InternetConnect()
        {
            WifiNetworkHelper.SetupNetworkHelper(requiresDateTime: true);
        }
    }
}
