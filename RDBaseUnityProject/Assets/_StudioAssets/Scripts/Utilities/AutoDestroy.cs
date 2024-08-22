/*
 * AutoDestroy.cs
 * 
 * Description:
 * - Automatically destroy an object after X seconds
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities
{
	public class AutoDestroy : MonoBehaviour
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[SerializeField] private float m_Lifetime = 1.0f;

		//--- Unity Methods ---//
		private void Start()
		{
			Destroy(this.gameObject, m_Lifetime);
		}

		//--- Public Methods ---//

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
