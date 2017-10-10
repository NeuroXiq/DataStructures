using System;

namespace DataStructures.ADT
{
	public class Stack<T>
	{
		T[] array;
		int size;
		int capacity;
		
		public Stack()
		{
			array = new T[16];
			size = 0;
			capacity = 16;
		}
		
		public void Push(T item)
		{
			EnsureCapacity(size);
			array[size] = item;
			size++;
		}
		
		public T Top()
		{
			if(size > 0)
			{
				return array[size - 1];
			}
			else throw new Exception("Stack is empty !");
		}
		
		public T Pop()
		{
			T obj = Top();
			size--;
			return obj;
		}
		
		public bool IsEmpty()
		{
			return size == 0;
		}
		
		public bool IsFull()
		{
			return false;
		}
		
		public int GetSize()
		{
			return size;
		}
		
		private void EnsureCapacity(int newSize)
		{
			if(newSize >= capacity)
			{
				this.capacity = capacity * 2;
				Array.Resize(ref this.array, this.capacity);
			}
		}
	}
}