using DataStructures.Trees.BinaryTrees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructuresTests.Trees.BinaryTrees.AVLTest
{
    [TestClass]
    public class AVLTests
    {
        [TestMethod]
        public void InsertOneKey_ContainsKey_RetursTrue()
        {
            AVL tree = new AVL();
            int key = 5;
            tree.Insert(key);
            bool containsResult = tree.ContainsKey(key);

            Assert.IsTrue(containsResult);
        }

        [TestMethod]
        public void InsertKey_DeleteKey_ContainsKey_ReturnsFalse()
        {
            AVL tree = new AVL();
            int key = 5;

            tree.Insert(key);
            tree.Delete(key);

            bool containsResult = tree.ContainsKey(key);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        public void EmptyTree_ContainsKey_RetursFalse()
        {
            AVL tree = new AVL();
            int key = 5;
            bool containsResult = tree.ContainsKey(key);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        public void Insert10Keys_ContainsAllKeys_ReturnTrue()
        {
            int[] keys = new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            AVL tree = new AVL();

            for (int i = 0; i < 10; i++)
                tree.Insert(keys[i]);

            bool containsAllKeys = true;
            for (int i = 0; i < 10; i++)
                containsAllKeys = containsAllKeys && tree.ContainsKey(keys[i]);
            Assert.IsTrue(containsAllKeys);
        }

        [TestMethod]
        public void Insert10Keys_ContainsNotInsertedKey_ReturnsFalse()
        {
            int[] keys = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int noInsertedKey = 123456;

            AVL tree = new AVL();

            for (int i = 0; i < 10; i++)
                tree.Insert(keys[i]);

            bool containsKey = tree.ContainsKey(noInsertedKey);
            Assert.IsFalse(containsKey);
        }
    }
}
