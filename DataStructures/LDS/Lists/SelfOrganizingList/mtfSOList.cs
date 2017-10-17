using System;
using System.Collections.Generic;

namespace DataStructures.LDS.Lists
{
    ///<summary>Move to front self-organizing list</summary>
    public class mtfSOList<T> : SOListBase<T>
    {
        private SONode<T> lastNode;

        public mtfSOList() : base(new SONode<T>())
        {
            lastNode = base.preHead;
        }

        #region SOListBase
        
        public override void Append(T value)
        {
            Insert(value);
        }
        
        public override void Prepend(T value)
        {
            Insert(value);
        }

        public override bool Contains(T value)
        {
            SONode<T> prev = base.preHead;
            SONode<T> next = prev.Next;
            
            for (;
                next != null;
                next = next.Next)
            {
                if (EqualityComparer<T>.Default.Equals(next.Value, value))
                {
                    prev.SetNext(next.Next);
                    next.SetNext(preHead.Next);
                    preHead.SetNext(next);
                    
                    return true;
                }
            }

            return false;
        }

        public override T GetHead()
        {
            if (size == 0)
                throw new InvalidOperationException("Cannot get head from empty list.");

            return preHead.Next.Value;
        }

        public override T[] GetTail(T origin)
        {
            SONode<T> iter = base.preHead.Next;
            int startIndex = 0;
            while (iter != null)
            {
                if (EqualityComparer<T>.Default.Equals(origin, iter.Value))
                {
                    int arraySize = size - startIndex - 1;
                    T[] tail = CreateTailArray(iter.Next, arraySize);

                    return tail;
                }
                iter = iter.Next;
                startIndex++;
            }

            T[] empty = new T[0];
            return empty;

        }

        public override void Insert(T value)
        {
            base.size++;
            SONode<T> newNode = new SONode<T>(value);
            lastNode.SetNext(newNode);
            lastNode = newNode;
        }

        public override bool IsEmpty()
        {
            return size == 0;
        }

        public override void Remove(T value)
        {
            SONode<T> current = base.preHead;

            while (current.Next != null)
            {
                if (EqualityComparer<T>.Default.Equals(current.Next.Value, value))
                {
                    current.SetNext(current.Next.Next);
                    current.Next.SetNext(null);
                    break;
                }
            }
        }

        #endregion


        private T[] CreateTailArray(SONode<T> firstElement, int size)
        {
            T[] array = new T[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = firstElement.Value;
                firstElement = firstElement.Next;
            }

            return array;
        }

    }
}
