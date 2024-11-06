using System;

namespace TuyaLink.Functions.Properties
{
    public class PropertyUpdatedEventArgs(object newValue, object oldValue) : EventArgs
    {
        public object NewValue { get; } = newValue;

        public object OldValue { get; } = oldValue;
    }
}
