/*
 * ForceKeepRotation.cs
 * 
 * Description:
 * - Keeps an object aligned vertically so it is always facing the right way
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.Events;

namespace RubberDucks.Utilities
{
	public class ForceKeepRotation : MonoBehaviour
	{
		//--- Events ---//
		[System.Serializable]
		public class EventList
		{
		}
		[Header("Events")]
		public EventList Events = default;

		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[SerializeField] private Transform m_AnchorToRotate = default;
		[SerializeField] private Vector3 m_ConstantUpVector = Vector3.up;

		//--- Unity Methods ---//

		//--- Public Methods ---//
		private void LateUpdate()
		{
			m_AnchorToRotate.up = m_ConstantUpVector;
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
