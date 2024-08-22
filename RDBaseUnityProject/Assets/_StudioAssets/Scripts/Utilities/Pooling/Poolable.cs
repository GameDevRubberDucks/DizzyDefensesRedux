/*
 * Poolable.cs
 * 
 * Description:
 * - An object that can be used within a Pool
 * - Prefabs that we want to be able to use within a Pool must have this component on them
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.Pooling
{
	public class Poolable : MonoBehaviour
	{
		//--- Properties ---//
		public PoolManager AttachedPool { get => m_AttachedPool; set => m_AttachedPool = value; }
		public bool ActiveInPool { get => m_ActiveInPool; set => m_ActiveInPool = value; }

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		private PoolManager m_AttachedPool = default;
		private bool m_ActiveInPool = false;

		//--- Unity Methods ---//

		//--- Public Methods ---//

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}