using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Identifiers.Trees
{
    public class NodeTree<T> : BinaryTree<T> where T : class, IComparable<T>
    {
        protected Node<T> _root;

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
        }

        private Node<T> FindNode(T data, Node<T> currentNode = null)
        {
            currentNode = currentNode ?? _root;
            int result = data.CompareTo(currentNode.Data);
            if(result == 0) {
                return currentNode;
            }
            else if(result < 0){
                if(currentNode.Left == null)
                {
                    return null;
                }
                return FindNode(data, currentNode.Left);
            }
            else
            {
                if (currentNode.Right == null)
                {
                    return null;
                }
                return FindNode(data, currentNode.Right);
            }
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

        protected enum Side
        {
            Left,
            Right
        }
        protected class Node<T> : IComparable<Node<T>>, IEnumerable<T> where T : class, IComparable<T>
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
