using System;

namespace DataStructures.Trees.BinaryTrees
{
    ///<summary>Internal class used to build Cartesian tree in 'sift up' method</summary>
    ///<remarks>From wiki:
    ///A Cartesian tree may be constructed in linear time from its input sequence. One method is to simply process the sequence values in left-to-right order,
    ///maintaining the Cartesian tree of the nodes processed so far, in a structure that allows both upwards and downwards traversal of the tree.
    ///To process each new value x, start at the node representing the value prior to x in the sequence and follow the path from this node
    ///to the root of the tree until finding a value y smaller than x. This node y is the parent of x,
    ///and the previous right child of y becomes the new left child of x. The total time for this procedure is linear,
    ///because the time spent searching for the parent y of each new node x can be charged against the number of nodes
    ///that are removed from the rightmost path in the tree.
    /// </remarks>
    public class CTBSift<T> : CTBuilder<T>
    {
        private readonly Comparison<T> compare;

        public CTBSift(Comparison<T> comparisonDelegate)
        {
            if (comparisonDelegate == null)
                throw new ArgumentNullException("Comparison delegate cannot be null");
            compare = comparisonDelegate;
        }


        public override CTNode<T> BuildTree(T[] array)
        {
            CTNode<T> root = new CTNode<T>(array[0]);
            CTNode<T> previous = root;
            
            for (int i = 1; i < array.Length; i++)
            {
                previous = InsertNewNode(previous, array[i]);

                if (previous.Parent == null)
                    root = previous;
            }

            return root;
        }

        ///<summary>Insert new node</summary>
        ///<param name="prev">Previous inserted node</param>
        ///<param name="value">Value of new node</param>
        ///<returns>Returns new inserted node</returns>
        private CTNode<T> InsertNewNode(CTNode<T> prev, T value)
        {
            CTNode<T> newNode = new CTNode<T>(value);
            CTNode<T> currentParent = prev;

            while (IsBigger(currentParent.Value, value))
            {
                if (currentParent.Parent == null)
                    break;
                else currentParent = currentParent.Parent;
            }

            if (IsBigger(currentParent.Value, value))
            {
                //currentParent is root node
                //newNode will be the new root and its left child must be currentParent
                newNode.SetLeft(currentParent);
                currentParent.SetParent(newNode);
            }
            else
            {
                InsertToRight(currentParent, newNode);
            }


            return newNode;
        }

        ///<summary>
        ///Insert newNode to parent node as right child and also 
        ///replace parent right child as a left child of newNode
        ///</summary>
        private void InsertToRight(CTNode<T> parent ,CTNode<T> newNode)
        {
            newNode.SetParent(parent);
            newNode.SetLeft(parent.Right);

            if (parent.Right != null)
                parent.Right.SetParent(newNode);

            parent.SetRight(newNode);
        }

        ///<summary>Indicates whether v1 is greater than v2</summary>
        ///<return>True is v1 is bigger than v2 otherwise returns false</return>
        private bool IsBigger(T v1, T v2)
        {
            int compareResult = compare(v1, v2);
            return compareResult > 0;
        }
    }
}
