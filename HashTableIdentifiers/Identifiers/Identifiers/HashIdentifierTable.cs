using Identifiers.HashTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.Identifiers
{
    public class HashIdentifierTable : IdentifierTable
    {
        HashTable<Identifier> _hashTable;
        public HashIdentifierTable(int tableSize, HashFunction function)
        {
            _hashTable = new HashTable<Identifier>(tableSize, function);
        }

        public override void Add(Identifier identifier)
        {
            _hashTable.Add(identifier);
        }

        public override Identifier FindIdentifierByKey(string key)
        {
            return _hashTable.Find(key);
        }
    }
}
