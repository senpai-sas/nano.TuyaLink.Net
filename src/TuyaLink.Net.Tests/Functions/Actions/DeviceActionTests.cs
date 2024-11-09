using System;
using System.Collections;

using nanoFramework.TestFramework;

using TuyaLink.Functions.Properties;
using TuyaLink.Model;

namespace TuyaLink.Functions.Actions
{
    [TestClass]
    internal class DeviceActionTests
    {
        private class TestDeviceAction : DeviceAction
        {
            public TestDeviceAction(string code, TuyaDevice device) : base(code, device) { }

            protected override ActionExecuteResult OnExecute(Hashtable inputParams)
            {
                return ActionExecuteResult.Success(inputParams);
            }
        }

        private static TuyaDevice _device;
        private static TestDeviceAction _action;

        [Setup]
        public void SetUp()
        {
            _device = FakeDevice.Default;
            _action = new TestDeviceAction("test_code", _device);
        }

        [TestMethod]
        public void Constructor_ShouldThrowArgumentException_WhenCodeIsNullOrEmpty()
        {
            Assert.ThrowsException(typeof(ArgumentException), () => new TestDeviceAction(null, _device));
            Assert.ThrowsException(typeof(ArgumentException), () => new TestDeviceAction(string.Empty, _device));
        }

        [TestMethod]
        public void Constructor_ShouldThrowArgumentNullException_WhenDeviceIsNull()
        {
            Assert.ThrowsException(typeof(ArgumentNullException), () => new TestDeviceAction("test_code", null));
        }

        [TestMethod]
        public void Model_ShouldReturnActionModel()
        {
            ActionModel model = new();
            _action.BindModel(model);
            Assert.AreEqual(model, _action.Model);
        }

        [TestMethod]
        public void Execute_ShouldThrowFunctionRuntimeException_WhenOutputParamsMismatch()
        {
            ActionModel model = new()
            {
                Code = "test_code",
                OutputParams = [new() {
                    Code = "param1", TypeSpec = new TypeSpecifications { Type = PropertyDataType.String } }]
            };

            TestDeviceAction action = new("test_code", FakeDevice.ValidateModelDevice);

            action.BindModel(model);

            Hashtable inputParams = new();
            Assert.ThrowsException(typeof(FunctionRuntimeException), () => action.Execute(inputParams));
        }

        [TestMethod]
        public void Execute_ShouldThrowFunctionRuntimeException_WhenOutputParamsCountMismatch()
        {
            ActionModel model = new()
            {
                Code = "test_code",
                OutputParams = [new() { Code = "param1", TypeSpec = new TypeSpecifications { Type = PropertyDataType.String } }]
            };

            TestDeviceAction action = new("test_code", FakeDevice.ValidateModelDevice);

            action.BindModel(model);

            Hashtable inputParams = new()
            {
                { "param1", "value1" },
                { "param2", "value2" }
            };
            Assert.ThrowsException(typeof(FunctionRuntimeException), () => action.Execute(inputParams));
        }

        [TestMethod]
        public void Execute_ShouldReturnSuccess_WhenOutputParamsMatch()
        {
            ActionModel model = new()
            {
                OutputParams = [new() { Code = "param1", TypeSpec = new TypeSpecifications { Type = PropertyDataType.String } }]
            };
            _action.BindModel(model);

            Hashtable inputParams = new()
            {
                { "param1", "value1" }
            };

            ActionExecuteResult result = _action.Execute(inputParams);

            Assert.IsTrue(result.Code.IsSuccess);
            Assert.AreEqual(inputParams, result.OutputParameters);
        }

        [TestMethod]
        public void BindModel_ShouldCallOnBindModel()
        {
            ActionModel model = new();
            _action.BindModel(model);
            Assert.AreEqual(model, _action.Model);
        }
    }
}
