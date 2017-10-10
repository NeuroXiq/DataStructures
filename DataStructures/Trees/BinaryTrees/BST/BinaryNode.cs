namespace DataStructures.Trees.BinaryTrees
{
	public class BinaryNode
	{
		public BinaryNode Left  {get; private set;}
		public BinaryNode Right {get; private set;}
		public BinaryNode Parent {get; private set;}
		public object Value {get; private set;}
		public int Key {get; private set;}
		
		public BinaryNode(int key) : this(key, null)
		{
			
		}
		
		public BinaryNode(int key, object value)
		{
			this.Key = key;
			this.Value = value;
			this.Left = null;
			this.Right = null;
		}

		public void SetKey(int key)
		{
			this.Key = key;
		}
		
		public void SetLeftNode(BinaryNode left)
		{
			this.Left = left;
		}
		
		public void SetRightNode(BinaryNode right)
		{
			this.Right = right;
		}
		
		public void SetParentNode(BinaryNode parent)
		{
			this.Parent = parent;
		}
		
		public void SetValue(object value)
		{
			this.Value = value;
		}
		
	}
}