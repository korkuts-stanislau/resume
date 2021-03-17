using FiniteElements.BLL.SolutionTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FiniteElements.BLL.Parsers
{
    public class ElasticitySolutionInfoParser
    {
        public string PathToData { get; set; }
        public ElasticitySolutionInfoParser(string pathToData)
        {
            PathToData = pathToData;
        }

        public ElasticitySolutionInfo ParseTxt(string solutionFileName)
        {
            try
            {
                ElasticitySolutionInfo info = new ElasticitySolutionInfo();
                using (StreamReader reader = new StreamReader(Path.Combine(PathToData, solutionFileName)))
                {
                    Regex reg = new Regex(@"(.+)\s*=\s*(.+)(\s*#.*)?");
                    string line;
                    Dictionary<string, double> keyValuePairs = new Dictionary<string, double>();
                    while ((line = reader.ReadLine()) != null)
                    {
                        if(line.StartsWith("#"))
                        {
                            continue;
                        }
                        Match match = reg.Match(line);
                        keyValuePairs[match.Groups[1].Value.Trim()] = double.Parse(match.Groups[2].Value);
                    }
                    foreach (var pair in keyValuePairs)
                    {
                        switch (pair.Key)
                        {
                            case "modulus":
                                info.Modulus = pair.Value;
                                break;
                            case "coef":
                                info.Coefficient = pair.Value;
                                break;
                            default:
                                break;
                        }
                    }
                }
                return info;
            }
            catch(Exception exception)
            {
                throw new Exception($"Не удалось распарсить текстовый файл с информацией. {exception.Message}");
            }
        }
    }
}
