using System;

namespace DataStructures.Trees.BTrees
{
    ///<summary>Node used in <see cref="BTree{K,V}"/></summary>
    public class INGORE__BTreeNode<K,V>
    {
        ///<summary>Get maximum count of keys which node can store</summary>
        public int MaxKeysCount { get { return elements.Length; } }
        ///<summary>Get current number of keys which node stores</summary>
        public int KeysCount { get; private set; }
        ///<summary>Get <see cref="BTreeNode{K, V}"/> parent</summary>
        public BTreeNode<K, V> Parent { get; private set; }

        private NodeElement<K, V>[] elements;
        private Comparison<K> keyCompare;

        public BTreeNode(int maxKeysCount, BTreeNode<K, V> parent, Comparison<K> compare)
        {
            KeysCount = 0;
            Parent = parent;
            elements = new NodeElement<K, V>[maxKeysCount];
            keyCompare = compare;
        }

        public bool IsFull()
        {
            return KeysCount == MaxKeysCount;
        }

        public K GetKey(int index)
        {
            ThrowIfGetIndexInvalid(index);

            return elements[index].Key;
        }

        public V GetValue(int index)
        {
            ThrowIfGetIndexInvalid(index);

            return elements[index].Value;
        }

        public BTreeNode<K, V> GetLeftChild(int index)
        {
            ThrowIfGetIndexInvalid(index);

            return elements[index].Left;
        }

        public BTreeNode<K, V> GetRightChild(int index)
        {
            ThrowIfGetIndexInvalid(index);

            return elements[index].Right;
        }

        ///<summary>Insert raw element and keep orders of key/value pairs</summary>
        public void InsertRawElement(NodeElement<K, V> element)
        {

        }

        public NodeElement<K,V> GetRawElement(int index)
        {
            ThrowIfGetIndexInvalid(index);

            return elements[index];

        }

        public void ForceSetNodeElement(int index, NodeElement<K, V> element)
        {
            ThrowIfSetIndexInvalid(index);

            elements[index] = element;

            if (elements[index] != null)
                KeysCount++;
        }

        public void InsertElement(K key, V value, BTreeNode<K, V> left, BTreeNode<K, V> right)
        {
            if (MaxKeysCount == KeysCount)
                throw new InvalidOperationException("Cannot insert new key-value pair to filled node");

            int insertIndex = GetInsertIndex(key);
            ShiftRightArrayElements(insertIndex);

            elements[insertIndex] = new NodeElement<K, V>(key, value, left, right);

            KeysCount++;
        }

        public void SetParent(BTreeNode<K, V> parent)
        {
            Parent = parent;
        }

        ///<summary>Inserts a new key-value pair in valid position</summary>
        public void InsertElement(K key, V value)
        {
            if (MaxKeysCount == KeysCount)
                throw new InvalidOperationException("Cannot insert new key-value pair to filled node");

            int insertIndex = GetInsertIndex(key);
            ShiftRightArrayElements(insertIndex);

            BTreeNode<K, V> left = insertIndex > 0 ? elements[insertIndex - 1].Right : null;
            BTreeNode<K, V> right = insertIndex < KeysCount ? elements[insertIndex + 1].Left : null;
            elements[insertIndex] = new NodeElement<K, V>(key, value, left, right);

            KeysCount++;
        }

        public void SetRightChild(int index, BTreeNode<K, V> childNode)
        {
            ThrowIfGetIndexInvalid(index);

            elements[index].SetRight(childNode);
        }

        public void SetLeftChild(int index, BTreeNode<K, V> childNode)
        {
            ThrowIfGetIndexInvalid(index);

            elements[index].SetLeft(childNode);
        }

        private void ShiftRightArrayElements(int startIndex)
        {
            for (int i = KeysCount; i > startIndex; i--)
            {
                elements[i] = elements[i - 1];
            }
        }

        private void ThrowIfSetIndexInvalid(int index)
        {
            if (index >= MaxKeysCount)
            {
                string errorMsg = string.Format("Index if out of bounds of keys array. Actual: {0} but max:{1}", MaxKeysCount, index);
                throw new IndexOutOfRangeException(errorMsg);
            }
            else if (index < 0)
            {
                throw new IndexOutOfRangeException("Index cannot contain negative value");
            }
        }

        private int GetInsertIndex(K key)
        {
            int insertIndex = 0;
            for (; insertIndex < KeysCount; insertIndex++)
            {
                if (keyCompare(key, elements[insertIndex].Key) < 0)
                {
                    break;
                }
            }

            return insertIndex;
        }

        private void ThrowIfGetIndexInvalid(int index)
        {
            if (index >= KeysCount)
            {
                string errMessage =
                    string.Format("Index is out of bounds of keys array. Actual value: {0} but max: {1} ", index, KeysCount - 1);

                throw new IndexOutOfRangeException(errMessage);
            }
            else if (index < 0)
            {
                string errMessage =
                    string.Format("Index cannot be negative value. Actual: {0} but minimum: 0", index, 0);

                throw new IndexOutOfRangeException(errMessage);
            }
        }
    }
}
