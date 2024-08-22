/*
 * Pool.cs
 * 
 * Description:
 * - An individual pool. The PoolManager handles a list of these
 * 
 * Author(s): 
 * - Dan
*/

using System;
using System.Collections.Generic;

using UnityEngine;

namespace RubberDucks.Utilities.Pooling
{
	[Serializable]
	public class PoolDescriptor
	{
		public int m_InitialCount = 100;
		public Poolable m_PoolTypePrefab = default;
	}

	public class Pool : MonoBehaviour
	{
		//--- Properties ---//
		public Transform PoolRoot { get => m_PoolRoot; }
		public PoolDescriptor Descriptor { get => m_Descriptor; }
		public int TotalCloneCount { get => Clones.Count; }
		public int ActiveCloneCount { get => throw new System.NotImplementedException(); }
		public int InactiveCloneCount { get => TotalCloneCount - ActiveCloneCount; }
		public List<GameObject> Clones { get => m_Clones; }

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		private PoolDescriptor m_Descriptor = default;
		private Transform m_PoolRoot = default;
		private List<GameObject> m_Clones = new List<GameObject>();

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public void Initialize(PoolDescriptor descriptor)
		{
			m_Descriptor = descriptor;
			m_PoolRoot = this.transform;
			m_Clones.Capacity = descriptor.m_InitialCount;

			for (int i = 0; i < m_Descriptor.m_InitialCount; ++i)
			{
				GameObject newClone = Instantiate(m_Descriptor.m_PoolTypePrefab.gameObject, m_PoolRoot);
				m_Clones.Add(newClone);
			}
		}

		public void DestroyPool()
		{
			foreach (var clone in m_Clones)
			{
				Destroy(clone);
			}

			Destroy(m_PoolRoot.gameObject);
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}