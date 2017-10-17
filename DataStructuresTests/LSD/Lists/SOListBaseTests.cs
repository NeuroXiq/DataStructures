using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStructures.LDS.Lists;
using System;

namespace DataStructuresTests.LSD.Lists
{
    [TestClass]
    public abstract class SOListTests<T>
    {
        public abstract T GetValue1();
        public abstract T GetValue2();
        public abstract T GetValue3();

        public abstract SOListBase<T> GetTestClass();

        [TestMethod]
        public void Insert_AddOneElement_ListContainsElement_ReturnsTrue()
        {
            SOListBase<T> list = GetTestClass();
            T value1 = GetValue1();
            list.Insert(value1);

            bool containsResult = list.Contains(value1);

            Assert.IsTrue(containsResult);
        }

        [TestMethod]
        public void ContainsValue_ListIsEmpty_ReturnsFalse()
        {
            SOListBase<T> list = GetTestClass();
            T value1 = GetValue1();
            bool containsResult = list.Contains(value1);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        public void InsertFirstElement_ListContainsSecondElement_ReturnsFalse()
        {
            SOListBase<T> list = GetTestClass();
            T value1 = GetValue1();
            T value2 = GetValue2();

            list.Insert(value1);
            bool containsResult = list.Contains(value2);

            Assert.IsFalse(containsResult);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetHead_EmptyList_ThrowsException()
        {
            SOListBase<T> list = GetTestClass();
            T head = list.GetHead();

            
        }

    }
}
