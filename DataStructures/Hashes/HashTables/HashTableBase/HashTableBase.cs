using System;

namespace DataStructures.HashTables
{	
	public abstract class HashTableBase
	{
		private HashProviderBase hashProvider;
		
		protected const int BASE_TABLE_CAPACITY = 16;
		
		public HashTableBase(HashProviderBase hashProvider)
		{
			this.hashProvider = hashProvider;
		}
		
		public HashTableBase() : this(new HashProviderBase())
		{
			
		}
		
		public object this[object key]
		{
			get
			{
				return GetValue(key);
			}
			set
			{
				SetValue(key, value);
			}
		}
		
		public abstract object GetValue(object key);
		public abstract void SetValue(object key, object value);
		public abstract void Remove(object key);
		public abstract LoadFactor GetLoadFactor();
		
		protected int GetHash(object key)
		{
			return hashProvider.GetHash(key);
		}
	}
}