using Identifiers.HashTables;
using Identifiers.Identifiers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identifiers.Parsers
{
    public enum TableType
    {
        Tree,
        Hash
    }
    public class IdentifiersParser
    {
        public IdentifierTable ParseTxt(string path, TableType type)
        {
            IdentifierTable table;
            switch (type)
            {
                case TableType.Tree:
                    {
                        table = new TreeIdentifierTable();
                    }
                    break;
                case TableType.Hash:
                    {
                        table = new HashIdentifierTable((int)Math.Pow(2, 10), new ConvolutionHashFunction(123456789101));
                    }
                    break;
                default:
                    throw new Exception("Нет такого типа");
            }
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                Identifier identifier;
                int i = 1;
                while((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string[] values = line.Split(';');
                        identifier = new Identifier
                        {
                            Key = values[0],
                            Type = values[1] == "int" ? IdentifierType.Int32 : values[1] == "double" ? IdentifierType.Double : IdentifierType.String,
                            Value = values[2]
                        };
                        table.Add(identifier);
                        i++;
                    }
                    catch
                    {
                        throw new Exception($"Проблема чтения файла в строке с индексом {i}");
                    }
                }
            }
            return table;
        }
    }
}
