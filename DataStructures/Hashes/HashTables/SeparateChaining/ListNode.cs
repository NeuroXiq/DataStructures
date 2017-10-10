namespace DataStructures.HashTables
{
	public class ListNode
	{
		public HashSlot Slot {get; private set;}
		public ListNode NextNode {get; private set;}
		
		
		public ListNode() : this(null)
		{
			
		}
		
		public ListNode(HashSlot slot) : this(slot, null)
		{
			
		}
		
		public ListNode(HashSlot slot, ListNode next)
		{
			this.NextNode = next;
			this.Slot = slot;
		}
		
		public void SetNextNode(ListNode nextNode)
		{
			this.NextNode = nextNode;
		}
		
		public void SetHashSlot(HashSlot slot)
		{
			this.Slot = slot;
		}
	}
}