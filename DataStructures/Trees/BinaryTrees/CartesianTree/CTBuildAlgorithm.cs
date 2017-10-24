namespace DataStructures.Trees.BinaryTrees.CartesianTree
{
    public abstract class CTBuildAlgorithm<T>
    {
        ///<summary>Build tree from specified array</summary>
        ///<param name="array">Base array to build tree</param>
        ///<returns>Returns root <see cref="CTNode{T}"/> node of builded tree</returns>
        public abstract CTNode<T> BuildTree(T[] array);
    }
}
