namespace DataStructures.Trees.BinaryTrees
{
    public class RBNode<T>
    {
        private T value;

        public RBNode<T> Right { get; private set; }
        public RBNode<T> Left { get;  private set; }
        public RBNode<T> Parent { get; private set; }
        public Color Color { get; private set; }
        public T Value { get; private set; }

        public RBNode() : this(null,null,default(Color)) { }
        public RBNode(Color color) : this(null, null, color) { }
        public RBNode(RBNode<T> left, RBNode<T> right) : this(left, right, default(Color)) { }
        public RBNode(RBNode<T> left, RBNode<T> right, Color color)
        {
            Left = left;
            Right = right;
            Color = color;
        }

        public RBNode(T value)
        {
            this.value = value;     
        }

        public void SetLeft(RBNode<T> left)
        {
            Left = left;
        }

        public void SetRight(RBNode<T> right)
        {
            Right = right;
        }

        public void SetParent(RBNode<T> parent)
        {
            Parent = parent;
        }

        public void SetColor(Color color)
        {
            Color = color;
        }
    }
}
