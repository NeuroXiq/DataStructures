using System;
namespace DataStructures.Trees.BinaryTrees
{
	/*
	*
	* Implementation of binary searh tree (BST)
	*
	*/
	
	public class BST
	{
		public BinaryNode root;
		public int nodesCount;
		
		public BST()
		{
			this.root = null;
			this.nodesCount = 0;
		}
		
		public void Insert(int key, object value)
		{
			if(this.root == null)
			{
				root = new BinaryNode(key, value);
				root.SetParentNode(null);
			}
			else
			{
				Insert(key, value, this.root);
			}
			
			nodesCount++;
		}
		
		public void Delete(int key)
		{
			bool deleted = DeleteNode(key, this.root);
			if(deleted)
			{
				nodesCount--;
			}
		}
		
		public bool Lookup(int key)
		{
			BinaryNode node = SearchNode(key, this.root);
			
			if(node != null)
				return true;
			else return false;
		}
		
		public object GetValue(int key, out bool found)
		{
			BinaryNode node = SearchNode(key, this.root);
			if(node != null)
			{
				found = true;
				return node.Value;
			}
			else
			{
				found = false;
				return null;
			}
		}
		
		
		
		#region PRIVATE METHODS
		
		
		///<summary>
		/// Deletes node from specified tree.
		/// Returs true if node  exist in the tree and can be deleted,
		/// otherwise returns false.
		///<summary>
		private bool DeleteNode(int key, BinaryNode treeRoot)
		{
			//node with 'key' exist in tree ?
			BinaryNode toDelete = SearchNode(key, treeRoot);
			bool nodeRemoved = false;
			
			if(toDelete != null)
			{
				DeleteTreeNode(treeRoot, toDelete);
				nodeRemoved = true;
				
			}
			else nodeRemoved = false;
			
			return nodeRemoved;
		}
		
		private void DeleteTreeNode(BinaryNode tree, BinaryNode nodeToDelete)
		{
			if(nodeToDelete.Left == null && nodeToDelete.Right == null)
			{
				if(tree == nodeToDelete)
				{
					//nodeToDelete is a root without any childs
					this.root = null;
				}
				else
				{
					NoChildsDelete(nodeToDelete);
				}
			}
			else if(nodeToDelete.Left != null && nodeToDelete.Right != null)
			{
				/* Contains 2 childs, some operations must be done */
				
				TwoChildsDelete(nodeToDelete);
			}
			else
			{
				/* Node have one child. Need to repleace node with his child */
				
				OneChildDelete(nodeToDelete);
			}
		}
		
		private void TwoChildsDelete(BinaryNode node)
		{
			BinaryNode next = FindSuccessor(node);
			BinaryNode parent = next.Parent;
			
			if(parent == node)
			{
				parent.SetKey(next.Key);
				parent.SetValue(next.Value);
				parent.SetRightNode(next.Right);
				
				if(next.Right != null)
				{
					next.Right.SetParentNode(parent);
				}
			}
			else
			{
				parent.SetLeftNode(next.Right);
				if(next.Right != null)
				{
					next.Right.SetParentNode(parent);
				}
				
				node.SetKey(next.Key);
				node.SetValue(next.Value);
			}
		    
			next.SetParentNode(null);	
			next.SetLeftNode(null);
			next.SetRightNode(null);
		}
		
		private BinaryNode FindSuccessor(BinaryNode node)
		{
			BinaryNode successor = null;
			
			if(node.Right != null)
			{
				successor = node.Right;
				BinaryNode child = successor.Left;	
				while(child != null)
				{
					successor = child;
					child = child.Left;
				}
			}	
			
			
			return successor;
		}
		
		private BinaryNode FindPredecessor(BinaryNode node)
		{
			BinaryNode predecessor = null;
			
			if(node.Left != null)
			{
				BinaryNode child = node.Left;
				while(child != null)
				{
					predecessor = child;
					child = child.Right;
				}
			}
			
			return predecessor;
		}
		
		private void NoChildsDelete(BinaryNode nodeToDelete)
		{
			if(nodeToDelete.Parent != null)
			{
				if(nodeToDelete.Parent.Left == nodeToDelete)
				{
					nodeToDelete.Parent.SetLeftNode(null);
				}
				else nodeToDelete.Parent.SetRightNode(null);
				
				nodeToDelete.SetParentNode(null);
			}
		}
		
		private void OneChildDelete(BinaryNode nodeToDelete)
		{
			BinaryNode child = null;
			
			if(nodeToDelete.Left != null)
			{
				child = nodeToDelete.Left;
				
			}
			else
			{
				child = nodeToDelete.Right;
			}

			nodeToDelete.SetKey(child.Key);
			nodeToDelete.SetValue(child.Value);
			nodeToDelete.SetLeftNode(child.Left);
			nodeToDelete.SetRightNode(child.Right);
		
			child.SetParentNode(null);
			child.SetRightNode(null);
			child.SetLeftNode(null);
			
		}
		
		private BinaryNode SearchNode(int key, BinaryNode rootNode)
		{
			BinaryNode node = rootNode;
			
			while(node != null)
			{
				if(node.Key == key)
					return node;
				else if(node.Key < key)
					node = node.Right;
				else node = node.Left;
			}
			
			return null;
		}
		
		private void Insert(int key, object value, BinaryNode root)
		{
			
			BinaryNode parent = root;
			BinaryNode child = parent;
			
			while(child != null)
			{
				parent = child;
				
				if(child.Key == key)
				{
					throw new Exception("Tree can contains only unique values ");
				}
				
				if(child.Key < key)
				{
					child = child.Right;
				}
				else//else if(child.Key > key)
				{
					child = child.Left;
				}
				
			}
			
			BinaryNode newNode = new BinaryNode(key, value);
			if(parent.Key < key)
			{
				parent.SetRightNode(newNode);
			}
			else 
			{
				parent.SetLeftNode(newNode);
			}
			newNode.SetParentNode(parent);
			
		}
		
		#endregion
		
	}
}