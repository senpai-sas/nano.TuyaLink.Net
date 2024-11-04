using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace TuyaLink.Mqtt
{
    public class TuyaMqttSign
    {

        /// <summary>
        /// Mqtt username
        /// </summary>
        public string Username { get; private set; } = "";

        /// <summary>
        /// Mqtt password
        /// </summary>
        public string Password { get; private set; } = "";

        /// <summary>
        /// Mqtt client
        /// </summary>
        public string ClientId { get; private set; } = "";
        /// <summary>
        ///  Calculate mqtt connection parameters
        /// </summary>
        /// <param name="productId">The product id</param>
        /// <param name="deviceId"></param>
        /// <param name="deviceSecret"></param>
        public void Calculate(string productId, string deviceId, string deviceSecret)
        {
            if (productId == null || deviceId == null || deviceSecret == null)
            {
                return;
            }
            try
            {
                long timestamp = DateTime.UtcNow.ToUnixTimeSeconds();
                //MQTT username
                Username = deviceId + "|signMethod=hmacSha256,timestamp=" + timestamp + ",secureMode=1,accessType=1";
                //MQTT clientId
                ClientId = "tuyalink_" + deviceId;
                string plainPasswd = "deviceId=" + deviceId + ",timestamp=" + timestamp + ",secureMode=1,accessType=1";
                Console.WriteLine("plainPasswd= " + plainPasswd);
                //MQTT password
                Password = HmacSha256(plainPasswd, deviceSecret);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

        public static string HmacSha256(string plainText, string key)
        {
            // Zero filling less than 64 characters
            return Hmac(plainText, key);
        }

        private static string Hmac(string plainText, string key)
        {
            if (plainText == null || key == null)
            {
                return null;
            }
            var keyBytes = Encoding.UTF8.GetBytes(key);
            using HMACSHA256 hmac = new(keyBytes);
            var painBytes = Encoding.UTF8.GetBytes(plainText);
            var hash = hmac.ComputeHash(painBytes);

            return hash.ToExeString();
        }
    }
}