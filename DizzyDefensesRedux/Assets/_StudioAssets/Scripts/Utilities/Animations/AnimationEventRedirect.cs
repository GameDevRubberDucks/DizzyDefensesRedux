/*
 * AnimationEventRedirect.cs
 * 
 * Description:
 * - AnimationEvents are a bit limited, so this allows for redirecting that event elsewhere
 * - In this game, AnimationEvents are placed on the BettingObject_RevealRed and BettingObject_RevealGreen animations
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using RubberDucks.Utilities.Events;

namespace RubberDucks.Utilities.Animations
{
	public class AnimationEventRedirect : MonoBehaviour
	{
		//--- Properties ---//
		public UnityEvent_String OnAnimationEvent => m_OnAnimationEvent;

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		// Can use the string parameter to help differentiate between different event messages if one object is hooked into multiple events
		[SerializeField] private UnityEvent_String m_OnAnimationEvent = new UnityEvent_String();

		//--- Unity Methods ---//
		private void OnDestroy()
		{
			if (m_OnAnimationEvent != null)
			{
				m_OnAnimationEvent.RemoveAllListeners();
			}
		}

		//--- Public Methods ---//
		public void OnAnimationEventReceived(string eventName)
		{
			if (m_OnAnimationEvent != null)
			{
				m_OnAnimationEvent.Invoke(eventName);
			}
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
