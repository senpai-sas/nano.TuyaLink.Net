using System;
using System.Collections;
#nullable enable
namespace nano.SmartEnum
{
    public abstract class SmartEnum : IComparable
    {
        public string Name { get; }
        public object EnumValue { get; }

        public SmartEnum(string name, object value)
        {
            Name = name;
            EnumValue = value;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            if (obj is not SmartEnum other)
            {
                return false;
            }
            bool typeMatches = GetType().Equals(obj.GetType());
            bool valueMatches = EnumValue.Equals(other.EnumValue);
            return typeMatches && valueMatches;

        }

        public override int GetHashCode()
        {
            return EnumValue.GetHashCode();
        }

        public static bool operator ==(SmartEnum a, SmartEnum b)
        {
            return a.EnumValue == b.EnumValue;
        }

        public static bool operator !=(SmartEnum a, SmartEnum b)
        {
            return a.EnumValue != b.EnumValue;
        }

        

        protected static object? GetFromValue(object value, Type enumType, Hashtable store)
        {
            if (value == null)
            {
                return null;
            }
            if (store.TryGetValue(value, out object? result))
            {
                return result;
            }

            System.Reflection.FieldInfo[] fields = enumType.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
            result = null;
            foreach (System.Reflection.FieldInfo? field in fields)
            {
                if (field.FieldType == enumType || field.FieldType.IsSubclassOf(enumType))
                {
                    SmartEnum instance = (SmartEnum)field.GetValue(null);
                    store[instance.EnumValue] = instance;
                    if (Equals(value, instance.EnumValue))
                    {
                        result = instance;
                    }
                }
            }
            return result;
        }

        public int CompareTo(object obj)
        {
            if (obj is SmartEnum other)
            {
                if (other.EnumValue is IComparable comparableValue)
                {
                    return comparableValue.CompareTo(EnumValue);
                }
            }

            throw new ArgumentException("Cannot compare two values");
        }
    }
}
