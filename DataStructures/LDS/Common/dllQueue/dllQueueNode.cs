
namespace DataStructures.LDS.Common
{
    ///<summary>Internal class used in <see cref="dllQueue{T}"/> class</summary>
    internal class dllQueueNode<T>
    {
        ///<summary>Get next node of current node</summary>
        public dllQueueNode<T> Next { get; private set; }
        ///<summary>Get previous node of current node</summary>
        public dllQueueNode<T> Prev { get; private set; }
        ///<summary>Get value of current node</summary>
        public T Value { get; private set; }

        public dllQueueNode() { }
        public dllQueueNode(T value) : this(null, null, value) { }

        ///<summary>Creates new instance of <see cref="dllQueue{T}"/> class</summary>
        ///<param name="prev">Previous node of current node</param>
        ///<param name="next">Next node of current node</param>
        ///<param node="value">Value of current node</param>
        public dllQueueNode(dllQueueNode<T> prev, dllQueueNode<T> next, T value)
        {
            Next = next;
            Prev = prev;
            Value = value;
        }

        ///<summary> Set <see cref="Next"/> node</summary>
        ///<param name="next">New <see cref="dllQueue{T}"/> node to be set</param>
        public void SetNext(dllQueueNode<T> next)
        {
            this.Next = next;
        }
    }
}
