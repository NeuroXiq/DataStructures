using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LDS.Lists
{
    public abstract class SOListBase<T> : ADT.List<T>, IEnumerable<T>
    {
        protected SONode<T> preHead;
        protected int size;

        protected SOListBase(SONode<T> preHead)
        {
            this.preHead = preHead;
            this.size = 0;
        }

        public abstract void Insert(T value);

        #region IENUMERABLE

        public IEnumerator<T> GetEnumerator()
        {
            return new SOListIterator<T>(this.preHead);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SOListIterator<T>(this.preHead);
        }

        #endregion
    }
}
