﻿using System.Diagnostics;

using TuyaLink.Functions;
using TuyaLink.Functions.Properties;

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


    public abstract class FunctionModel
    {
        public FunctionType FunctionType { get; protected set; }
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
        public PropertyModel()
        {
            FunctionType = FunctionType.Property;
        }
        public AccessMode AccessMode { get; set; }
        public TypeSpecifications TypeSpec { get; set; }
    }

    public class TypeSpecifications
    {
        public PropertyDataType Type { get; set; }

        public string[] Label { get; set; } = [];

        public string[] Range { get; set; } = [];

        public double Min { get; set; }

        public double Max { get; set; }

        public float Step { get; set; }

        public string Unit { get; set; } = string.Empty;

        public float Scale { get; set; }

        public int Maxlen { get; set; } = -1;

        public bool IsInBoundary(double value) => value >= Min && value <= Max;

        internal void CheckCouldValue(object value)
        {
            Type.CheckCouldValue(this, value);
        }

        public override string ToString()
        {
            return $"Type: {Type}, Label: {Label}, Range: {Range}, Min: {Min}, Max: {Max}, Step: {Step}, Unit: {Unit}, Scale: {Scale}, Maxlen: {Maxlen}";
        }
    }

    public class EventModel : FunctionModel
    {
        public EventModel()
        {
            FunctionType = FunctionType.Event;
        }
        public ParameterModel[] OutputParams { get; set; }
    }

    [DebuggerDisplay("{Code}")]
    public class ParameterModel
    {
        public string Code { get; set; }

        public TypeSpecifications TypeSpec { get; set; }

        public override string ToString()
        {
            return $"{{Code: {Code}, Specs: {{{TypeSpec}}}}}";
        }
    }

    public class ActionModel : FunctionModel
    {
        public ActionModel()
        {
            FunctionType = FunctionType.Action;
        }
        public ParameterModel[] OutputParams { get; set; }

        public ParameterModel[] InputParams { get; set; }
    }
}
