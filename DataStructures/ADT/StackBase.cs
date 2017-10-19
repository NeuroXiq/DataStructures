namespace DataStructures.ADT
{
    public abstract class StackBase<T>
    {
        public StackBase() { }

        public abstract void Push(T value);
        public abstract T Pop();
    }
}
