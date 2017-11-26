using System;

namespace DataStructures.Trees.BinaryTrees
{
    //TODO Bulk operations, remove 'public root' as private

    public class Treap<K,V>
    {
        private const int MaxPriority = int.MaxValue;
        private const int MinPriority = int.MinValue;
        private readonly Comparison<K> keyCompare;

        private Random randomGenerator;
        public Node<K, V> root;


        public Treap(Comparison<K> keyCompare)
        {
            if (keyCompare == null)
                throw new ArgumentNullException("keyCompare argument cannot be null. You must specify key comparison strategy");

            randomGenerator = new Random(Environment.TickCount);
            this.keyCompare = keyCompare;
        }

        




        public void Insert(K key, V value)
        {
            if (root == null)
            {
                int priority = GetRandomPriority();
                root = new Node<K, V>(key,value,priority);
            }
            else
            {
                InsertNewElement(key, value);
            }
        }

        public void Delete(K key)
        {
            var keyNode = FindParentFor(key);

            if (keyCompare(key, keyNode.Key) == 0)
            {
                switch (keyNode.ChildsCount)
                {
                    case 0:
                        DeleteLeafNode(keyNode);
                        break;
                    case 1:
                        DeleteOneChild(keyNode);
                        break;
                    case 2:
                        DeleteTwoChilds(keyNode);
                        break;
                    default:
                        throw new Exception("INTERNAL ERROR >> Unexpected childs count >> in DeteteKey()");
                }
            }
        }   

        private void DeleteTwoChilds(Node<K, V> keyNode)
        {
            var successor = FindSuccessor(keyNode);
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           
            if (successor.Parent == keyNode)
            {
                // successor is first right child of keyNode

                successor.Parent = keyNode.Parent;
                if (keyNode.HaveParent)
                {
                    if (keyNode.IsLeftChild)
                        keyNode.Parent.Left = successor;
                    else keyNode.Parent.Right = successor;
                }

                successor.Left = keyNode.Left;
                keyNode.Left.Parent = successor;
            }
            else
            {
                //successor is somewhere in subtree, we must upadate all references

                successor.Parent.Left = successor.Right;
                if (successor.HaveRightChild)
                    successor.Right.Parent = successor.Parent;

                successor.Parent = keyNode.Parent;
                if (keyNode.HaveParent)
                {
                    if (keyNode.IsLeftChild)
                        keyNode.Parent.Left = successor;
                    else keyNode.Parent.Right = successor;
                }

                keyNode.Left.Parent = keyNode.Right.Parent = successor;
                successor.Left = keyNode.Left;
                successor.Right = keyNode.Right;

            }

            if (successor.IsRoot)
                root = successor;
        }

        private Node<K,V> FindSuccessor(Node<K, V> keyNode)
        {
            var successor = keyNode.Right;

            while (successor.HaveLeftChild)
                successor = successor.Left;

            return successor;
        }

        private void DeleteOneChild(Node<K, V> keyNode)
        {
            if (keyNode.HaveParent)
            {
                var child = keyNode.HaveLeftChild ? keyNode.Left : keyNode.Right;

                if (keyNode.IsLeftChild)
                    keyNode.Parent.Left = child;
                else keyNode.Parent.Right = child;

                child.Parent = keyNode.Parent;
            }
            else
            {
                Node<K, V> child = null;
                if (keyNode.HaveLeftChild)
                    child = keyNode.Left;
                else child = keyNode.Right;

                root = child;
                child.Parent = null;
            }
        }

        private void DeleteLeafNode(Node<K, V> keyNode)
        {
            if (keyNode.HaveParent)
            {
                if (keyNode.IsLeftChild)
                    keyNode.Parent.Left = null;
                else keyNode.Parent.Right = null;

                keyNode.Parent = null;
            }
            else
            {
                root = null;
            }
        }

        public bool Search(K key)
        {
            var node = FindParentFor(key);

            return keyCompare(key, node.Key) == 0;
        }

        private void InsertNewElement(K key,V value)
        {
            var parent = FindParentFor(key);
            if (keyCompare(key, parent.Key) == 0)
                throw new ArgumentException("Key alredy exist in treap");

            int priority = GetRandomPriority();
            Node<K, V> newNode = new Node<K, V>(key, value, priority);

            if (keyCompare(key, parent.Key) < 0)
                parent.Left = newNode;
            else parent.Right = newNode;
            newNode.Parent = parent;
            
            RepairTree(newNode);
            if (!newNode.HaveParent)
                root = newNode;
        }

        private void RepairTree(Node<K, V> newNode)
        {
            while (newNode.HaveParent)
            {
                if (newNode.Parent.Priority < newNode.Priority)
                {
                    if (newNode.IsLeftChild)
                    {
                        RightRotate(newNode.Parent);
                    }
                    else LeftRotate(newNode.Parent);
                }
                else break;
            }
        }

        ///<summary>Rotate left from specified origin node</summary>
        ///<returns>Returns new root node after rotation</returns>
        private Node<K, V> LeftRotate(Node<K, V> parent)
        {
            var child = parent.Right;
            child.Parent = parent.Parent;

            if (parent.HaveParent)
            {
                if (parent.IsLeftChild)
                {
                    parent.Parent.Left = child;
                }
                else parent.Parent.Right = child;
            }

            parent.Right = child.Left;
            if (child.HaveLeftChild)
                child.Left.Parent = parent;

            child.Left = parent;
            parent.Parent = child;

            return child;
        }

        ///<summary>Rotate right from specified origin node</summary>
        ///<returns>Returns new root node after rotation</returns>
        private Node<K, V> RightRotate(Node<K, V> parent)
        {
            var child = parent.Left;
            child.Parent = parent.Parent;

            if (parent.HaveParent)
            {
                if (parent.IsLeftChild)
                    parent.Parent.Left = child;
                else parent.Parent.Right = child;
            }

            parent.Left = child.Right;

            if (child.HaveRightChild)
                child.Right.Parent = parent;

            parent.Parent = child;
            child.Right = parent;

            return child;
        }

        ///<summary>Search for parent for new key</summary>
        ///<remarks>If key do not not exists returns parent for new key, otherwise returns node with input key</remarks>
        private Node<K, V> FindParentFor(K key)
        {
            Node<K, V> search = root;

            int compare = 0;

            while (search != null)
            {
                compare = keyCompare(key, search.Key);

                if (compare < 0)
                {
                    if (search.HaveLeftChild)
                        search = search.Left;
                    else break;
                }
                else if (compare > 0)
                {
                    if (search.HaveRightChild)
                        search = search.Right;
                    else break;
                }
                else
                {
                    break;
                }
            }

            return search;
        }

        private int GetRandomPriority()
        {
            //return randomGenerator.Next(-10, 10);
            return randomGenerator.Next(MinPriority + 1, MaxPriority - 1);
        }
    }
}


/*

            Node<K, V> node = FindNode(key);
            bool exist = false;

            int cresult = keyCompare(key, node.Key);

            if (cresult == 0)
                exist |= true;

            if (cresult < 0)
                if (node.HaveLeftChild)
                    if (keyCompare(node.Left.Key, key) == 0)
                        exist = true;
            if (node.HaveRightChild)
                if (keyCompare(node.Right.Key, key) == 0)
                       exist = true;
            
            if(exist)
                throw new InvalidOperationException("Key alredy exist in treap");

            return node;*/
