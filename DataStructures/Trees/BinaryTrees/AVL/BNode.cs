namespace DataStructures.Trees.BinaryTrees
{
	///<summary>Provides binary node class used in AVL tree.</summary>
	///<seealso cref="BinaryTreesNs.AVL">AVL binary tree.</seealso>
	public class BNode
	{
		///<summary>Get or set left BNode child of current node. </summary>
		public BNode Left {get; set; }
		///<summary>Get or set right BNode child of current node.</summary>
		public BNode Right {get; set; }
		///<summary>Get or set parent node of current node.</summary>
		public BNode Parent {get; set; }
		///<summary>Get or set height of subtree</summary>
		public int Height { get; set;}
		///<summary>Get ot set current key of the node</summary>
		public int Key {get; set;}
		
		///<summary>Initialize new instance of BNode.</summary>
		///<param name="key">Key to be assign to the node.</param>
		public BNode(int key) : this(key,0, null,null,null)
		{
			
		}
		///<summary>Initialize new instance of BNode.</summary>		
		///<param name="key">Key to be assign to the node</param>
		///<param name="parent"> Parent of the <see cref="BNode"/></param>
		public BNode(int key, BNode parent) : this(key, 0, parent, null, null)
		{
			
		}

        public BNode(int key, int height, BNode parent) : this(key, height, parent, null, null)
        { }
		///<summary>Initialize new insance of BNode</summary>
		///<param name="key">Key to be assign to the node</param>
		///<param name="parent">Parent of <see cref="BNode"/></param>
		///<param name="left">Left child of<see cref="BNode"/></param>
		///<param name="right">Right child of <see cref="BNode"/></param>
		public BNode(int key,int height, BNode parent, BNode left, BNode right)
		{
			this.Key = key;
			this.Parent = parent;
			this.Left = left;
			this.Right = right;
		}
		
		
		///<summary>Set left <see cref="BNode"/> child item</summary>
		public void SetLeft(BNode node)
		{
			this.Left = node;
		}
		///<summary>Set right <see cref="BNode"/> child item</summary>
		public void SetRight(BNode node)
		{
			this.Right = node;
		}
		///<summary>Set parent <see cref="BNode"/> child item</summary>
		public void SetParent(BNode node)
		{
			this.Parent = node;
		}
		///<summary>Set key value of current node</summary>
		public void SetKey(int key)
		{
			this.Key = key;
		}
		///<summary>Set balance factor for this node</summary>
		private void SetHeight(int h)
		{
			this.Height = h;
		}
		
		
	}
}