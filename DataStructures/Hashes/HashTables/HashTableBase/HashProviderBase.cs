using System;

namespace DataStructures.HashTables
{
	public class HashProviderBase
	{
		public HashProviderBase()
		{
			
		}
		
		
		/* This is a default hash function */
		public virtual int GetHash(object key)
		{
			return key.GetHashCode();
		}
	}
}