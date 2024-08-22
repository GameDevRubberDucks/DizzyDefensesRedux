/*
 * TreeNode.cs
 * 
 * Description:
 * - Generic tree node that can be used to construct a tree of nodes (no balancing / binary support yet)
 * 
 * Author(s): 
 * - Dan
*/

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace RubberDucks.Utilities.DataStructures.Tree
{
	public class TreeNode<T>
	{
		//--- Events ---//

		//--- Properties ---//
		public int ChildCount => m_Children.Count;
		public bool IsRootNode => m_Parent == null;
		public T Data => m_Data;
		public TreeNode<T> Parent => m_Parent;
		public List<TreeNode<T>> Children => m_Children;

		//--- Public Variables ---//

		//--- Protected Variables ---//
		protected TreeNode<T> m_Parent = null;
		protected List<TreeNode<T>> m_Children = new List<TreeNode<T>>();
		protected T m_Data = default;

		//--- Private Variables ---//

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public virtual void SetParent(TreeNode<T> newParent, bool removeFromParent = true)
		{
			if (m_Parent == newParent)
			{
				return;
			}

			if (m_Parent != null && removeFromParent)
			{
				m_Parent.RemoveChild(this);
			}

			m_Parent = newParent;

			if (newParent != null)
			{
				newParent.AddChild(this);
			}
		}

		public virtual void AddChild(TreeNode<T> newChild)
		{
			if (m_Children.Contains(newChild))
			{
				return;
			}

			m_Children.Add(newChild);

			newChild.SetParent(this);
		}

		public virtual void RemoveChild(TreeNode<T> child)
		{
			child.SetParent(null, false);
			m_Children.Remove(child);
		}

		public virtual List<TreeNode<T>> RemoveAllChildren()
		{
			List<TreeNode<T>> childrenCopy = new List<TreeNode<T>>(m_Children);

			while(m_Children.Count > 0)
			{
				RemoveChild(m_Children[0]);
			}

			return childrenCopy;
		}
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
