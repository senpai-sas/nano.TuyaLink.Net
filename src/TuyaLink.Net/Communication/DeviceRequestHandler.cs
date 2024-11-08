﻿
using System.Collections;
using System.Diagnostics;

namespace TuyaLink.Communication
{
    internal abstract class DeviceRequestHandler(ResponseHandler responseHandler)
    {
        public ResponseHandler ResponseHandler { get; } = responseHandler;
        public abstract void HandleMessage(FunctionMessage message);

        protected void AcknowledgeResponse(FunctionResponse response)
        {
            Debug.WriteLine($"Aknowloging property, {response}");
            ResponseHandler.Acknowledge(response);
        }
    }
}
