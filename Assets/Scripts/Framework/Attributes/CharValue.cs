using System;

namespace Framework.Attributes
{
    public sealed class CharValue : Attribute
    {
        public char Value { get; }

        public CharValue(char value) => Value = value;
    }
}