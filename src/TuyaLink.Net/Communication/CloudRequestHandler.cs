namespace TuyaLink.Communication
{
    internal abstract class CloudRequestHandler
    {
        public abstract void HandleMessage(FunctionMessage message);
    }
}
