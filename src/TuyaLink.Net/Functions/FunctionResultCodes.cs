using System.Collections;

using nano.SmartEnum;

namespace TuyaLink.Functions
{
    public static class FunctionResultCodes
    {
        /// <summary>
        /// Success result
        /// </summary>
        public const int Success = 0;

        public static class TuyaLinNetCodes
        {
            /// <summary>
            /// Unknown error as been occurred while processing the request.
            /// </summary>
            public const int UnknownError = -10000;

            /// <summary>
            /// The function was not found in the device.
            /// Used when the device does not have the function requested by the cloud.
            /// </summary>
            /// <remarks>
            /// This happen when the device does not support the function or the function is not implemented.
            /// </remarks>
            public const int FunctionNotFound = -10001;
        }
    }

    /// <summary>
    /// Represents various status codes for function results.
    /// </summary>
    public class StatusCode : SmartEnum
    {
        private static readonly Hashtable _store = new(15);

        private StatusCode(int value, string name) : base(name, value)
        {
            Description = name;
            _store[value] = this;
        }

        public string Description { get; private set; } = string.Empty;

        public bool IsSuccess { get; private set; }

        /// <summary>
        /// The request was successfully processed.
        /// </summary>
        public static readonly StatusCode Success = new(0, nameof(Success))
        {
            Description = "The request was successfully processed.",
            IsSuccess = true
        };


        /// <summary>
        /// Unknown error as been occurred while processing the request.
        /// </summary>
        public static readonly StatusCode UnknownError = new(-10000, nameof(UnknownError))
        {
            Description = "Unknown error as been occurred while processing the request."
        };

        /// <summary>
        /// The function was not found in the device.
        /// </summary>
        public static readonly StatusCode FunctionNotFound = new(-10001, nameof(FunctionNotFound))
        {
            Description = "The function was not found in the device.",
        };

        public static readonly StatusCode InvalidValueError = new(-10002, nameof(InvalidValueError))
        {
            Description = "The value is invalid for the function."
        };

        public static readonly StatusCode FunctionOutputParameterMismatch = new(-10002, nameof(InvalidValueError))
        {
            Description = "The output parameter of the function does't match with the model"
        };

        public static readonly StatusCode ModelNotBinded = new(-10003, nameof(ModelNotBinded))
        {
            Description = "The model is not binded to the function."
        };

        public static readonly StatusCode FunctionCodeMismatch = new(-10004, nameof(FunctionCodeMismatch))
        {
            Description = "The function code does not match with the model."
        };

        public static readonly StatusCode FunctionTypeMismatch = new(-10005, nameof(FunctionTypeMismatch))
        {
            Description = "The function type does not match with the model."
        };


        /// <summary>
        /// Service error.
        /// </summary>

        public static readonly StatusCode ServiceError = new(1001, nameof(ServiceError))
        {
            Description = "Service error."
        };

        /// <summary>
        /// Invalid parameter.
        /// </summary>
        public static readonly StatusCode InvalidParameter = new(1002, nameof(InvalidParameter))
        {
            Description = "Invalid parameter."
        };

        /// <summary>
        /// Data format error.
        /// </summary>
        public static readonly StatusCode DataFormatError = new(1003, nameof(DataFormatError))
        {
            Description = "Data format error."
        };

        /// <summary>
        /// Device does not exist.
        /// </summary>
        public static readonly StatusCode DeviceNotFound = new(1004, nameof(DeviceNotFound))
        {
            Description = "Device does not exist."
        };

        /// <summary>
        /// The device is offline.
        /// </summary>
        public static readonly StatusCode DeviceOffline = new(2001, nameof(DeviceOffline))
        {
            Description = "The device is offline."
        };

        /// <summary>
        /// The model associated with the device is not defined.
        /// </summary>
        public static readonly StatusCode ModelNotDefined = new(2002, nameof(ModelNotDefined))
        {
            Description = "The model associated with the device is not defined."
        };

        /// <summary>
        /// The event associated with the device is not defined.
        /// </summary>
        public static readonly StatusCode EventNotDefined = new(2004, nameof(EventNotDefined))
        {
            Description = "The event associated with the device is not defined."
        };

        /// <summary>
        /// Incorrect data check.
        /// </summary>
        public static readonly StatusCode IncorrectDataCheck = new(2006, nameof(IncorrectDataCheck))
        {
            Description = "Incorrect data check."
        };

        /// <summary>
        /// Some of the data reported in bulk/historical fails to be processed.
        /// </summary>
        public static readonly StatusCode PartialDataError = new(2121, nameof(PartialDataError))
        {
            Description = "Some of the data reported in bulk/historical fails to be processed."
        };

        /// <summary>
        /// All the data reported in bulk/historical fails to be processed.
        /// </summary>
        public static readonly StatusCode AllDataError = new(2122, nameof(AllDataError))
        {
            Description = "All the data reported in bulk/historical fails to be processed."
        };

        /// <summary>
        /// The number of sub-devices to be reported in bulk/historical exceeds the limit of 20.
        /// </summary>
        public static readonly StatusCode SubDevicesLimitExceeded = new(2123, nameof(SubDevicesLimitExceeded))
        {
            Description = "The number of sub-devices in the bulk/historical request exceeds the limit."
        };

        /// <summary>
        /// The size of historical data exceeds the limit of 500.
        /// </summary>
        public static readonly StatusCode HistoricalDataExceeded = new(2121, nameof(HistoricalDataExceeded))
        {
            Description = "The size of historical data exceeds the limit of 500."
        };

        public static StatusCode? FromValue(int value)
        {
            return (StatusCode?)GetFromValue(value, typeof(StatusCode), _store);
        }

        public override string ToString()
        {
            return $"{EnumValue} - {Description}";
        }

        public static StatusCode CreateCustom(int value, string name, string description)
        {
            StatusCode? status = FromValue(value);
            return status ??= new StatusCode(value, name)
            {
                Description = description
            };
        }
    }
}
