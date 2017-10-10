namespace DataStructures.HashTables
{
	public struct HeadNode
	{
		public ListNode Next {get; private set;}
		
		public HeadNode() : this(null)
		{
			
		}
		
		public HeadNode(ListNode next) 
		{
			this.Next = next;
		}
		
		public void SetNext(ListNode node)
		{
			this.Next = node;
		}
		
	}
}