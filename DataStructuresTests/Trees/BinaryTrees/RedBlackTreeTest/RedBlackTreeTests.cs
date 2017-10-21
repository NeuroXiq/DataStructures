using DataStructures.Trees.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataStructuresTests.Trees.BinaryTrees.RedBlackTreeTest
{
    [TestClass]
    public class RedBlackTreeTests
    {
        private Comparison<int> intComparison = delegate (int a, int b) { return a - b; };

        [TestMethod]
        public void EmptyTree_ContainsKey_ReturnsFalse()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int notInserted = 5;
            bool containsResult = tree.Contains(notInserted);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        public void InsertOneKey_ContainsAnotherKey_ReturnsFalse()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int addedKey = 0;
            int notAddedKey = -1;

            tree.Insert(addedKey);
            bool containsResult = tree.Contains(notAddedKey);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InsertKey_InsertSameKey_ThrowsException()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int key = 0;
            tree.Insert(key);
            tree.Insert(key);
        }

        [TestMethod]
        public void InsertKey_ContainsThisKey_ReturnsTrue()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int key = 0;

            tree.Insert(key);
            bool containsResult = tree.Contains(key);

            Assert.IsTrue(containsResult);
        }

        [TestMethod]
        public void Insert10Keys_ContainsAllKeys_ReturnsTrue()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int size = 10;
            int[] keys = new int[size];
            for (int i = 0; i < size; i++)
            {
                keys[i] = i;
                tree.Insert(keys[i]);
            }

            bool containsAll = true;
            for (int i = 0; i < size; i++)
            {
                containsAll = containsAll && tree.Contains(keys[i]);
            }

        }

        [TestMethod]
        public void Insert10Keys_ContainsNonExistentKey_ReturnsFalse()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int size = 10;
            int[] keys = new int[size];
            for (int i = 0; i < size; i++)
            {
                keys[i] = i;
                tree.Insert(keys[i]);
            }
            int notExistKey = int.MinValue;
            bool containsResult = tree.Contains(notExistKey);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        public void Insert100Keys_ContainsKeys_ReturnTrue()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int size = 100;
            int[] keys = new int[size];
            for (int i = 0; i < size; i++)
            {
                keys[i] = i;
                tree.Insert(keys[i]);
            }

            bool containsAll = true;
            for (int i = 0; i < size; i++)
            {
                containsAll = containsAll && tree.Contains(keys[i]);
            }
        }

        [TestMethod]
        public void Insert100Keys_ContainsNotInsertedKey_ReturnsFalse()
        {
            RBTree<int> tree = new RBTree<int>(intComparison);
            int size = 10;
            int[] keys = new int[size];
            for (int i = 0; i < size; i++)
            {
                keys[i] = i;
                tree.Insert(keys[i]);
            }
            int notExistKey = int.MinValue;
            bool containsResult = tree.Contains(notExistKey);

            Assert.IsFalse(containsResult);
        }
    }
}
