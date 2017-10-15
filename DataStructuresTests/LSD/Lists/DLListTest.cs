using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.LDS.Lists;
using System;

namespace DataStructuresTests.LSD.Lists
{
    [TestClass]
    public abstract class DLListTest<T>
    {
        protected abstract T CreateSampleValue();
        protected abstract T CreateDifferentValue();

        [TestMethod]
        public void AppendValueToEmptyList_ListContainsValue_ReturnsTrue()
        {
            T valueToAdd = this.CreateSampleValue();

            DLList<T> list = new DLList<T>();
            list.Append(valueToAdd);

            T valueToSeek = this.CreateSampleValue();
            bool listContains = list.Contains(valueToSeek);

            Assert.IsTrue(listContains);
        }

        [TestMethod]
        public void PrependValueToEmptyList_ListContainsValue_ReturnsTrue()
        {
            T value = this.CreateSampleValue();
            
            DLList<T> list = new DLList<T>();
            list.Prepend(value);

            T valueToSeek = this.CreateSampleValue();

            bool listContains = list.Contains(valueToSeek);

            Assert.IsTrue(listContains);
        }

        [TestMethod]
        public void AppendValueToEmptyList_ListDontContainsDifferentValue_ReturnsFalse()
        {
            T value = this.CreateSampleValue();
            DLList<T> list = new DLList<T>();

            list.Append(value);

            T differentValue = this.CreateDifferentValue();
            bool containsDifferentValue = list.Contains(differentValue);

            Assert.IsFalse(containsDifferentValue);
        }
        [TestMethod]
        public void PrependValueToEmptyList_ListDontContainsDifferentValue_ReturnsFalse()
        {
            T value = this.CreateSampleValue();
            DLList<T> list = new DLList<T>();
            list.Prepend(value);
            T differentValue = this.CreateDifferentValue();
            bool containsDifferentValue = list.Contains(differentValue);

            Assert.IsFalse(containsDifferentValue);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetTailOfEmptyList_ThrowsInvalidOperationException()
        {
            DLList<T> list = new DLList<T>();
            T sampleValue = CreateSampleValue();

            list.GetTail(sampleValue);
        }
        [TestMethod]
        public void GetTailOfSingleElementList_ReturnsZeroLengthArray()
        {
            DLList<T> list = new DLList<T>();
            T sampleValue = this.CreateSampleValue();
            list.Append(sampleValue);

            T[] tailOfList = list.GetTail(sampleValue);
            int tailLength = tailOfList.Length;

            Assert.AreEqual(0, tailLength);
        }
        [TestMethod]
        public void
            AppendTwoElements_GetTailOfFirstElementInTwoElementsList_ReturnsArrayWithOneElementSecondInList()
        {
            DLList<T> list = new DLList<T>();
            T firsElement = this.CreateDifferentValue();
            T secondElement = this.CreateSampleValue();

            list.Append(firsElement);
            list.Append(secondElement);

            T[] resultTail = list.GetTail(firsElement);
            T[] expectedTail = new T[] { secondElement };

            CollectionAssert.AreEqual(expectedTail, resultTail);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetHeadElementFromEmptyList_ThrowsInvalidOperationException()
        {
            DLList<T> list = new DLList<T>();
            list.GetHead();
        }

        [TestMethod]
        public void GetHeadElementFromOneElementList_ReturnsElement()
        {
            T sampleValue = this.CreateSampleValue();

            DLList<T> list = new DLList<T>();
            list.Append(sampleValue);
            T headValue = list.GetHead();

            Assert.AreEqual(sampleValue, headValue);

        }


    }
}
