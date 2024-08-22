/*
 * AnimationStateCallbackReceiver.cs
 * 
 * Description:
 * - Recives OnStateEnter and OnStateExit events from an AnimationCallbackBroadcaster
 * - This *must* be on the same script as the animator component, or it will not work
 * - You can have multiple of these on the same object and differentiate it them via the broadcast channel tag
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.Events;

using RubberDucks.Utilities.CustomInspectors;

namespace RubberDucks.Utilities.Animations
{
	[RequireComponent(typeof(Animator))]
	public class AnimationStateCallbackReceiver : MonoBehaviour
	{
		//--- Events ---//
		[System.Serializable]
		public class EventList
		{
			public UnityEvent OnStateEntered;
			public UnityEvent OnStateExited;
		}
		[Header("Events")]
		public EventList m_Events;

		//--- Properties ---//
		public bool ReceiveAnyBroadcast => m_ReceiveAnyBroadcast;
		public string BroadcastChannelTag => m_BroadcastChannelTag;

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[SerializeField] private bool m_ReceiveAnyBroadcast = false;
		[ReadOnly("m_ReceiveAnyBroadcast", false)][SerializeField] private string m_BroadcastChannelTag = "ChannelTag";
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
