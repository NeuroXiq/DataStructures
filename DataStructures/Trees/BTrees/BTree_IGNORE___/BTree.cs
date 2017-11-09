using System;

namespace DataStructures.Trees.BTrees
{
    public class IGNORE__BTree<K,V>
    {
        public int NodeCount { get; private set; }
        public int NodeCapacity { get; private set; }


        //TODO remove public
        //TODO implement insertion and everything else :c
        public BTreeNode<K, V> root;
        private Comparison<K> keyComparison;
        

        public BTree(int nodeCapacity, Comparison<K> comparison)
        {
            if (comparison == null)
                throw new ArgumentException("Comparison delegate cannot be null");
            if (nodeCapacity < 1)
                throw new ArgumentException("Node capacity must be greater than zero");

            keyComparison = comparison;
            NodeCapacity = nodeCapacity;
            NodeCount = 0;
        }

        public void Insert(K key, V value)
        {
            if (root == null)
            {
                root = new BTreeNode<K, V>(NodeCapacity, null, keyComparison);
                root.InsertElement(key, value);
            }
            else InsertNew(key, value);
        }
        

        private void InsertNew(K key, V value)
        {
            BTreeNode<K,V> btn = FindNodeFor(root, key);
            if (btn.IsFull())
            {
                BTreeNode<K, V> left, right;
                NodeElement<K, V> medianElement = MedianSplitNode(btn, out left, out right);

                if (keyComparison(key, medianElement.Key) < 0)
                    left.InsertElement(key, value);
                else right.InsertElement(key, value);


                if (btn.Parent != null)
                {
                    InsertToFullNode(btn.Parent, medianElement);
                }
                else
                {
                    //its root
                    BTreeNode<K, V> newRoot = new BTreeNode<K, V>(NodeCapacity, null, keyComparison);
                    newRoot.ForceSetNodeElement(0, medianElement);
                    root = newRoot;
                }

            }
            else
            {
                InsertToNotFullNode(btn, key, value);
            }
        }

        private void InsertToNotFullNode(BTreeNode<K, V> node, K key, V value)
        {
            node.InsertElement(key, value);
        }

        private void InsertToFullNode(BTreeNode<K, V> node, NodeElement<K, V> medianElement)
        {
            if (node.IsFull())
            {
                if (node.Parent != null)
                {
                    BTreeNode<K, V> left, right;
                    NodeElement<K, V> newMedian = MedianSplitNode(node, out left, out right);
                    


                    InsertToFullNode(node.Parent, newMedian);
                }
                else
                {
                    // this is a root node
                    // recursion stops here
                    BTreeNode<K, V> newRoot = new BTreeNode<K, V>(NodeCapacity, null, keyComparison);
                    newRoot.ForceSetNodeElement(0, medianElement);
                    root = newRoot;
                }
            }
            else
            {
                // we find node with free slot, and this is a not root node
                // insert median element here


            }
            
        }

        private void UpdateParent(BTreeNode<K, V> lastChild, BTreeNode<K, V> newChild)
        {
            BTreeNode<K, V> parent = lastChild.Parent;
            newChild.SetParent(parent);

            if (lastChild.Parent != null)
            {
                //update reference in parent (set lastChild to newChild)

                if (parent.GetLeftChild(0) == lastChild)
                {
                    parent.SetLeftChild(0, newChild);
                }
                else
                {
                    //need to search where is lastChild 
                    //and replace it with newChild
                    for (int i = 0; i < parent.KeysCount; i++)
                    {
                        if (parent.GetRightChild(i) == lastChild)
                        {
                            parent.SetRightChild(i, newChild);
                            break;
                        }
                    }
                }

            }
        }

        ///<summary>Split node on median value</summary>
        ///<returns>median element with updated references to left/right childs</returns>
        ///<param name="leftOut">Node builded on values to the left of the median</param>
        ///<param name="rightOut">Node builded on values to the right of the median</param>
        public NodeElement<K,V> MedianSplitNode(BTreeNode<K, V> toSplit, out BTreeNode<K,V> leftOut, out BTreeNode<K,V> rightOut)
        {
            int median = (toSplit.KeysCount / 2);

            leftOut = new BTreeNode<K, V>(toSplit.MaxKeysCount, toSplit.Parent, keyComparison);
            rightOut = new BTreeNode<K, V>(toSplit.MaxKeysCount, toSplit.Parent, keyComparison);

            int shift = 0;
            if (toSplit.KeysCount % 2 == 0)
            {
                shift = 1;
                leftOut.ForceSetNodeElement(0, toSplit.GetRawElement(0));
            }

            // copy element from toSplit to left/right child
            for (int i = shift; i < median ; i++)
            {
                leftOut.ForceSetNodeElement(i, toSplit.GetRawElement(i));

                int rindex = toSplit.KeysCount - i - 1 + shift;
                rightOut.ForceSetNodeElement(rindex, toSplit.GetRawElement(rindex));
            }

            NodeElement<K,V> medianElement = toSplit.GetRawElement(median);
            medianElement.SetLeft(leftOut);
            medianElement.SetRight(rightOut);

            return medianElement;
        }

        ///<summary>Returns node where key should be inserted</summary>
        private BTreeNode<K, V> FindNodeFor(BTreeNode<K,V> origin, K key)
        {
            bool search = true;
            int compareResult = -1;
            BTreeNode<K, V> current = origin;
            BTreeNode<K, V> prev = origin; 

            while (search)
            {
                for (int i = 0; i < current.KeysCount; i++)
                {
                    compareResult = keyComparison(key, current.GetKey(i));
                    prev = current;

                    if (compareResult == 0)
                    {
                        search = false;
                        break;
                    }
                    else if (compareResult < 0)
                    {
                        current = current.GetLeftChild(i);
                        break;
                    }
                    else if (i == current.KeysCount - 1)
                    {
                        //key is bigger than all keys in 'current'.
                        //need to take last right child.
                        current = current.GetRightChild(current.KeysCount - 1);
                        break;
                    }
                }

                if (current == null)
                {
                    search = false;
                }
                       
            }
            return prev;
        }

    }
}
