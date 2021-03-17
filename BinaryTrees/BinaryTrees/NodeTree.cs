using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BinaryTrees
{
    public class NodeTree<T> : BinaryTree<T> where T : class, IComparable<T>
    {
        private Node<T> _root;

        public NodeTree()
        {
            _root = null;
        }

        public override void Add(T value)
        {
            if(_root == null)
            {
                _root = new Node<T>(value);
            }
            else
            {
                Add(_root, new Node<T>(value));
            }
        }

        private void Add(Node<T> currentNode, Node<T> newNode)
        {
            if(newNode.CompareTo(currentNode) < 0)
            {
                if(currentNode.Left == null)
                {
                    currentNode.Left = newNode;
                    newNode.Parent = currentNode;
                }
                else
                {
                    Add(currentNode.Left, newNode);
                }
            }
            else
            {
                if (currentNode.Right == null)
                {
                    currentNode.Right = newNode;
                    newNode.Parent = currentNode;
                }
                else
                {
                    Add(currentNode.Right, newNode);
                }
            }
        }

        public override void Add(params T[] values)
        {
            foreach(var value in values)
            {
                Add(value);
            }
        }

        public override bool Remove(T value)
        {
            var foundNode = FindNode(value);
            if(foundNode != null)
            {
                Remove(foundNode);
                return true;
            }
            else
            {
                throw new Exception("Нельзя удалить узел которого нет");
            }
            return false;
        }

        private Node<T> FindNode(T data, Node<T> startWithNode = null)
        {
            startWithNode = startWithNode ?? _root;
            int result;
            return (result = data.CompareTo(startWithNode.Data)) == 0
                ? startWithNode
                : result < 0
                    ? startWithNode.Left == null
                        ? null
                        : FindNode(data, startWithNode.Left)
                    : startWithNode.Right == null
                        ? null
                        : FindNode(data, startWithNode.Right);
        }

        private void Remove(Node<T> node)
        {
            if (node == null)
            {
                return;
            }
            if(node.Parent == null)
            {
                RemoveRoot();
                return;
            }
            var currentNodeSide = node.NodeSide;
            //если у узла нет подузлов, можно его удалить
            if (node.Left == null && node.Right == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.Parent.Left = null;
                }
                else
                {
                    node.Parent.Right = null;
                }
            }
            //если нет левого, то правый ставим на место удаляемого 
            else if (node.Left == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.Parent.Left = node.Right;
                }
                else
                {
                    node.Parent.Right = node.Right;
                }

                node.Right.Parent = node.Parent;
            }
            //если нет правого, то левый ставим на место удаляемого 
            else if (node.Right == null)
            {
                if (currentNodeSide == Side.Left)
                {
                    node.Parent.Left = node.Left;
                }
                else
                {
                    node.Parent.Right = node.Left;
                }

                node.Left.Parent = node.Parent;
            }
            //если оба дочерних присутствуют, 
            //то правый становится на место удаляемого,
            //а левый вставляется в правый
            else
            {
                if (currentNodeSide == Side.Left)
                {
                    Add(node.Right, node.Left);
                    node.Right.Parent = node.Parent;
                    node.Parent.Left = node.Right;
                }
                else
                {
                    Add(node.Right, node.Left);
                    node.Right.Parent = node.Parent;
                    node.Parent.Right = node.Right;
                }
            }
        }

        private void RemoveRoot()
        {
            //если у узла нет подузлов, можно его удалить
            if (_root.Left == null && _root.Right == null)
            {
                _root = null;
            }
            //если нет левого, то правый ставим на место удаляемого 
            else if (_root.Left == null)
            {
                _root.Right.Parent = null;
                _root = _root.Right;
            }
            //если нет правого, то левый ставим на место удаляемого 
            else if (_root.Right == null)
            {
                _root.Left.Parent = null;
                _root = _root.Left;
            }
            //если оба дочерних присутствуют, 
            //то правый становится на место удаляемого,
            //а левый вставляется в правый
            else
            {
                _root.Right.Parent = null;
                Add(_root.Right, _root.Left);
                _root = _root.Right;
            }
        }

        public override string GetTextForPrinting(PrintFormat format)
        {
            if(_root == null)
            {
                return "Пустое дерево";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append($"Двоичное дерево поиска размера {Count}.\n");
            switch (format)
            {
                case PrintFormat.AsList:
                    builder.Append(GetListText(_root));
                    break;
                case PrintFormat.AsTable:
                    builder.Append("--------------------------------------\n");
                    builder.Append($"{"Node",10}|{"Left",10}|{"Right",10}\n");
                    builder.Append("--------------------------------------\n");
                    builder.Append(GetTableText(_root));
                    builder.Append("--------------------------------------");
                    break;
                case PrintFormat.AsTree:
                    builder.Append("Уровни\n");
                    for(int i = 0; i < (int)Math.Log2(Count) + 3; i++)
                    {
                        builder.Append($"{i,3}");
                    }
                    builder.Append("\n");
                    builder.Append(GetTreeText(_root));
                    break;
                default:
                    throw new Exception();
            }
            builder.Append("\n");
            return builder.ToString();
        }

        private string GetTreeText(Node<T> node, string indent = "", Side? side = null)
        {
            if(node != null)
            {
                StringBuilder builder = new StringBuilder();
                var nodeSide = side == null ? "+" : side == Side.Left ? "L" : "R";
                builder.Append($"{indent} [{nodeSide}] -> {node.Data}\n");
                indent += new string(' ', 3);
                //рекурсивный вызов для левой и правой веток
                StringBuilder innerBuilder = new StringBuilder();
                innerBuilder.Append(GetTreeText(node.Left, indent, Side.Left));
                innerBuilder.Append(builder);
                innerBuilder.Append(GetTreeText(node.Right, indent, Side.Right));
                return innerBuilder.ToString();
            }
            return "";
        }

        private string GetTableText(Node<T> node)
        {
            if(node == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            builder.Append($"{node.Data,10}|{(node.Left == null ? " - " : node.Left.Data.ToString()),10}|" +
                              $"{(node.Right == null ? " - " : node.Right.Data.ToString()),10}\n");
            builder.Append(GetTableText(node.Left));
            builder.Append(GetTableText(node.Right));
            return builder.ToString();
        }

        private string GetListText(Node<T> node)
        {
            StringBuilder builder = new StringBuilder();
            int i = 1;
            foreach(var item in node)
            {
                builder.Append(item.ToString());
                builder.Append(i % 10 == 0 ? "\n" : "  ");
                i++;
            }
            return builder.ToString();
        }

        public override int GetLayerLeaves(int layer)
        {
            return GetLayerLeaves(layer, 0, _root);
        }

        private int GetLayerLeaves(int layer, int currentLayer, Node<T> node)
        {
            if(node == null)
            {
                return 0;
            }
            else if(currentLayer > layer)
            {
                return 0;
            }
            else if(currentLayer < layer)
            {
                return GetLayerLeaves(layer, currentLayer + 1, node.Left) + GetLayerLeaves(layer, currentLayer + 1, node.Right);
            }
            else
            {
                if(node.Left == null && node.Right == null)
                {
                    return 1;
                }
                return 0;
            }
        }

        public override void RotateByNodeWithValue(T value, Rotation rotation)
        {
            Node<T> node = FindNode(value);
            if(node == null)
            {
                throw new Exception("Узел с таким значением не найден");
            }
            if (rotation == Rotation.Right)
            {
                if(node.Left == null)
                {
                    throw new Exception("Нельзя осуществить правый поворот по этому узлу");
                }
                var parent = node.Parent;
                var nodeSide = node.NodeSide;

                var a = node.Left;

                a.Parent = null;
                node.Left = null;

                var beta = a.Right;
                if(beta != null)
                {
                    a.Right = null;
                    beta.Parent = null;

                    node.Left = beta;
                    beta.Parent = node;
                }

                a.Right = node;
                node.Parent = a;

                if(parent == null)
                {
                    _root = a;
                }
                else
                {
                    if(nodeSide == Side.Right)
                    {
                        parent.Right = a;
                    }
                    else
                    {
                        parent.Left = a;
                    }
                    a.Parent = parent;
                }
            }
            else
            {
                if (node.Right == null)
                {
                    throw new Exception("Нельзя осуществить левый поворот по этому узлу");
                }
                var parent = node.Parent;
                var nodeSide = node.NodeSide;

                var b = node.Right;

                b.Parent = null;
                node.Right = null;

                var beta = b.Left;
                if(beta != null)
                {
                    b.Left = null;
                    beta.Parent = null;

                    node.Right = beta;
                    beta.Parent = node;
                }

                b.Left = node;
                node.Parent = b;

                if (parent == null)
                {
                    _root = b;
                }
                else
                {
                    if (nodeSide == Side.Right)
                    {
                        parent.Right = b;
                    }
                    else
                    {
                        parent.Left = b;
                    }
                    b.Parent = parent;
                }
            }
        }
        public override List<T> GetAllOneChildNodes()
        {
            List<T> nodesWithOneChild = new List<T>();
            foreach(var el in this)
            {
                Node<T> node = FindNode(el);
                if((node.Left == null && node.Right != null) || (node.Left != null && node.Right == null))
                {
                    nodesWithOneChild.Add(node.Data);
                }
            }
            return nodesWithOneChild;
        }

        public override void Clear()
        {
            _root = null;
        }

        public override bool Contains(T item)
        {
            return FindNode(item) != null;
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            if(Count + arrayIndex > array.Length)
            {
                throw new Exception("Недостаточный размер массива");
            }
            foreach(var item in this)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return _root != null ? _root.GetEnumerator() : null;
        }
        public override int Count => _root != null ? _root.Count : 0;

        public override bool IsReadOnly => false;

        private enum Side
        {
            Left,
            Right
        }
        private class Node<T> : IComparable<Node<T>>, IEnumerable<T> where T : class, IComparable<T>
        {
            public Node(T data)
            {
                Data = data;
            }

            public T Data { get; set; }

            public Node<T> Left { get; set; }

            public Node<T> Right { get; set; }

            public Node<T> Parent { get; set; }

            public Side? NodeSide =>
                Parent == null
                ? (Side?)null
                : Parent.Left == this
                    ? Side.Left
                    : Side.Right;

            public int Count
            {
                get
                {
                    int sum = 1;
                    if (Right != null)
                    {
                        sum += Right.Count;
                    }
                    if (Left != null)
                    {
                        sum += Left.Count;
                    }
                    return sum;
                }
            }

            public int CompareTo(Node<T> other)
            {
                return Data.CompareTo(other.Data);
            }

            public IEnumerator<T> GetEnumerator()
            {
                if(Left != null)
                {
                    foreach (var item in Left)
                    {
                        yield return item;
                    }
                }

                if(Data != null)
                {
                    yield return Data;
                }

                if (Right != null)
                {
                    foreach (var item in Right)
                    {
                        yield return item;
                    }
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString() => Data.ToString();
        }
    }
}
