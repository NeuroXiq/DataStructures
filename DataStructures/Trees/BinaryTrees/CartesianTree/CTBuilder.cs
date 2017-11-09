using DataStructures.Trees.BinaryTrees.CartesianTree;

namespace DataStructures.Trees.BinaryTrees
{
    ///<summary>Cartesian tree builder</summary>
    ///<remarks>Strategy design pattern</remarks>
    public class CTBuilder<T>
    {
        protected CTBuildAlgorithm<T> buildAlgorithm;

        public CTBuilder(CTBuildAlgorithm<T> algorithm)
        {
            buildAlgorithm = algorithm;
        }

        public CTNode<T> CreateTree(T[] array)
        {
            return buildAlgorithm.BuildTree(array);
        }
    }
}
