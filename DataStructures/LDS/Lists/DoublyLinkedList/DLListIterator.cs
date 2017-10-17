using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LDS.Lists
{
    public class DLListIterator<T> : IEnumerator<T>
    {
        private DLListNode<T> beforeHeadNode;
        private DLListNode<T> afterEndNode;
        private DLListNode<T> current;

        public DLListIterator(DLListNode<T> beforeHeadNode, DLListNode<T> afterEndNode)
        {
            this.beforeHeadNode = beforeHeadNode;
            this.afterEndNode = afterEndNode;
            this.current = beforeHeadNode;
        }

        public DLListNode<T> GetCurrentNode()
        {
            return current;
        }

        #region IEnumerator

        public T Current
        {
            get
            {
                return current.Value;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return (object)current.Value;
            }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            current = current.Next;
            return current != afterEndNode;
        }

        public bool MovePrevious()
        {
            current = current.Previous;
            return current != beforeHeadNode;
        }

        public void Reset()
        {
            this.current = beforeHeadNode;
        }

        #endregion
    }
}
