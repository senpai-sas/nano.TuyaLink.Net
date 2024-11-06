using System;
using System.Text;

namespace TuyaLink.Model
{
    public class ModelUpdatedEventArgs : EventArgs
    {
        public DeviceModel Model { get; set; }
    }
}
