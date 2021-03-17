using Identifiers.Identifiers;
using Identifiers.Trees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.Trees
{
    public class IdentifierSearchTree : NodeTree<Identifier>
    {
        public Identifier FindIdentifier(string key)
        {
            return FindIdentifier(key, _root);
        }
        private Identifier FindIdentifier(string key, Node<Identifier> currentNode)
        {
            if(currentNode == null)
            {
                throw new Exception("Нет идентификатора с таким именем");
            }
            int result = key.CompareTo(currentNode.Data.Key);
            if (result == 0)
            {
                return currentNode.Data;
            }
            else if (result < 0)
            {
                if (currentNode.Left == null)
                {
                    throw new Exception("Нет идентификатора с таким именем");
                }
                return FindIdentifier(key, currentNode.Left);
            }
            else
            {
                if (currentNode.Right == null)
                {
                    throw new Exception("Нет идентификатора с таким именем");
                }
                return FindIdentifier(key, currentNode.Right);
            }
        }
    }
}
