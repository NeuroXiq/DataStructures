using DataStructures.LDS.Common;
using System;

namespace DataStructures.Trees.BinaryTrees
{
    // TODO implement deletion

    public class RBTree<T>
    {
        public RBNode<T> root;
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
           if(!Contains(value))
            {
                RBNode<T> insertedNode = InsertNode(new RBNode<T>(value));
                RepairTree(insertedNode);
            }
            else throw new InvalidOperationException("Cannot insert value which actually exist in the tree");

            nodesCount++;
        }

        private void RepairTree(RBNode<T> origin)
        {
            if (origin.Parent == null)
            {
                RepairRootNode(origin);
                root = origin;
            }
            else if (origin.Parent.Color == Color.Black)
            {
                // its ok .
            }
            else if (origin.Parent.Parent != null)
            {
                if (GetUncleColor(origin) == Color.Red)
                {
                    RepairRedUncle(origin);
                }
                else
                {
                    RepairBlackUncle(origin);
                }
            }
            else
            {
                RepairBlackUncle(origin);
            }

            root = origin;
            while (root.Parent != null)
            {
                root = root.Parent;
            }
        }

        private void RepairRedUncle(RBNode<T> origin)
        {
            RBNode<T> uncle = GetUncle(origin);
            origin.Parent.SetColor(Color.Black);
            uncle.SetColor(Color.Black);
            origin.Parent.Parent.SetColor(Color.Red);

            RepairTree(origin.Parent.Parent);
        }

        private RBNode<T> GetUncle(RBNode<T> origin)
        {
            RBNode<T> grand = origin.Parent.Parent;

            if (grand.Left == origin)
                return grand.Right;
            else return grand.Left;
        }

        private void RepairBlackUncle(RBNode<T> origin)
        {
            RBNode<T> grand = origin.Parent.Parent;
            RBNode<T> secondRotateNode = origin.Parent;

            if (grand.Right != null)
                if (grand.Right.Left == origin)
                    secondRotateNode = RotateRight(origin.Parent, origin);
            if (grand.Left != null)
                if (grand.Left.Right == origin)
                    secondRotateNode = RotateLeft(origin.Parent, origin);

            if (grand.Left == secondRotateNode)
                RotateRight(grand, secondRotateNode);
            else RotateLeft(grand, secondRotateNode);

            origin.Parent.SetColor(Color.Black);
            grand.SetColor(Color.Red);
        }

        private void RepairRootNode(RBNode<T> origin)
        {
            origin.SetColor(Color.Black);
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

        ///<summary>binary insert, node color is set to RED</summary>
        private RBNode<T> InsertNode(RBNode<T> node)
        {
            if (root == null)
            {
                root = new RBNode<T>(Color.Red);
                return root;
            }

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
                    else current = current.Right;
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

        ///<remarks>
        /// If Uncle is NULL returns black (!)
        /// otherwise returns node color.
        ///</remarks>
        private Color GetUncleColor(RBNode<T> node)
        {
            RBNode<T> grand = node.Parent.Parent;

            if (grand.Left == node.Parent)
            {
                if (grand.Right != null)
                    return grand.Right.Color;
                else return Color.Black;
            }
            else
            {
                if (grand.Left != null)
                    return grand.Left.Color;
                else return Color.Black;
            }

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

        private RBNode<T> RotateRight(RBNode<T> parent, RBNode<T> child)
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

            return child;
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
