using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.LDS.Lists
{
    public class DLListNode<T>
    {
        public T Value { get; private set; }
        public DLListNode<T> Next { get; private set; }
        public DLListNode<T> Previous { get; private set; }

        public DLListNode(T value, DLListNode<T> previous, DLListNode<T> next)
        {
            this.Value = value;
            this.Previous = previous;
            this.Next = next;
        }

        public void SetNext(DLListNode<T> nextNode)
        {
            this.Next = nextNode;
        }

        public void SetPrevious(DLListNode<T> previousNode)
        {
            this.Previous = previousNode;
        }   
    }
}
