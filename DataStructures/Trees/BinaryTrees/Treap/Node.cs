using System;

namespace DataStructures.Trees.BinaryTrees
{
    public class Node<K, V>
    {
        #region Public properties

        ///<summary>Indicates whether this is a root node.</summary>
        public bool IsRoot { get { return Parent == null; } }

        ///<summary>Get child count of this node</summary>
        public int ChildsCount { get
            {
                int count = 0;
                if (Left != null) count++;
                if (Right != null) count++;
                return count;
            } }

        ///<summary>Indicates whether node have left child</summary>
        public bool HaveLeftChild { get { return Left != null; } }

        ///<summary>Indicates whether node have right child</summary>
        public bool HaveRightChild { get { return Right != null; } }

        ///<summary>Indicates whether node have parent</summary>
        public bool HaveParent { get { return Parent != null; } }

        ///<summary>Get or set parent of this node</summary>
        public Node<K, V> Parent { get; set; }

        ///<summary>Get or set left childs of this node</summary>
        public Node<K, V> Left { get; set; }

        ///<summary>Get or set right child of this node</summary>
        public Node<K, V> Right { get; set; }

        ///<summary>Get internal priority of this node</summary>
        public int Priority { get; private set; }

        ///<summary><see cref="K"/> key of this node</summary>
        public K Key { get; private set; }

        ///<summary><see cref="V"/> value of this node</summary>
        public V Value { get; private set; }

        ///<summary>Indicates whether node is left child of its parent</summary>
        public bool IsLeftChild
        { get
            {
                if (HaveParent)
                {
                    return object.ReferenceEquals(this, Parent.Left);
                }
                else return false;
            }
        }

        ///<summary>Indicates whether node is right child of its parent</summary>
        public bool IsRightChild
        {
            get
            {
                if (HaveParent)
                {
                    return object.ReferenceEquals(this, Parent.Right);
                }
                else return false;
            }
        }

        #endregion

        #region Constructors

        ///<summary>Creates new instance of <see cref="Node{V, K}"/> with default <see cref="K"/> key, <see cref="V"/> value and priority</summary>
        public Node() : this(default(K), default(V),default(int))
        {

        }

        ///<summary>Creates new instance of <see cref="Node{V, K}"/> with default values for <see cref="K"/> key and <see cref="V"/> value </summary>
        ///<param name="priority">Node priority</param>
        public Node(int priority) : this(default(K), default(V), priority)
        {
            
        }
        ///<summary>Creates new instance of <see cref="Node{V, K}"/></summary>
        ///<param name="key">Node <see cref="K"/> key</param>
        ///<param name="priority">Node priority</param>
        ///<param name="value">Node <see cref="V"/> value</param>
        public Node(K key, V value, int priority)
        {
            Key = key;
            Value = value;
            Priority = priority;
        }

        #endregion

    }
}
