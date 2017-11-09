namespace DataStructures.Trees.BTrees
{
    public class __INGORE__NodeElement<K,V>
    {
        public K Key { get; private set; }
        public V Value { get; private set; }

        public BTreeNode<K, V> Left { get; private set; }
        public BTreeNode<K, V> Right { get; private set; }

        public NodeElement(K key, V value, BTreeNode<K,V> leftChild, BTreeNode<K,V> rightChild)
        {
            Key = key;
            Value = value;

            Left = leftChild;
            Right = rightChild;
        }

        public void SetLeft(BTreeNode<K, V> left)
        {
            Left = left;
        }

        public void SetRight(BTreeNode<K, V> right)
        {
            Right = right;
        }

    }
}
