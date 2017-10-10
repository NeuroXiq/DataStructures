namespace DataStructures.HashTables
{
	public class HashSlot // SHO'uLD BE INTERNAL !!!
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