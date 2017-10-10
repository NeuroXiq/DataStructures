namespace DataStructures.HashTables
{
	public struct LoadFactor
	{
		private int entries;
		private int buckets;
		
		public int Entries {get { return entries; }}
		public int Buckets {get { return buckets; }}
		public double Factor {get { return ((double)entries/buckets); } }
		
		
		public LoadFactor(int entries, int buckets)
		{
			this.entries = entries;
			this.buckets = buckets;
		}
	}
}