namespace TuyaLink.Properties
{
    public class PropertyUpdatedEventArg(object newValue, object oldValue)
    {
        public object NewValue { get; } = newValue;

        public object OldValue { get; } = oldValue;
    }
}
