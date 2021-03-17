using Identifiers.HashTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.Identifiers
{
    public enum IdentifierType
    {
        Int32,
        Double,
        String
    }
    public class Identifier : IComparable<Identifier>, IHashable, IKeyable
    {
        public string Key { get; set; }
        public IdentifierType Type { get; set; }
        public object Value { get; set; }
        
        public int CompareTo(Identifier other)
        {
            return Key.CompareTo(other.Key);
        }

        public int GetHash(HashFunction function)
        {
            return function.GetHashValue(Key);
        }
        public override string ToString()
        {
            return $"{Type} {Key} = {Value}";
        }
    }
}
