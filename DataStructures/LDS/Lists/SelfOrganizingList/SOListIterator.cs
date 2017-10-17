using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LDS.Lists
{
    public class SOListIterator<T> : IEnumerator<T>
    {
        private SONode<T> current;
        private SONode<T> preHead;

        public SOListIterator(SONode<T> head)
        {
            this.current = head;
            this.preHead = head;
        }

        public T Current
        {
            get
            {
                if (current != null)
                    return current.Value;
                else throw new ArgumentNullException("Current node is null");
            }
        }

        object IEnumerator.Current
        {
            get
            {
                if (current != null)
                    return (object)current.Value;
                else throw new ArgumentNullException("Current node is null");
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (current != null)
                current = current.Next;
            return current != null;
        }

        public void Reset()
        {
            current = preHead;
        }
    }
}
