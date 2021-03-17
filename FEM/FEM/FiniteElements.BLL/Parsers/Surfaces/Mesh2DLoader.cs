using FiniteElements.BLL.Elements;
using FiniteElements.BLL.Elements.Surfaces;
using FiniteElements.BLL.Meshes;
using FiniteElements.BLL.Meshes.Surfaces;
using FiniteElements.BLL.Nodes;
using FiniteElements.BLL.Nodes.Surfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace FiniteElements.BLL.Parsers.Surfaces
{
    public class Mesh2DLoader
    {
        public string PathToData { get; set; }

        public Mesh2DLoader(string pathToData)
        {
            PathToData = pathToData;
        }
        public Mesh2D ParseMeshFromCsv(string nodesFileName, string elementsFileName, char csvSeparator)
        {
            List<FiniteElement2D> elements = new List<FiniteElement2D>();
            List<Node2D> nodes = new List<Node2D>();
            
            using(StreamReader reader = new StreamReader(Path.Combine(PathToData, nodesFileName)))
            {
                reader.ReadLine(); //Пропускаю строку с названиями столбцов Id;X;Y;Z;Type;Force value;Force angle
                string line;
                int i = 1;
                while((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string[] values = line.Split(csvSeparator);
                        //Убираю кавычки по краям если они есть
                        for(int j = 0; j < values.Length; j++)
                        {
                            values[j] = values[j].Trim('\"');
                        }
                        nodes.Add(new Node2D(int.Parse(values[0]), double.Parse(values[1]), double.Parse(values[2])) { 
                            Type = values[4] == "None" ? NodeType.None : values[4] == "Fixed" ? NodeType.Fixed : NodeType.Force,
                            ForceValue = values[4] == "Force" ? double.Parse(values[5]) : 0,
                            ForceAngle = values[4] == "Force" ? double.Parse(values[6]) : 0
                        });
                        i++;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception($"Ошибка парсинга файла с узлами. Строка {i}. {exception.Message}");
                    }
                }
            }

            using (StreamReader reader = new StreamReader(Path.Combine(PathToData, elementsFileName)))
            {
                reader.ReadLine(); //Пропускаю строку с названиями столбцов Id;NodesIds,Thickness
                string line;
                int i = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        string[] values = line.Split(csvSeparator);

                        int[] nodeIds = values[1].Split('|').Select(id => int.Parse(id)).ToArray();

                        if(nodeIds.Length == 3) // Треугольный элемент
                        {
                            Node2D[] currentNodes = new Node2D[3];
                            for(int j = 0; j < 3; j++)
                            {
                                currentNodes[j] = nodes.FirstOrDefault(n => n.Id == nodeIds[j]);
                            }

                            elements.Add(new TriangleElement(int.Parse(values[0]), Brushes.Gray, new List<Node2D>(currentNodes),
                                double.Parse(values[2].Trim('\"'))));
                        }
                        else if(nodeIds.Length == 4) // Четырехугольный
                        {
                            Node2D[] currentNodes = new Node2D[4];
                            for (int j = 0; j < 4; j++)
                            {
                                currentNodes[j] = nodes.FirstOrDefault(n => n.Id == nodeIds[j]);
                            }

                            elements.Add(new QuadriteralElement(int.Parse(values[0]), Brushes.Gray, new List<Node2D>(currentNodes),
                                double.Parse(values[2].Trim('\"'))));
                        }

                        i++;
                    }
                    catch (Exception exception)
                    {
                        throw new Exception($"Ошибка парсинга файла с элементами. Строка {i}. {exception.Message}");
                    }
                }
            }

            return new Mesh2D(elements, nodes);
        }
    }
}
