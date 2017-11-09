using System;

namespace DataStructures.Trees.BTrees
{
    public class BTreeNode<K,V>
    {
        public int Capacity { get; private set; }
        public int KeysCount { get; private set; }

        public K[] Keys { get; private set; }
        public V[] Values { get; private set; }
        public BTreeNode<K, V>[] Childs { get; private set; }

        private Comparison<K> keyCompare;

        public BTreeNode(int capacity, Comparison<K> compare)
        {
            Keys = new K[capacity];
            Values = new V[capacity];
            Childs = new BTreeNode<K, V>[capacity + 1];

            Capacity = capacity;
            KeysCount = 0;
            keyCompare = compare;
        }

        ///<summary>Get index of key</summary>
        public int IndexOf(K key)
        {
            for (int i = 0; i < Keys.Length; i++)
            {
                if (keyCompare(key, Keys[i]) == 0)
                    return i;
            }

            return -1;
        }

        public void UpdateChilds(int index, BTreeNode<K, V> left, BTreeNode<K, V> right)
        {
            Childs[index] = left;
            Childs[index + 1] = right;
        }

        ///<summary>Insert new k/v pair and keep order</summary>
        ///<returns>Returns index when k/v was inserted</returns>
        public int Insert(K key, V value)
        {

            int index = 0;
            for (; index < KeysCount; index++)
            {
                if (keyCompare(key, Keys[index]) > 0)
                {
                    break;
                }
            }
            return 0;



            KeysCount++;
        }

        public bool IsFull()
        {
            return Capacity == KeysCount;
        }

        ///<summary>Shift keys/values/childs 1 position to right right from 'start'</summary>
        private void ShiftKV(int start)
        {
            for (int i = KeysCount; i > start; i--)
            {
                Keys[i] = Keys[i - 1];
                Values[i] = Values[i - i];
                //Childs[i + 1] = Childs[i];

            }
        }
    }
}
