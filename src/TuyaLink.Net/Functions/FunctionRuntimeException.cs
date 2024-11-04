using System;


namespace TuyaLink.Functions
{
    public class FunctionRuntimeException : TuyaLinkException
    {
        public FunctionRuntimeException(StatusCode resultCode) => ResultCode = resultCode;

        public FunctionRuntimeException(StatusCode resultCode, string message) : base(message) => ResultCode = resultCode;

        public FunctionRuntimeException(StatusCode resultCode, string message, Exception innerException) : base(message, innerException) => ResultCode = resultCode;

        public StatusCode ResultCode { get; }
    }
}
