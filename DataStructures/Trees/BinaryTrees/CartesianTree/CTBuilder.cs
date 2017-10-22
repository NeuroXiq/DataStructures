namespace DataStructures.Trees.BinaryTrees
{
    ///<summary>Cartesian tree builder interface</summary>
    public abstract class CTBuilder<T>
    {
        ///<summary>Build tree</summary>
        ///<param name="array">Base of tree structure array</param>
        ///<returns>Returns root <see cref="CTNode{T}"/> node of the tree</returns>
        public abstract CTNode<T> BuildTree(T[] array);
    }
}
