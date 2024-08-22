/*
 * LinearMover.cs
 * 
 * Description:
 * - Simple script that uses DoTween to move to a destination at a certain speed
 * - Automatically calculates the duration of the Tween from the speed and destination
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using DG.Tweening;

namespace RubberDucks.Utilities
{
	public class LinearMover : MonoBehaviour
	{
		//--- Properties ---//

		//--- Public Variables ---//
		public Transform rollAnchor;

		//--- Protected Variables ---//

		//--- Private Variables ---//
		private Tween m_MovementTween = default;
		private Sequence m_RotationSequence = default; // TODO: This *definitely* should not be in this script, move it out to its own script later
		private bool m_UseSteppedAnimations = true;

		//--- Unity Methods ---//
		private void OnDestroy()
		{
			m_MovementTween.Kill();
			m_RotationSequence.Kill();
		}

		//--- Public Methods ---//
		public void MoveTo(Vector3 destination, float movementSpeed, TweenCallback OnComplete = null)
		{
			m_MovementTween.Kill();

			Vector3 path = destination - transform.position;
			float distance = path.magnitude;
			float duration = distance / movementSpeed;

			m_MovementTween = transform.DOMove(destination, duration, m_UseSteppedAnimations);
			m_MovementTween.onComplete = OnComplete;

			if (m_UseSteppedAnimations)
			{
				m_RotationSequence = DOTween.Sequence();
				for (int i = 0; i < 10; ++i)
				{
					m_RotationSequence.Append(rollAnchor.DORotate(new Vector3(36.0f, 0.0f, 0.0f), 0.0f, RotateMode.WorldAxisAdd));
					m_RotationSequence.AppendInterval(0.1f);
				}
				m_RotationSequence.Play();
			}
			else
			{
				m_RotationSequence = DOTween.Sequence();
				m_RotationSequence.Append(rollAnchor.DORotate(new Vector3(360.0f, 0.0f, 0.0f), 1.0f, RotateMode.WorldAxisAdd));
			}
		}
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
