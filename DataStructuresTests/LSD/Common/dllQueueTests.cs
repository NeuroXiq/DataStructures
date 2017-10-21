using DataStructures.LDS.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DataStructuresTests.LSD.Common
{
    [TestClass]
    public class dllQueueTests
    {

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptyQueue_Dequeue_ThrowsException()
        {
            dllQueue<int> queue = new dllQueue<int>();
            queue.Dequeue();
        }

        [TestMethod]
        public void EmptyQueue_IsEmpty_RetursTrue()
        {
            dllQueue<int> queue = new dllQueue<int>();
            bool isEmptyResult = queue.IsEmpty();

            Assert.IsTrue(isEmptyResult);
        }

        [TestMethod]
        public void EnqueueOneElement_IsEmpty_RetursFalse()
        {
            dllQueue<int> queue = new dllQueue<int>();
            int valueToEnqueue = -1;

            queue.Enqueue(valueToEnqueue);
            bool isEmptyResult = queue.IsEmpty();

            Assert.IsFalse(isEmptyResult);
        }

        [TestMethod]
        public void EnqueueOneElement_DequeueOnce_IsEmpty_ReturnsTrue()
        {
            dllQueue<int> queue = new dllQueue<int>();
            int element = -1;
            queue.Enqueue(element);
            queue.Dequeue();
            bool isEmptyResult = queue.IsEmpty();

            Assert.IsTrue(isEmptyResult);
        }

        [TestMethod]
        public void EnqueueTwoElements_DequeueOnce_IsEmpty_ReturnsFalse()
        {
            dllQueue<int> queue = new dllQueue<int>();
            int firstElement = 1;
            int secElement = 2;

            queue.Enqueue(firstElement);
            queue.Enqueue(secElement);
            queue.Dequeue();

            bool isEmptyRes = queue.IsEmpty();

            Assert.IsFalse(isEmptyRes);
        }

        [TestMethod]
        public void EnqueueOneElement_Dequeue_BothElementsAreEqual()
        {
            dllQueue<int> queue = new dllQueue<int>();
            int element = -1;
            queue.Enqueue(element);
            int dequeueResult = queue.Dequeue();

            Assert.AreEqual(element, dequeueResult);
        }

        [TestMethod]
        public void EnqueueTwoElement_Dequeue_ReturnsFirstElement()
        {
            dllQueue<int> queue = new dllQueue<int>();
            int firstElement = 1;
            int secElement = 2;

            queue.Enqueue(firstElement);
            queue.Enqueue(secElement);
            int dequeueElement = queue.Dequeue();

            Assert.AreEqual(firstElement, dequeueElement);
        }

        [TestMethod]
        public void Enqueue100Elements_DequeueOnec_ReturnFirstElement()
        {
            int size = 100;
            int[] elements = new int[size];
            dllQueue<int> queue = new dllQueue<int>();

            for (int i = 0; i < size; i++)
            {
                elements[i] = i;
                queue.Enqueue(elements[i]);
            }

            int dequeueElement = queue.Dequeue();
            Assert.AreEqual(elements[0], dequeueElement);
            
        }

        [TestMethod]
        public void Enqueue100Elements_DequeueAllElements_AllElementsAreEqual()
        {
            int size = 100;
            int[] elements = new int[size];
            dllQueue<int> queue = new dllQueue<int>();

            for (int i = 0; i < size; i++)
            {
                elements[i] = i;
                queue.Enqueue(elements[i]);
            }

            int[] dequeueArray = new int[size];
            for (int i = 0; i < size; i++)
            {
                dequeueArray[i] = queue.Dequeue();
            }

            CollectionAssert.AreEqual(elements, dequeueArray);
        }




    }
}
