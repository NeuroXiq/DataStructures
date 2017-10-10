using System;

namespace DataStructures.HashTables
{
	/*
	*
	* Implementation of linear probing hash table.
	*
	*
	*/
	
	public class LPHashTable : HashTableBase
	{
		private const double MAX_LOAD_FACTOR = (double)0.7;
		
		private HashSlot[] table; // test public
		private int entries;
		
		
		public LPHashTable() : this(new HashProviderBase())
		{
			
		}
		
		public LPHashTable(HashProviderBase hashProvider) : base(hashProvider)
		{
			this.table = new HashSlot[16];
			this.entries = 0;
		}
		
		#region ABSTRACT METHODS IMPLEMENTATION
		
		public override object GetValue(object key)
		{
			if(key == null)
				return null;
			
			bool success;
			object value = GetValueFromTable(key, this.table, out success);
			
			if(success)
			{
				return value;
			}
			else 
			{
				throw new InvalidOperationException("Cannot find value with corresponding key");
				
			}
		}
		
		public override void SetValue(object key, object value)
		{
			ResizeTableIfNeed();
			SetValueInTable(key, value, this.table);
			
			entries++;
		}
		
		public override void Remove(object key)
		{
			/* Need implementation */
			/*
			 * An easy technique is to:
             * 
			 * Find and remove the desired element
			 * Go to the next bucket
			 * If the bucket is empty, quit
			 * If the bucket is full, delete the element in that bucket and re-add it to the hash table using the normal means.
			 * The item must be removed before re-adding, because it is likely that the item could be added back into its original spot.
			 * Repeat step 2.
			 * This technique keeps your table tidy at the expense of slightly slower deletions.
			*/
		}
		
		public override LoadFactor GetLoadFactor()
		{
			return new LoadFactor(entries,table.Length);
		}
		
		#endregion
		
		#region PRIVATE METHODS
		
		private object GetValueFromTable(object key, HashSlot[] targetTable, out bool found)
		{
			int capacity = targetTable.Length;
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, targetTable.Length);
			
			for(int i = 0; i < capacity; i++)
			{
				if(targetTable[index] == null)
				{
					found = false;
					return null;
				}
				else if(targetTable[index].Key == key)
				{
					found = true;
					return targetTable[index].Value;
				}
				else
				{
					index = (index + 1) % targetTable.Length;
				}
			}
			
			found = false;
			return null;
		}
		
		private void SetValueInTable(object key, object value, HashSlot[] targetTable)
		{
			int capacity = targetTable.Length;
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, targetTable.Length);
			
			bool success = false;
			
			for(int i = 0; i < capacity; i++)
			{
				if(targetTable[index] == null)
				{
					targetTable[index] = new HashSlot(key, value);
					success = true;
					break;
				}
				else if(targetTable[index].Key == key)
				{
					targetTable[index] = new HashSlot(key, value);
					success = true;
					break;
				}
				else 
				{
					index = (index + 1) % capacity;
				}
			}
			
			if(!success)
			{
				throw new Exception("INTERNAL_ERROR: SetValueInTable() :: Cannot find valid index, loop ends.");
			}
			
		}
		
		private int GetIndexFromHash(int hash, int tableLength)
		{
			return (int)(hash & 0x7fffffff) % tableLength;
		}
		
		private void ResizeTableIfNeed()
		{
			int capacity = this.table.Length;
			
			if(GetLoadFactor().Factor > MAX_LOAD_FACTOR)
			{
				int newSize = this.table.Length * 2;
				HashSlot[] newTable = new HashSlot[newSize];
				
				RehashTable(this.table, newTable);
				
				this.table = newTable;
			}
		
		}
		
		private void RehashTable(HashSlot[] currentTable, HashSlot[] newTable)
		{	
			for(int i = 0 ; i < currentTable.Length; i++)
			{
				if(currentTable[i] != null)
				{
					SetValueInTable(currentTable[i].Key, currentTable[i].Value, newTable);
				}
			}
		}
		
		#endregion
	}
}