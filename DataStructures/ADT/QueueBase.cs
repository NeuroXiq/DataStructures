namespace DataStructures.ADT
{
    ///<summary>Base of abstract data type  - Queue</summary>
    public abstract class QueueBase<T>
    {
        public QueueBase() { }


        public abstract void Enqueue(T value);
        public abstract T Dequeue();
        public abstract bool IsEmpty();
    }
}
