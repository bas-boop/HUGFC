using System;

namespace Framework.Attributes
{
    public sealed class StringValue : Attribute
    {
        public string Value { get; }

        public StringValue(string value) => Value = value;
    }
}