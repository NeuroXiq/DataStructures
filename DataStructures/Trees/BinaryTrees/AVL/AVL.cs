using System;

namespace DataStructures.Trees.BinaryTrees
{
    //TODO Implement TwoChildsDelete method (search for successor etc.).


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
                BNode node = BinaryInsert(root, key);
                RebalanceTree(node);
			}
			
			nodesCount++;
		}

        public void Delete(int key)
        {
            if (root != null)
            {
                DeleteKey(root, key);
            }
        }

        public bool ContainsKey(int key)
        {
            if (root == null)
                return false;

            BNode nodeWithKey = FindNode(this.root, key);

            if (nodeWithKey == null)
                return false;
            else return true;
        }

        #endregion

        #region PRIVATE METHODS

        private void DeleteKey(BNode root, int key)
        {
            BNode nodeToDelete = FindNode(root, key);
            if (nodeToDelete != null)
            {
                if (nodeToDelete.Left == null && nodeToDelete.Right == null)
                {
                    NoChildsDelete(nodeToDelete);
                }
                else if (nodeToDelete.Left != null && nodeToDelete.Right != null)
                {
                    TwoChildsDelete(nodeToDelete);
                }
                else OneChildDelete(nodeToDelete);
            }
        }

        private void OneChildDelete(BNode nodeToDelete)
        {
            BNode parent = nodeToDelete.Parent;
            BNode switchNode = null;
            
            if (nodeToDelete.Left != null)
                switchNode = nodeToDelete.Left;
            else switchNode = nodeToDelete.Right;

            if (parent == null)
            {
                //nodeToDelete is root node
                //replace it with next child node 
                RemoveNodeReferences(nodeToDelete);
                this.root = switchNode;
                switchNode.Parent = null;
            }
            else
            {
                if (parent.Left == nodeToDelete)
                {
                    AssignLeftChild(parent, switchNode);
                }
                else
                {
                    AssignRightChild(parent, switchNode);
                }
                
                RemoveNodeReferences(nodeToDelete);
                RebalanceTree(switchNode);
            }
        }

        ///<summary>
        ///Assigns child as right child of parent and updates all references
        ///in parent and child instance.
        ///</summary>
        ///<returns>Returns unmodified right subtree from parent node before assignment</returns>
        private BNode AssignRightChild(BNode parent, BNode child)
        {
            BNode originalRightChild = parent.Right;

            if (child != null)
            {
                child.Parent = parent;
            }
            parent.Right = child;


            return originalRightChild;
        }

        ///<summary>
        ///Assigns child as left child of parent and updates all needed references
        ///in parent and child instance.
        ///</summary>
        ///<returns>Returns not modified left subtree from parent before assignment</returns>
        private BNode AssignLeftChild(BNode parent, BNode child)
        {
            BNode originalLeftChild = parent.Left;

            if (child != null)
            {
                child.Parent = parent;
            }
            parent.Left = child;


            return originalLeftChild;
        }

        private void TwoChildsDelete(BNode nodeToDelete)
        {

        }

        private void NoChildsDelete(BNode nodeToDelete)
        {
            BNode parent = nodeToDelete.Parent;

            RemoveNodeReferences(nodeToDelete);
            if (parent == null)
            {
                //root was removed
                this.root = null;
            }
            else
            {
                UpdateHeight(parent);
                RebalanceTree(parent);
            }
        }

        ///<summary>
        ///Removes all references from 'node' to its childs and parent and
        ///from parent and childs to the 'node'.
        ///</summary>
        private void RemoveNodeReferences(BNode node)
        {
            if (node != null)
            {
                if (node.Parent != null)
                {
                    if (node.Parent.Left == node)
                        node.Parent.Left = null;
                    else if(node.Parent.Right == node)
                        node.Parent.Right = null;
                }

                if (node.Left != null)
                    if (node.Left.Parent == node)
                        node.Left.Parent = null;

                if (node.Right != null)
                    if(node.Right.Parent == node)
                        node.Right.Parent = null;

                node.Left = 
                node.Right = 
                node.Parent = null;

                //if node for some reason will not delete
                //this assigns can help in debugging.
                node.Height = int.MinValue;
                node.Key = int.MinValue;
            }
        }

        private BNode FindNode(BNode rootNode, int key)
        {
            if (rootNode == null)
                return null;
            else if (rootNode.Key == key)
                return rootNode;
            else
            {
                BNode nextNode = key < rootNode.Key ? rootNode.Left : rootNode.Right;
                return FindNode(nextNode, key);
            }
        }

        private void RebalanceTree(BNode start)
        {
            // 1. Rotate left & right updates height of node and its childs
            // 2. 'start' node contains actual height and does not need update

            BNode node = start;
            BNode newGlobalRoot = node;

            while (node != null)
            {
                UpdateHeight(node);
                if (GetBalanceFactor(node) > 1)
                {
                    if (GetBalanceFactor(node.Right) > 0)
                    {
                        node = RotateLeft(node, node.Right);
                    }
                    else
                    {
                        BNode newRoot = RotateRight(node.Right, node.Right.Left);
                        node = RotateLeft(node, newRoot);
                    }
                }
                else if (GetBalanceFactor(node) < -1)
                {
                    if (GetBalanceFactor(node.Left) < 0)
                    {
                        node = RotateRight(node, node.Left);
                    }
                    else
                    {
                        BNode newRoot = RotateLeft(node.Left, node.Left.Right);
                        node = RotateRight(node, newRoot);
                    }
                }
                // save last node (next parent assign can be null but we must set new root node)
                newGlobalRoot = node;
                //we must update current height in trail from root to parent of new inserted node.
                node = node.Parent;
            }

            this.root = newGlobalRoot;
        }

        /*private void Insert(BNode root, int key)
        {
            //
            //Rotate left/right updates height of node and childs
            //
            //

            BNode node = BinaryInsert(root, key);
            BNode newGlobalRoot = node;

            node = node.Parent;

            while (node != null)
            {
                UpdateHeight(node);
                if (GetBalanceFactor(node) > 1)
                {
                    if (GetBalanceFactor(node.Right) > 0)
                    {
                        node = RotateLeft(node, node.Right);
                    }
                    else
                    {
                        BNode newRoot = RotateRight(node.Right, node.Right.Left);
                        node = RotateLeft(newRoot, node.Right);
                    }
                }
                else if (GetBalanceFactor(node) < -1)
                {
                    if (GetBalanceFactor(node.Left) < 0)
                    {
                        node = RotateRight(node, node.Left);
                    }
                    else
                    {
                        BNode newRoot = RotateLeft(node.Left, node.Left.Right);
                        node = RotateRight(newRoot, node.Left);
                    }
                }
                // save last node (next parent assign can be null but we must set new root node)
                newGlobalRoot = node;
                //we must update current height in trail from root to parent of new inserted node.
                node = node.Parent;
            }
            
            this.root = newGlobalRoot;
        }
*/

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
			
			BNode newNode = new BNode(key,0, parent);
			if(key < parent.Key)
			{
				parent.SetLeft(newNode);
			}
			else parent.SetRight(newNode);
			
			return newNode;
			
		}
		
		///<summary>
        ///</summary>
        ///<returns>New root node after rotation</returns>
		private BNode RotateLeft(BNode parent, BNode child)
		{
			BNode parentParentCopy = parent.Parent;
			
			parent.SetParent(child);
			parent.SetRight(child.Left);
			if(child.Left != null)
				child.Left.SetParent(parent);
			
			child.SetParent(parentParentCopy);
			child.SetLeft(parent);

            if (parentParentCopy != null)
            {
                if (parentParentCopy.Left == parent)
                    parentParentCopy.SetLeft(child);
                else parentParentCopy.SetRight(child);
            }

            UpdateHeight(parent); // parent is now child thats why 'childs' height must be updated first
            UpdateHeight(child);
            

			return child;
		}

        ///<summary>
        ///</summary>
        ///<returns>New root node after rotation</returns>
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

            UpdateHeight(parent); // parent now is child that's why parent height must be updated first
            UpdateHeight(child);

            return child;
		}

        private void UpdateHeight(BNode node)
        {
            if (node != null)
            {
                node.Height = MaxHeight(node.Left, node.Right) + 1;
            }
        }

        ///<summary>Find max height of specified nodes</summary>
        ///<returns>Value greater or equal</returns>
        private int MaxHeight(BNode n1, BNode n2)
        {
            int h1 = n1 != null ? n1.Height : -1;
            int h2 = n2 != null ? n2.Height : -1;

            int result = h1 > h2 ? h1 : h2;

            return result;
        }

        private int GetBalanceFactor(BNode node)
		{
			if(node == null)
				return 0;

            int leftH = node.Left != null ? node.Left.Height + 1 : 0;
            int rightH = node.Right != null ? node.Right.Height + 1: 0;
			
			return rightH - leftH;
		}
		
		#endregion
		
	}
}