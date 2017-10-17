namespace DataStructures.ADT
{
    // TODO add Remove method
    
    ///<summary>Base prototype of abstract data type - List</summary>
    public abstract class List<T>
    {
        public List()
        {

        }

        /* Abstract methods */

        public abstract bool IsEmpty();
        public abstract bool Contains(T value);
        public abstract void Prepend(T value);
        public abstract void Append(T value);
        public abstract T GetHead();
        public abstract T[] GetTail(T origin);
        public abstract void Remove(T value);
    }
}
