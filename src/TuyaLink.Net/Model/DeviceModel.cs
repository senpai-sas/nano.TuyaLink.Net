using TuyaLink.Functions;

namespace TuyaLink.Model
{
    public class DeviceModel
    {
        public string ModelId { get; set; }

        public ModelService[] Services { get; set; }
    }


    public class ModelService
    {
        public string Code { get; set; }

        public PropertyModel[] Properties { get; set; }

        public EventModel[] Events { get; set; }

        public ActionModel[] Actions { get; set; }
    }


    public class FunctionModel
    {
        public string AbilityId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Code}) | {GetType().Name}";
        }
    }

    public class PropertyModel : FunctionModel
    {
        public AccessMode AccessMode { get; set; }
        public TypeSpecifications TypeSpec { get; set; }
    }

    public class TypeSpecifications
    {
        public TuyaDataType Type { get; set; }

        public string[] Label { get; set; }

        public string[] Range { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public float Step { get; set; }

        public string Unit { get; set; }

        public float Scale { get; set; }

        public int Maxlen { get; set; } = -1;
    }

    public class EventModel : FunctionModel
    {
        public ParameterModel[] OutputParams { get; set; }
    }

    public class ParameterModel
    {
        public string Code { get; set; }
        public TypeSpecifications TypeSpec { get; set; }
    }

    public class ActionModel : FunctionModel
    {
        public ParameterModel[] OutputParams { get; set; }

        public ParameterModel[] InputParams { get; set; }
    }
}
