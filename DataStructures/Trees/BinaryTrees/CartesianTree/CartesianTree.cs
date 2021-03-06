﻿
using System;

namespace DataStructures.Trees.BinaryTrees
{
    public class CartesianTree<T> 
    {
        public CTNode<T> root;
        private Comparison<T> compare;

        
        public CartesianTree(Comparison<T> comparisonDelegate)
        {
            if (comparisonDelegate == null)
                throw new ArgumentNullException("comparisonDelegate cannot be null value. You must specify comparison process");

            compare = comparisonDelegate;
        }

        public void BuildNewTree(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException("Cannot build CartesianTree based on null array");
            else if (array.Length == 0)
                throw new InvalidOperationException("Cannot build cartesian tree based on empty array");

            BuildTreeFromArray(array);
        }

        public bool Contains()
        {
            return false;
            
        }


        private void BuildTreeFromArray(T[] array)
        {
            // choosing algorithm to build tree (today is only one algorithm named 'CTBSift')

            CTBSift<T> siftBuildAlgorithm = new CTBSift<T>(compare);
            CTBuilder<T> ctBuilder = new CTBuilder<T>(siftBuildAlgorithm);

            root = ctBuilder.CreateTree(array);
        }

    }
}
