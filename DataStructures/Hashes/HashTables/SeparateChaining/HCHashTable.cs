namespace DataStructures.HashTables
{
	/*  CLASS NOT IMPLEMENTED!  */
	
	/*
	*
	*
	*
	* Implementation of separate chaining with list head cells hash table
	*
	*/
	
	public class HCHashTable : HashTableBase
	{
		private const double MAX_LOAD_FACTOR = (double)0.65;
		
		private HeadNode[] table;
		private int entries;
		
		public HCHashTable() : this(new HashProviderBase())
		{
			
		}
		
		public HCHashTable(HashProviderBase hashProvider) : base(hashProvider)
		{
			this.entries = 0;
			this.table = new HeadNode[16];
		}

		#region ABSTRACT METHODS IMPLEMENTATION
		
		public override object GetValue(object key)
		{
			bool found;
			object value = GetValueFromTable(key, this.table, out found);
			
			if(!found)
			{
				// value not found in table,
				// returs some default value
				
				value = null;
			}
			
			return value;
		}
		
		public override void SetValue(object key, object value)
		{
			//ExtendArrayIfNeed();
			
			bool newOccupied = SetValueInTable(key, value, this.table);
			
			if(newOccupied)
			{
				this.entries++;
			}
			
			
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
		
		private bool SetValueInTable(object key, object value, HeadNode[] table)
		{
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, table.Length);
			bool setNew = false;
			
			if(table[index] == null)
			{
				setNew = true;
			}
            return setNew;
		}
		
		private object GetValueFromTable(object key, HeadNode[] table, out bool found)
		{
			int hash = base.GetHash(key);
			int index = GetIndexFromHash(hash, table.Length);
			bool success = false;
			object value = null;
			
			if(table[index] != null)
			{
				ListNode node = table[index].Next;
				
				while(node != null)
				{
					if(node.Slot.Key == key)
					{
						value = node.Slot.Value;
						success = true;
						break;
					}
					else
					{
						node = node.NextNode;
					}
				}
			}
			
			found = success;
			
			return value;
			
		}
		
		private int GetIndexFromHash(int hash, int tableLength)
		{
			//int uhash = hash & 0x7fffffff;
			
			int uhash = 0;
			unchecked 
			{
				uhash = hash & 0x7fffffff;
			}
			
			return uhash % tableLength;
		}
		
		#endregion
		
	}
}