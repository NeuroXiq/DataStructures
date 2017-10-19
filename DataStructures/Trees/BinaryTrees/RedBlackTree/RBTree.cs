using DataStructures.LDS.Common;
using System;

namespace DataStructures.Trees.BinaryTrees
{
    public class RBTree<T>
    {
        private RBNode<T> root;
        private int nodesCount;
        //private Func<T, T, bool> valueComparisonEquality;
        private Comparison<T> valueComparison;
        

        public RBTree(Comparison<T> comparisonDifference)
        {
            if (comparisonDifference == null)
                throw new ArgumentNullException("comparisonDifference paramenter cannot be null value");

            valueComparison = comparisonDifference;
            //valueComparisonEquality = GetValueComparator();

            root = null;
            nodesCount = 0;
        }

        private Func<T, T, bool> GetValueComparator()
        {
            Func<T, T, bool> comparator = null;

            if (typeof(IEquatable<T>).IsAssignableFrom(typeof(T)))
            {
                comparator = delegate (T v1, T v2) { return ((IEquatable<T>)v1).Equals(v2); };
            }
            else if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
            {
                comparator = delegate (T v1, T v2) { return ((IComparable<T>)v1).CompareTo(v2) == 0; };
            }
            else if (typeof(T).IsValueType)
            {
                comparator = delegate (T v1, T v2) { return v1.Equals(v2); };
            }
            else
            {
                comparator = delegate(T v1, T v2)
                {
                    if (ReferenceEquals(v1, null) && ReferenceEquals(v2, null))
                    {
                        return true;
                    }
                    else if (ReferenceEquals(v1, null))
                    {
                        return v2.Equals(v1);
                    }
                    else return v1.Equals(v2);
                };
            }


            return comparator;
        }

        public void Insert(T value)
        {
            if (nodesCount == 0)
            {
                root = new RBNode<T>(Color.Black);
            }
            else if(!Contains(value))
            {
                RBNode<T> insertedNode = InsertNode(new RBNode<T>(value));



            }
            else throw new InvalidOperationException("Cannot insert value which exist in the tree");

            nodesCount++;
        }

        private RBNode<T> InsertNode(RBNode<T> node)
        {
            RBNode<T> current = root;
            int compResult = 0;

            while (true)
            {
                compResult = valueComparison(node.Value, current.Value);
                if (compResult < 0)
                {
                    if (current.Left == null)
                        break;
                    else current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                        break;
                    else current = current.Left;
                }
            }

            node.SetParent(current);
            node.SetColor(Color.Red);

            if (compResult < 0)
            {
                current.SetLeft(node);
            }
            else
            {
                current.SetRight(node);
            }

            return node;
        }

        private void RepairTree(RBNode<T> origin)
        {
            if (origin.Parent == null)
                RepairRootNode(origin);
            else if (origin.Parent.Color == Color.Black)
            {
                RepairBlackParentNode(origin);
            }
            //else if(
        }

        private void RepairBlackParentNode(RBNode<T> origin)
        {
            throw new NotImplementedException();
        }

        private void RepairRootNode(RBNode<T> origin)
        {
            throw new NotImplementedException();
        }

        public void Delete(T value)
        {

        }

        public bool Contains(T value)
        {
            if (root == null)
                return false;
            else
            {
                RBNode<T> current = root;
                bool found = false;

                while (current != null && !found)
                {
                    int comparisonResult = valueComparison(value, current.Value);

                    if (comparisonResult == 0)
                        found = true;
                    else if (comparisonResult < 0)
                        current = current.Left;
                    else current = current.Right;
                }

                return found;
            }
        }

        public T[] GetPreOrderValues()
        {
            T[] resultArray = new T[nodesCount];

            if (nodesCount > 0)
            {
                aStack<RBNode<T>> stack = new aStack<RBNode<T>>();
                stack.Push(root);

                RBNode<T> current;
                int index = 0;

                while (stack.IsEmpty())
                {
                    current = stack.Pop();

                    if (current != null)
                    {
                        resultArray[index] = current.Value;
                        stack.Push(current.Left);
                        stack.Push(current.Right);
                    }
                }
            }
            else resultArray = new T[0];

            return resultArray;
        }

        public T[] GetInOrderValues()
        {
            return null;
        }

        public T[] GetPostOrderValues()
        {
            return null;
        }

        ///<summary>Rotate left</summary>
        ///<returns>Returns new root (parent) node</returns>
        private RBNode<T> RotateLeft(RBNode<T> parent, RBNode<T> child)
        {
            child.SetParent(parent.Parent);
            if (parent.Parent != null)
            {
                if (IsRightChild(parent))
                    parent.Parent.SetRight(child);
                else parent.Parent.SetLeft(child);           
            }

            parent.SetParent(child);
            parent.SetRight(child.Left);
            child.SetLeft(parent);

            return child;
        }

        private void RotateRight(RBNode<T> parent, RBNode<T> child)
        {
            child.SetParent(parent.Parent); 
            if (parent.Parent != null)
            {
                if (IsRightChild(parent))
                    parent.Parent.SetRight(child);
                else parent.Parent.SetLeft(child);
            }

            parent.SetParent(child);
            parent.SetLeft(child.Right);
            child.SetRight(parent);
        }

        private bool IsRightChild(RBNode<T> node)
        {
            if (node.Parent != null)
            {
                return node.Parent.Right == node;
            }

            throw new NullReferenceException("Cannot specify node direction becouse parent node is null");
        }

        private bool IsLeftChild(RBNode<T> node)
        {
            if (node.Parent != null)
            {
                return node.Parent.Left == node;
            }

            throw new NullReferenceException("Cannot specify node direction becouse parent node is null");
        }
    }
}
