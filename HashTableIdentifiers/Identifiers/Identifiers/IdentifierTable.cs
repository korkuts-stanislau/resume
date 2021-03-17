using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.Identifiers
{
    public abstract class IdentifierTable
    {
        public abstract void Add(Identifier identifier);
        public abstract Identifier FindIdentifierByKey(string key);
    }
}
