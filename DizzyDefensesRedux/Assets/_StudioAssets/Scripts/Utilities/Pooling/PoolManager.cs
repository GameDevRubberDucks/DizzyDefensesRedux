/*
 * PoolManager.cs
 * 
 * Description:
 * - A manager that handles multiple pools of different objects to avoid constant destroying and reinstantiating
 * 
 * Author(s): 
 * - Dan
*/

using System;
using System.Collections.Generic;

using UnityEngine;

namespace RubberDucks.Utilities.Pooling
{
	public class PoolManager : Singleton<PoolManager>
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[Header("Pool Controls")]
		[SerializeField] private Transform m_PoolParentRoot;
		[SerializeField] private List<PoolDescriptor> m_PoolDescriptors = new List<PoolDescriptor>();

		private Dictionary<Poolable, Pool> m_Pools = new Dictionary<Poolable, Pool>();

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public virtual void Initialize()
		{
			DestroyAllData();
			GeneratePools();
		}

		public virtual GameObject PoolInstantiate(Poolable type, string name = "", Vector3 position = default, Quaternion rotation = default, Transform parent = null)
		{
			// TODO Dan
			return null;
		}

		public virtual void PoolDestroy(GameObject objectToDestroy)
		{
			// TODO Dan
		}

		//--- Protected Methods ---//
		protected virtual void DestroyAllData()
		{
			foreach(var pool in m_Pools.Values)
			{
				pool.DestroyPool();
			}
		}

		protected virtual void GeneratePools()
		{
			foreach(var poolDescriptor in m_PoolDescriptors)
			{
				GameObject newPoolObj = new GameObject($"Pool - {poolDescriptor.m_PoolTypePrefab.gameObject.name}");
				newPoolObj.transform.parent = m_PoolParentRoot;

				Pool newPool = newPoolObj.AddComponent<Pool>();
				newPool.Initialize(poolDescriptor);

				m_Pools.Add(poolDescriptor.m_PoolTypePrefab, newPool);
			}
		}

		//--- Private Methods ---//
	}
}
