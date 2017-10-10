using System;

namespace DataStructures.HashTables
{
	/*
	*
	* Implementation of separate chaining with linked list hash array.
	*
	*/
	
	public class LLHashTable : HashTableBase
	{
		private const int INIT_TABLE_LENGTH = 16;
		private const double MAX_LOAD_FACTOR = (double)0.99;
		
		int entries;
		public ListNode[] table; // TEST PuBLIC
		
		public LLHashTable() : this(new HashProviderBase())
		{
			
		}
		
		public LLHashTable(HashProviderBase hashProvider) : base(hashProvider)
		{
			this.entries = 0;
			this.table = new ListNode[INIT_TABLE_LENGTH];
		}
		
		
		
		#region ABSTRACT METHODS IMPLEMENTATION
		
		public override object GetValue(object key)
		{
			bool success;
			
			object value = GetValueFromTable(key, this.table, out success);
			if(success)
			{
				return value;
			}
			else
			{
				return null;
			}
			
		}
		
		public override void SetValue(object key, object value)
		{
			ExtendTableIfNeed();
			
			bool newSlotOccupied = SetValueInTable(key, value, this.table);
			
			if(newSlotOccupied)
			{
				this.entries++;
			}
			
		}
		
		public override void Remove(object key)
		{
			/* need to implement */
		}
		
		public override LoadFactor GetLoadFactor()
		{
			return new LoadFactor(this.entries, table.Length);
		}

		
		#endregion
		
		
		#region PRIVATE METHODS
		
		private object GetValueFromTable(object key, ListNode[] table, out bool success)
		{
			bool found = false;
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, table.Length);
			object value = null;  
			
			if(table[index] != null)
			{
				ListNode node = table[index];
				while(node != null && !found)
				{
					if(node.Slot.Key == key)
					{
						found = true;
						value = node.Slot.Value;
					}
					else
					{
						node = node.NextNode;
					}
				}
			}
			
			success = found;
			
			return value;
		}
		
		private void ExtendTableIfNeed()
		{
			double factor = GetLoadFactor().Factor;
			
			if(factor > MAX_LOAD_FACTOR)
			{
				int newSize = table.Length * 2;
				ListNode[] newTable = new ListNode[newSize];
				
				Rehash(this.table, newTable);
				
				this.table = newTable;
			}
		}
		
		private void Rehash(ListNode[] source, ListNode[] destination)
		{
			for(int i = 0; i < source.Length; i++)
			{
				ListNode listNode = source[i];
				while(listNode != null) // iterating on all items in list
				{
					SetValueInTable(listNode.Slot.Key, listNode.Slot.Value, destination);
					listNode = listNode.NextNode;
				}
			}
		}
		
		private bool SetValueInTable(object key, object value, ListNode[] table)
		{
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, table.Length);
			bool sameKeyFound = false;
			
			HashSlot newSlot = new HashSlot(key, value);
			
			ListNode lastNode = table[index];
			
			if(lastNode != null)
			{
				bool search = true;
				while(search)
				{
					if(lastNode.Slot.Key == key)
					{
						sameKeyFound = true;
						search = false;
					}
					else
					{
						if(lastNode.NextNode != null)
						{
							lastNode = lastNode.NextNode;
						}
						else
						{
							search = false;
						}
					}
				}
			}
			
			if(sameKeyFound)
			{
				lastNode.SetHashSlot(newSlot);
			}
			else
			{
				if(lastNode == null)
				{
					table[index] = new ListNode(newSlot);
				}
				else
				{
					lastNode.SetNextNode(new ListNode(newSlot));
				}
			}
			
			return !sameKeyFound;
		}
		
		private int GetIndexFromHash(int hash, int tableSize)
		{
			int index = hash;
			
			if(index < 0)
			{
				index = -index;
			}
			
			index = index % tableSize;
			
			return index;
			
		}
		
		#endregion
		
	}
}