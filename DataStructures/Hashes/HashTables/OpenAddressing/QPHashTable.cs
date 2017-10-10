using System;

namespace DataStructures.HashTables
{
	/*
	*
	* Implementation of Quadratic Probing Hash Table 
	*
	*/
	
	public class QPHashTable : HashTableBase
	{
		private const double MAX_LOAD_FACTOR = (double)0.6;
		
		private HashSlot[] table;
		private int entries;
		
		public QPHashTable() : this(new HashProviderBase())
		{
			
		}
		
		public QPHashTable(HashProviderBase hashProvider) : base(hashProvider)
		{
			this.table = new HashSlot[16];
			this.entries = 0;
		}
		
		#region ABSTRACT IMPLEMENTATION
		
		public override object GetValue(object key)
		{
			bool success = false;
			
			object result = GetValueFromTable(key, this.table, out success);
			
			if(success)
			{
				return result;
			}
			else
			{
				throw new InvalidOperationException("Cannot get value from non-existent key");
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
			
		}
		
		public override LoadFactor GetLoadFactor()
		{
			return new LoadFactor(this.entries, this.table.Length);
		}
		
		#endregion
		
		
		#region PRIVATE METHODS
		
		private object GetValueFromTable(object key, HashSlot[] table, out bool foundSuccess)
		{
			int size = table.Length;
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, size); //entry index
			
			bool success = false;
			object sValue = null;
			
			for(int i = 0 ; i < size; i++)
			{	
				index = GetNextQuadraticIndex(index, i, size);
				
				if(table[index] != null)
				{
					if(table[index].Key == key)
					{
						sValue = table[index].Value;
						success = true;
						break;
					}
				}
				else break;
			}
			
			if(success)
			{
				foundSuccess = true;
				return sValue;
			}
			else 
			{
				foundSuccess = false;
				return null;
			}
		}
		
		private int GetNextQuadraticIndex(int entryIndex, int iterator, int tableSize)
		{
			//TESTSETSE TEST
			//return (entryIndex + 1) % tableSize;
			
			double result = -1;
			int i = iterator;
			double c1 = 0.5;
			double c2 = 0.5;
			
			if(iterator == 0)
			{
				return entryIndex;
			}
			else
			{
				result = entryIndex + (c1 * i) + (c2 * (i * i));
				result = result % tableSize;
			}
			
			int resultIndex = (int)result;
			
			return resultIndex;
		}
		
		private void SetValueInTable(object key, object value, HashSlot[] table)
		{
			int size = table.Length;
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, size);
			
			bool success = false;
			
			for(int i = 0 ; i < size; i++)
			{
				index = GetNextQuadraticIndex(index, i, size);
				if(table[index] == null)
				{
					table[index] = new HashSlot(key, value);
					success = true;
					break;
				}
				else if(table[index].Key == key)
				{
					table[index] = new HashSlot(key, value);
					success = true;
					break;
				}
			}
			
			if(!success)
			{
				throw new Exception("_INTERNAL_EXCEPTION > QPHashTable > 'SetValueInTable()' :: loop ends, value not inserted");
			}
			
		}
		
		private int GetIndexFromHash(int hash, int tableSize)
		{
			int index = hash;
			if(index < 0) // index must be non-negative number
			{
				unchecked
				{
					index = (~index);
					++index;
				}
			}
			
			return index % tableSize;
		}
		
		private void ResizeTableIfNeed()
		{
			if(GetLoadFactor().Factor > MAX_LOAD_FACTOR)
			{
				int newSize = this.table.Length * 2;
				HashSlot[] newTable = new HashSlot[newSize];
				
				Rehash(this.table, newTable);
				
				this.table = newTable;
			}
		}
		
		private void Rehash(HashSlot[] source, HashSlot[] destination)
		{
			int size = source.Length;
			
			for(int i = 0 ; i < size; i++)
			{
				if(source[i] != null)
				{
					SetValueInTable(source[i].Key, source[i].Value, destination);
				}
			}
		}
		
		#endregion
		
	}
	
}