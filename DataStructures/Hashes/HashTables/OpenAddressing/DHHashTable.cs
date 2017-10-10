using System;

namespace DataStructures.HashTables
{
	public class DHHashTable : HashTableBase
	{
		private const double MAX_LOAD_FACTOR = (double)0.7;
		private const int INITIAL_ARRAY_SIZE = 16;
		
		private HashSlot[] table;
		private int entries;
		
		public DHHashTable() : this(new HashProviderBase())
		{
			
		}
		
		public DHHashTable(HashProviderBase hashBase) : base(hashBase)
		{
			this.table = new HashSlot[INITIAL_ARRAY_SIZE];
			this.entries = 0;
		}
		
		#region ABSTRACT METHOD IMPLEMENTATION
		
		
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
			bool insertedInNewRow = SetValueInTable(key, value, this.table);
			
			
			//if key exist in table, value of HashSlot will be replaced with 
			//new value - and there is no adding new HashSlot into the table
			
			if(insertedInNewRow)
			{
				entries++;
			}
			
		}
		
		public override void Remove(object key)
		{
			/* remove */
		}
		
		public override LoadFactor GetLoadFactor()
		{
			return new LoadFactor(this.entries, table.Length);
		}
		
		#endregion
		
		#region PRIVATE METHODS
		
		private object GetValueFromTable(object key, HashSlot[] table, out bool foundSuccess)
		{
			int hash = base.GetHash(key);
			int size = table.Length;
			int index;
			object value = null;
			bool success = false;
		
			for(int i = 0; i < size; i++)
			{
				index = GetNextDoubleHashIndex(hash, i, size);
				
				if(table[index] != null)
				{
					if(table[index].Key == key)
					{
						value = table[index].Value;
						success = true;
						break;
					}
				}
				else
				{
					success = false;
					break;
				}
			}
			
			if(success)
			{
				foundSuccess = true;
				return value;
			}
			else 
			{
				foundSuccess = false;
				return null;
			}
		}
		
		private bool SetValueInTable(object key, object value, HashSlot[] table)
		{
			
			int hash = base.GetHash(key);
			int size = table.Length;
			int index;
			bool success = false;
			bool insertedNew = false;
			
			for(int i = 0; i < size; i++)
			{
				index = GetNextDoubleHashIndex(hash, i, size);
				
				if(table[index] == null)
				{
					table[index] = new HashSlot(key, value);
					success = true;
					insertedNew = true;
					
					break;
				}
				else if(table[index].Key == key)
				{
					table[index] = new HashSlot(key,value);
					insertedNew = false;
					success = true;
					
					break;
				}
			}
			
			if(!success)
			{
				throw new Exception("__INTERNAL_ERROR > DHHashTable > SetValueInTable :: value not inserted, loops end");
			}
			
			return insertedNew;
			
		}
		
		private int GetNextDoubleHashIndex(int baseHash, int iterator, int tableSize)
		{
			int newHash = DoubleHashFunction(baseHash);
			int newIndex = -1;
			
			unchecked
			{
				newIndex = ((iterator * newHash) + baseHash);
			}
			
			if(newIndex < 0)
				newIndex = -newIndex;
			
			newIndex = newIndex % tableSize;
			
			return newIndex;
		}
		
		private void ExtendTableIfNeed()
		{
			double factor = GetLoadFactor().Factor;
			
			if(factor > MAX_LOAD_FACTOR)
			{
				int newSize = this.table.Length * 2;
				HashSlot[] newTable = new HashSlot[newSize];
				
				RehashTable(this.table, newTable);
				
				this.table = newTable;
			}
		}
		
		private void RehashTable(HashSlot[] source, HashSlot[] destination)
		{
			int size = source.Length;
			
			for(int i = 0; i < size; i++)
			{
				if(source[i] != null)
				{
					SetValueInTable(source[i].Key, source[i].Value, destination);
				}
			}
		}
		
		private int DoubleHashFunction(int input)
		{
			/* Implementation of imaginated hash function */
			/* Do NOT use this in real project - its just example */
			
			uint newHash =  (uint)input;
			uint c1 = 0x90abcdef;
			uint c2 = 0x12345678;
		
			unchecked
			{
			
				newHash = ( (newHash & 0xffff0000) >> 16) |
						  ( (newHash & 0x0000ffff) << 16);
						
				newHash = (~newHash);
				newHash ^= c1;
				newHash += c2;
				
				newHash = ( ((newHash ^ 0x12345678) + 0xff000000)) ^
						  ( ((newHash ^ 0x87654321) + 0x00ff0000)) ^
						  ( ((newHash ^ 0xabcdef10) + 0x0000ff00)) ^
						  ( ((newHash ^ 0x10987654) + 0x000000ff));  
					  
			}
			
			
			return (int)newHash;
		}
		
		#endregion
	}
}