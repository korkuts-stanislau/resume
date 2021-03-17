using Identifiers.Trees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.Identifiers
{
    public class TreeIdentifierTable : IdentifierTable, IEnumerable<Identifier>
    {
        IdentifierSearchTree _searchTree;
        public TreeIdentifierTable()
        {
            _searchTree = new IdentifierSearchTree();
        }
        public override void Add(Identifier identifier)
        {
            try
            {
                _searchTree.FindIdentifier(identifier.Key);
            }
            catch
            {
                _searchTree.Add(identifier);
                return;
            }
            throw new Exception("Уже есть идентификатор с таким именем");
        }
        public override Identifier FindIdentifierByKey(string key)
        {
            return _searchTree.FindIdentifier(key);
        }

        public IEnumerator<Identifier> GetEnumerator()
        {
            return _searchTree.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
