namespace DataStructures.HashTables
{
	public class HeadNode
	{
		public ListNode Next {get; private set;}
		
		public HeadNode()
		{
            this.Next = null;
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