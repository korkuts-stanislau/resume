using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BinaryTrees
{
    public class IntWrapper : IComparable<IntWrapper>
    {
        public int Value { get; set; }
        public IntWrapper(int value)
        {
            Value = value;
        }

        public int CompareTo(IntWrapper other)
        {
            return Value - other.Value;
        }

        public override bool Equals(object obj)
        {
            if(obj is IntWrapper)
            {
                return Value.Equals(((IntWrapper)obj).Value);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
