using DataStructures.LDS.Common;
using DataStructures.LDS.Lists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataStructuresTests.LSD.Common
{
    [TestClass]
    public class aStackTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ThrowsException()
        {
            aStack<object> stack = new aStack<object>();
            stack.Pop();
        }

        [TestMethod]
        public void PushOneElement_PopElement_ElementAreSame()
        {
            aStack<object> stack = new aStack<object>();
            object testElement = new object();
            stack.Push(testElement);
            object popedElement = stack.Pop();

            Assert.AreEqual(testElement, popedElement);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PushTenElements_Pop11Elements_ThrowsException()
        {
            aStack<object> stack = new aStack<object>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(new object());
            }
            for (int i = 0; i < 11; i++)
            {
                stack.Pop();
            }
            
        }

        [TestMethod]
        public void PushTenElements_PopSame10Elements()
        {
            object[] testArray = new object[10];
            for (int i = 0; i < 10; i++)
                testArray[i] = new object();

            aStack<object> stack = new aStack<object>();
            for (int i = 0; i < 10; i++)
            {
                stack.Push(testArray[i]);
            }
            bool allAreSame = true;
            for (int i = 0; i < 10; i++)
            {
                allAreSame = allAreSame && (testArray[9 - i] == stack.Pop());
            }

            Assert.IsTrue(allAreSame);
        }

        [TestMethod]
        public void Push100Elements_PopSame100Elements()
        {
            object[] testArray = new object[100];
            for (int i = 0; i < 100; i++)
                testArray[i] = new object();

            aStack<object> stack = new aStack<object>();
            for (int i = 0; i < 100; i++)
            {
                stack.Push(testArray[i]);
            }
            bool allAreSame = true;
            for (int i = 0; i < 100; i++)
            {
                allAreSame = allAreSame && (testArray[99 - i] == stack.Pop());
            }

            Assert.IsTrue(allAreSame);
        }
    }
}
