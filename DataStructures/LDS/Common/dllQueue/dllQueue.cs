using System;
using DataStructures.ADT;

namespace DataStructures.LDS.Common
{
    ///<summary>Implementation of doubly linked list queue</summary>
    public class dllQueue<T> : QueueBase<T>
    {
        private dllQueueNode<T> listHead;
        private dllQueueNode<T> lastNode;
        private int size;

        ///<summary>Create new instance of <see cref="dllQueue{T}"/> class</summary>
        public dllQueue()
        {
            listHead = new dllQueueNode<T>();
            lastNode = listHead;
        }

        ///<summary>Retrieve <see cref="{T}"/> element from Queue</summary>
        public override T Dequeue()
        {
            if (size > 0)
            {
                size--;
                T value = listHead.Next.Value;
                listHead.SetNext(listHead.Next.Next);

                return value;
            }
            else throw new InvalidOperationException("Cannot get element from empty queue");
        }

        ///<summary>Put new <see cref="{T}"/> element into queue </summayr>
        public override void Enqueue(T value)
        {
            dllQueueNode<T> newNode = new dllQueueNode<T>(lastNode, null, value);
            lastNode.SetNext(newNode);
            lastNode = newNode;
            size++;
        }

        ///<summary>Test an queue for empty condition</summary>
        ///<returns>True if queue is empty otherwise returns false</returns>
        public override bool IsEmpty()
        {
            return size == 0;
        }
    }
}
