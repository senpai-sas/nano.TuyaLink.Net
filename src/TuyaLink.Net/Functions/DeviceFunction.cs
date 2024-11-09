using System;

using TuyaLink.Model;

namespace TuyaLink.Functions
{
    /// <summary>
    /// Represents an abstract base class for device functions.
    /// </summary>
    public abstract class DeviceFunction
    {
        /// <summary>
        /// Gets the code of the device function.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the type of the device function.
        /// </summary>
        public FunctionType Type { get; private set; }

        /// <summary>
        /// Gets the associated Tuya device.
        /// </summary>
        protected TuyaDevice Device { get; }

        /// <summary>
        /// Gets the function model associated with the device function.
        /// </summary>
        internal FunctionModel? Model { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceFunction"/> class.
        /// </summary>
        /// <param name="code">The code of the device function.</param>
        /// <param name="type">The type of the device function.</param>
        /// <param name="device">The associated Tuya device.</param>
        /// <exception cref="ArgumentException">Thrown when the code is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when the type or device is null.</exception>
        public DeviceFunction(string code, FunctionType type, TuyaDevice device)
        {
            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException($"'{nameof(code)}' cannot be null or empty.", nameof(code));
            }

            Code = code;
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Device = device ?? throw new ArgumentNullException(nameof(device));
        }

        /// <summary>
        /// Binds the function model to the device function.
        /// </summary>
        /// <param name="model">The function model to bind.</param>
        protected void BindModel(FunctionModel model)
        {
            Model = model;
            CheckModel();
        }

        /// <summary>
        /// Checks the validity of the function model.
        /// </summary>
        /// <param name="action">An optional action to perform after validation.</param>
        /// <exception cref="TuyaLinkException">Thrown when the model is invalid.</exception>
        protected virtual void CheckModel(Action? action = null)
        {

            if(Device is null)
            {
                throw new ArgumentNullException(nameof(Device));
            }

            if(Device.Settings is null)
            {
                throw new ArgumentNullException(nameof(Device.Settings));
            }

            if (!Device.Settings.ValdiateModel)
            {
                return;
            }
            if (Model == null)
            {
                throw new TuyaLinkException($"The function {Code} has no model");
            }

            if (Model.Code != Code)
            {
                throw new TuyaLinkException($"Function code {Code} does't match with model code {Model.Code}");
            }

            if (Model.FunctionType != Type)
            {
                throw new TuyaLinkException($"Function type {Type} does't match with model type {Model.FunctionType}");
            }

            ValidateModel();

            action?.Invoke();
        }

        /// <summary>
        /// Validates the function model.
        /// </summary>
        /// <remarks>
        /// Overrides this method to provide custom validation logic.
        /// </remarks>
        protected virtual void ValidateModel()
        {

        }
    }
}
