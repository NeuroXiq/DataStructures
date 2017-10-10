namespace DataStructures.Trees.BinaryTrees
{
	/*
	*
	* Implementation of AVL - self-balancing binary search tree.
	*
	*/
	
	///<summary>AVL tree - self balancing binary search tree.</summary>
	public class AVL
	{
		public BNode root;
		private int nodesCount;
		
		/// <summary>Creates new instanse of AVL</summary>
		public AVL()
		{
			this.root = null;
			this.nodesCount = 0;
		}
		
		
		#region PUBLIC METHODS
		
		///<summary>Insert new key into AVL tree</summary>
		public void Insert(int key)
		{
			if(this.root == null)
			{
				BNode newNode = new BNode(key);
				
				
				this.root = newNode;
				nodesCount = 1;
				
			}
			else
			{
				Insert(this.root, key);
			}
			
			nodesCount++;
		}
		
		#endregion
		
		#region PRIVATE METHODS
		
		private void Insert(BNode root, int key)
		{
			BNode node = BinaryInsert(root, key);
			//node = node.Parent;
			while(node != null)
			{
				if(GetBalanceFactor(node) > 1)
				{
					if(GetBalanceFactor(node) > 0)
					{
						node = RotateLeft(node,node.Right);
					}
					else
					{
						//BNode newRoot = RotateRight(node.Right, node.Right.Left);
						//RotateLeft(newRoot, node.Right);
						//node = newRoot;
					}
				}
				else if(GetBalanceFactor(node) < -1)
				{
					if(GetBalanceFactor(node.Left) < 0)
					{
						node = RotateRight(node, node.Left);
					}
					else
					{
						//BNode newRoot = RotateLeft(node.Left, node.Left.Right);
						//node = RotateRight(newRoot, node.Left);
					}
				}
				
				node = node.Parent;
			}
		}
		
		private BNode BinaryInsert(BNode root, int key)
		{
			BNode child = root;
			BNode parent = root;
			
			while(child != null)
			{
				parent = child;
				if(key < child.Key)
				{
					child = child.Left;
				}
				else child = child.Right;
			}
			
			BNode newNode = new BNode(key, parent);
			if(key < parent.Key)
			{
				parent.SetLeft(newNode);
			}
			else parent.SetRight(newNode);
			
			return newNode;
			
		}
		
		
		///returns new parent/root of rotated tree
		private BNode RotateLeft(BNode parent, BNode child)
		{
			BNode parentParentCopy = parent.Parent;
			
			parent.SetParent(child);
			parent.SetRight(child.Left);
			if(child.Left != null)
				child.Left.SetParent(parent);
			
			child.SetParent(parentParentCopy);
			child.SetLeft(parent);
			
			if(parentParentCopy != null)
			{
				if(parentParentCopy.Left == parent)
					parentParentCopy.SetLeft(child);
				else parentParentCopy.SetRight(child);
			}
			
			return child;
		}
		
		///returns new parent/root of rotated tree
		private BNode RotateRight(BNode parent, BNode child)
		{
			BNode parentParentCopy = parent.Parent;
			
			parent.SetParent(child);
			parent.SetLeft(child.Right);
			if(child.Right != null)
				child.Right.SetParent(parent);
			
			child.SetRight(parent);
			child.SetParent(parentParentCopy);
			
			if(parentParentCopy	 != null)
			{
				if(parentParentCopy.Left == parent)
					parentParentCopy.SetLeft(child);
				else parentParentCopy.SetRight(child);
			}
			
			return child;
		}
		
		/*private BNode GetPossibleParent(BNode node , int key)
		{
			// look for the parent if the node with 'key' would exist in tree
			
			if(node.Key == key)
				return node;
			BNode lastParent = node;
			
			bool search = true;
			while(search)
			{
				if(lastParent.Key == key)
				{
					search = false;
				}
				else if(key < lastParent.Key)
				{
					if(lastParent.Left != null)
					{
						lastParent = lastParent.Left;
					}
					else search = false;
				}
				else
				{
					if(lastParent.Right != null)
					{
						lastParent = lastParent.Right;
					}
					else search = false;
				}
			}
			
			return lastParent;
		}*/
		
		private BNode SearchNode(BNode node, int key)
		{
			if(node == null)
				return null;
			
			if(node.Key == key)
				return node;
			else if(node.Key < key)
				return SearchNode(node.Left, key);
			else return SearchNode(node.Right, key);
		}
		
		public int GetBalanceFactor(BNode node)
		{
			if(node == null)
				return 0;
			
			int leftH = GetHeight(node.Left) + 1;
			int rightH = GetHeight(node.Right) + 1;
			
				
			return rightH - leftH;
		}
		
		private int GetHeight(BNode node)
		{
			if(node == null)
				return -1;
			
			int l = 1 + GetHeight(node.Left);
			int r = 1 + GetHeight(node.Right);
			
			int result = l > r ? l : r;
			
			return result;
			
		}
		
		#endregion
		
	}
}