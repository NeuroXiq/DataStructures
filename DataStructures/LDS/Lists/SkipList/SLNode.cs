namespace DataStructures.LDS.Lists
{
    ///<summary>Internal generic class used as a node in <see cref="SkipList"/></summary>
    class SLNode<T>
    {
        ///<summary>Get level (height) of current node</summary>
        public int Level
        {
            get
            {
                return Next == null ? 0 : Next.Length;
            }
        }
        ///<summary>Array holds references to next nodes</summary>
        ///<remarks>node on index 0 is reference to next smallest node, all SLNode contains at least 1 Next node</remarks>
        public SLNode<T>[] Next { get; private set; }
        public T Key { get; private set; }

        ///<summary>Initialize new instance of SLNode class</summary>
        public SLNode()
        {
            this.Key = default(T);
            this.Next = null;
        }
        ///<summary>Initialize new instance of SLNode class</summary>
        ///<param name="Key">Key of current node</param>
        ///<param name="nextArray">Next array of current node</param>
        public SLNode(T key, SLNode<T>[] nextArray)
        {
            this.Key = key;
            this.Next = nextArray;
        }

        ///<summary>Checks if there is a node</summary>
        ///<param name="index">Index of <see cref="Next"/> array</param>
        ///<returns>False if node in <see cref="Next"/> is null. Otherwise returns true</returns>
        public bool NextExist(int index)
        {
            return Next[index] != null;
        }
        ///<summary>Gets key of node in <see cref="Next"/> array</summary>
        ///<param name="index">Index of <see cref="Next"/> array from which the value will be retrieved</param>
        ///<returns>Key of LSNode</returns>
        ///<exception cref="NullReferenceException"/>
        public T GetNextKey(int index)
        {
            return Next[index].Key;
        }

        ///<summary>Set <see cref="SLNode"/> in <see cref="Next"/> array.</summary>
        ///<param name="index">Index where new node will be inserted</param>
        ///<param name="next">New node to insert</param>
        ///<exception cref="IndexOutOfBoundsException"/>
        public void SetNext(int index, SLNode<T> next)
        {
            Next[index] = next;
        }
    }
}
