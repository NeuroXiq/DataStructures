using System;

namespace DataStructures.Trees.BTrees
{
    public class BTree<K,V>
    {
        public readonly int NodeCapacity;
        private Comparison<K> keyCompare;
        private BTreeNode<K, V> root;
        

        public BTree(int nodeCapacity, Comparison<K> keyComparison)
        {
            //root = new BTreeNode<K, V>(nodeCapacity);

            NodeCapacity = nodeCapacity;
        }

        public void Insert(K key, V value)
        {
            
        }



        ///<summary>Returns node where key should be inserted</summary>
        private BTreeNode<K, V> FindNodeFor(K key)
        {
            return null;
        }
    }
}
