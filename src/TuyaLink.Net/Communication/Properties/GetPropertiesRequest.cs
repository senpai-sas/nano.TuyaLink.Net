namespace TuyaLink.Communication.Properties
{
    internal class GetPropertiesRequest : FunctionRequest
    {
        public GetPropertiesRequestData Data { get; set; } = new GetPropertiesRequestData();
    }

    internal class GetPropertiesRequestData
    {
        public string[] Properties { get; set; }
    }
}
