namespace TuyaLink.Functions
{
    internal interface IAcknowledgeable
    {

        /// <summary>
        /// By default, the cloud does not respond to a property reporting message. You can set the ack parameter to enable acknowledgment.
        /// false: No acknowledgment returned.This is the default value.
        /// true: Acknowledgment returned.
        /// </summary>
        bool Acknowledge { get; }
    }
}
