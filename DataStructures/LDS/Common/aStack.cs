using System;
using DataStructures.ADT;

namespace DataStructures.LDS.Common
{
    ///<summary>Array based stack</summary>
    public class aStack<T> : StackBase<T>
    {
        private T[] stackArray;
        private int capacity;
        private int valuesCount;

        ///<summary>Creates new instance of <see cref="aStack{T}"/></summary>
        public aStack()
        {
            capacity = 16;
            valuesCount = 0;
            stackArray = new T[16];
        }

        ///<summary>Pop value from stack</summary>
        public override T Pop()
        {
            if (valuesCount > 0)
            {
                valuesCount--;
                return stackArray[valuesCount];
            }
            else throw new InvalidOperationException("Cannot pop value from empty stack");
        }
        ///<summary>push value on stack</summary>
        public override void Push(T value)
        {
            if (valuesCount >= capacity)
            {
                ExpandStack();
            }

            stackArray[valuesCount] = value;

            valuesCount++;
        }

        ///<summary>Chech if stack is empty</summary>
        ///<returns>True if stack is empty otherwise return false</returns>
        public bool IsEmpty()
        {
            return valuesCount == 0;
        }

        ///<summary>Resize stackArray</summary>
        private void ExpandStack()
        {
            int newSize = capacity * 2;
            T[] newStack = new T[newSize];

            for (int i = 0; i < valuesCount; i++)
            {
                newStack[i] = stackArray[i];
            }

            stackArray = newStack;
            capacity = newSize;
        }
    }
}
