/*
 * ObjectShaker.cs
 * 
 * Description:
 * - Simple script that can be attached to any object and called to shake said object
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities
{
	public class ObjectShaker : MonoBehaviour
	{
		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[SerializeField] private Transform m_ShakeAnchor = default;
		[SerializeField] private float m_ShakeStrength = 5.0f;
		[SerializeField] private bool m_IncludeYAxis = false;

		private bool m_IsShaking = false;
		private Vector3 m_DefaultShakeAnchorPosition = default;

		//--- Unity Methods ---//
		private void Start()
		{
			m_DefaultShakeAnchorPosition = m_ShakeAnchor.position;
		}

		private void Update()
		{
			if (m_IsShaking)
			{
				PerformShake();
			}
		}

		//--- Public Methods ---//
		public void Shake()
		{
			Shake(m_ShakeStrength, m_IncludeYAxis);
		}

		public void Shake(float newStrength, bool m_IncludeYAxis = false)
		{
			m_IsShaking = true;
			m_ShakeStrength = newStrength;
		}

		public void StopShaking()
		{
			m_IsShaking = false;
			m_ShakeAnchor.position = m_DefaultShakeAnchorPosition;
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
		private void PerformShake()
		{
			Vector3 randomDirection = Random.insideUnitSphere;
			if (!m_IncludeYAxis)
			{
				randomDirection.y = 0.0f;
				randomDirection.Normalize();
			}
			randomDirection *= (m_ShakeStrength * Time.deltaTime); // Time.deltaTime doesn't really do anything here I think? It just lowers the shake amount

			Vector3 newPosition = m_DefaultShakeAnchorPosition + randomDirection;

			m_ShakeAnchor.position = newPosition;
		}
	}
}
