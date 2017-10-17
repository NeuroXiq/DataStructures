namespace DataStructures.LDS.Lists
{
    public class SONode<T>
    {
        public T Value { get; private set; }
        public SONode<T> Next { get; private set; }

        public SONode(SONode<T> next, T value)
        {
            this.Next = next;
            this.Value = value;
        }

        public SONode() : this(null, default(T)) { }

        public SONode(SONode<T> next) : this(next, default(T))
        { }

        public SONode(T value) : this(null, value)
        {}

        public void SetNext(SONode<T> next)
        {
            this.Next = next;
        }
    }
}
