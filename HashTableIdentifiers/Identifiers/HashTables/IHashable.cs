using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.HashTables
{
    public interface IHashable
    {
        int GetHash(HashFunction function);
    }
}
