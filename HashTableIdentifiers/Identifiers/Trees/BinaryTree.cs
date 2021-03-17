using System;
using System.Collections;
using System.Collections.Generic;

namespace Identifiers.Trees
{
    public enum PrintFormat
    {
        AsTree,
        AsTable,
        AsList
    }
    public enum Rotation
    {
        Right,
        Left
    }
    public abstract class BinaryTree<T> : ICollection<T> where T : class, IComparable<T>
    {
        public abstract void Add(T value);
        public abstract void Add(params T[] values);
        public abstract bool Remove(T value);
        public abstract void Clear();
        public abstract bool Contains(T item);
        public abstract void CopyTo(T[] array, int arrayIndex);
        public abstract IEnumerator<T> GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public abstract int Count { get; }
        public abstract bool IsReadOnly { get; }
    }
}
