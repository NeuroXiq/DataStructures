using DataStructures.ADT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using DataStructures.LDS.Lists;

namespace DataStructures.LDS.Lists
{
    ///<summary>Implementation of doubly linked list data structure</summary>
    public class DLList<T> : ADT.List<T>, IEnumerable<T>
    {
        private DLListNode<T> beforeHeadNode;
        private DLListNode<T> afterEndNode;
        private int size;

        ///<summary>Creates new instance of <see cref="DLList{T}"></summary>
        public DLList()
        {
            this.size = 0;
            this.beforeHeadNode = new DLListNode<T>(default(T), null, afterEndNode);
            this.afterEndNode = new DLListNode<T>(default(T), beforeHeadNode, null);

            //its little tricky, prev and after always points to itself
            //this will help in IEnumerator implementation in DLListIterator class

            beforeHeadNode.SetPrevious(beforeHeadNode);
            beforeHeadNode.SetNext(afterEndNode);
            afterEndNode.SetNext(afterEndNode);
            afterEndNode.SetPrevious(beforeHeadNode);
            
        }

        #region ADT.LIST ABSTRACT METHODS

        ///<summary>Insert element at the end of the list</summary>
        ///<param name="value">Value to append</param>
        public override void Append(T value)
        {
            size++;

            DLListNode<T> after = afterEndNode.Previous;
            DLListNode<T> newNode = new DLListNode<T>(value, after, afterEndNode);
            after.SetNext(newNode);
            afterEndNode.SetPrevious(newNode);
        }

        ///<summary>Search for an element in the list</summary>
        ///<param name="value">Value to search</param>
        ///<retuns>True if element exist in list otherwise returns false</retuns>
        public override bool Contains(T value)
        {
            return GetIndexOf(value) > -1;
        }

        ///<summary>Get first element of the list</summary>
        ///<returns>First element in list</returns>
        ///<exception cref="InvalidOperationException">If list is empty</exception>
        public override T GetHead()
        {
            if (size > 0)
            {
                return beforeHeadNode.Next.Value;
            }

            throw new InvalidOperationException("Cannot get head of empty list");
        }
        ///<summary>Get all elements after specified element</summary>
        ///<param name="origin">Element after which array will be builded</param>
        ///<returns>Array of <see cref="T"> elements </returns>
        public override T[] GetTail(T origin)
        {
            IEnumerator<T> iterator;
            int start = GetIndexOf(origin, out iterator);
            if (start > -1)
            {
                int arraySize = size - start - 1;
                T[] tailArray = new T[arraySize];

                for (int i = 0; i < arraySize; i++)
                {
                    iterator.MoveNext();
                    tailArray[i] = iterator.Current;
                }

                return tailArray;

            }
            else throw new InvalidOperationException("Cannot get tail from non-existing origin value");
        }

        ///<summary>Check is the list is empty</summary>
        ///<returns>False if list contains at least one element otherwise returns true</returns>
        public override bool IsEmpty()
        {
            return size == 0;
        }

        ///<summary>Insert element at the begining of the list</summary>
        ///<param name="value">Value to insert</param>
        public override void Prepend(T value)
        {
            size++;

            DLListNode<T> newNode = new DLListNode<T>(value, beforeHeadNode, beforeHeadNode.Next);
            beforeHeadNode.SetNext(newNode);
            beforeHeadNode.Next.SetPrevious(newNode);
        }

        #endregion

        private int GetIndexOf(T value)
        {
            IEnumerator<T> ignore;
            return GetIndexOf(value, out ignore);
        }

        //returns iterator which points to founded element or (if not found) to afterEndNode
        //it can help if we need to do something else with elements near to 'value'
        private int GetIndexOf(T value, out IEnumerator<T> iteratorStop)
        {
            int index = 0;
            IEnumerator<T> iterator = GetEnumerator();

            while(iterator.MoveNext())
            {
                if (EqualityComparer<T>.Default.Equals(value, iterator.Current))
                {
                    iteratorStop = iterator;
                    return index;
                }
                index++;
            }

            iteratorStop = iterator;
            return -1;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new DLListIterator<T>(beforeHeadNode, afterEndNode);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new DLListIterator<T>(beforeHeadNode, afterEndNode);
        }
    }
}
