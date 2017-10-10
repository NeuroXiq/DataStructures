namespace DataStructures.HashTables
{
	internal class HashSlot
	{
		public object key;
		public object value;
		
		public object Key { get { return key; } }
		public object Value { get { return value; } }
		
		public HashSlot(object key, object value)
		{
			this.key = key;
			this.value = value;
		}
	}
}