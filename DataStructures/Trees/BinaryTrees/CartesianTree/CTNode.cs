namespace DataStructures.Trees.BinaryTrees
{
    ///<summary>Internal class used as node in <see cref="CartesianTree{T}"/></summary>
    public class CTNode<T>
    {
        ///<summary>Left child of current node</summary>
        public CTNode<T> Left { get; private set; }
        ///<summary>Right child of current node</summary>
        public CTNode<T> Right { get; private set; }
        ///<summary>Parent of current node</summary>
        public CTNode<T> Parent { get; private set; }
        ///<summary>Value of current node</summary>
        public T Value { get; private set; }

        ///<summary>Initialize new instance of <see cref="CTNode{T}"/></summary>
        public CTNode() : this(null, null, null, default(T)) { }

        ///<summary>Initialize new instance of <see cref="CTNode{T}"/></summary>
        ///<param name="value">Value to assign to this node</param>
        public CTNode(T value) : this(null, null, null, value) { }
        
        ///<summary>Initialize new instance of <see cref="CTNode{T}"/></summary>
        ///<param name="left">Left child</param>
        ///<param name="right">Right child</param>
        ///<param name="value">Value of current node</param>
        public CTNode(CTNode<T> left, CTNode<T> right, CTNode<T> parent, T value)
        {
            Left = left;
            Right = right;
            Value = value;
            Parent = parent;
        }

        ///<summary>Set parent of current node</summary>
        ///<param name="parent"><see cref="CTNode{T}"/> to be assigned as a <see cref="Parent"/> of current node</param>
        public void SetParent(CTNode<T> parent)
        {
            Parent = parent;
        }

        ///<summary>Set left child</summary>
        ///<param name="left"><see cref="CTNode{T}"/> to be assing as a <see cref="Left"/> child</param>
        public void SetLeft(CTNode<T> left)
        {
            Left = left;
        }

        ///<summary>Set right child</summary>
        ///<param name="left"><see cref="CTNode{T}"/> to be assing as a <see cref="Right"/> child</param>
        public void SetRight(CTNode<T> right)
        {
            Right = right;
        }

        ///<summary>Set value of current node</summary>
        ///<param name="value">Value to assign as a current <see cref="Value"/></param>
        public void SetValue(T value)
        {
            Value = value;
        }
    }
}
