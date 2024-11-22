/*
 * AnimationStateCallbackBroadcaster.cs
 * 
 * Description:
 * - State behaviour that can be added to mecanim states to broadcast out events when the state is entered or exited
 * - Connects to AnimationCallbackReceiver's that *must* be on the same object as the Animator that this is attached to
 * - Can differentiate between multiple AnimationCallbackReceiver's on the the same object by using the broadcast channel tag
 * 
 * Author(s): 
 * - Dan
*/

using System.Collections.Generic;

using UnityEngine;

namespace RubberDucks.Utilities.Animations
{
	public class AnimationStateCallbackBroadcaster : StateMachineBehaviour
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[SerializeField] private string m_BroadcastChannelTag = "ChannelTag";

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateEnter(animator, stateInfo, layerIndex);

			List<AnimationStateCallbackReceiver> receiversToBroadcastTo = GetRelevantReceivers(animator);
			if (receiversToBroadcastTo != null)
			{
				foreach(var receiver in receiversToBroadcastTo)
				{
					receiver.m_Events.OnStateEntered.Invoke();
				}
			}
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.OnStateExit(animator, stateInfo, layerIndex);

			List<AnimationStateCallbackReceiver> receiversToBroadcastTo = GetRelevantReceivers(animator);
			if (receiversToBroadcastTo != null)
			{
				foreach (var receiver in receiversToBroadcastTo)
				{
					receiver.m_Events.OnStateExited.Invoke();
				}
			}
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
		private List<AnimationStateCallbackReceiver> GetRelevantReceivers(Animator animator)
		{
			// Look for receiver components
			AnimationStateCallbackReceiver[] receiverComponents = animator.GetComponents<AnimationStateCallbackReceiver>();
			if (receiverComponents.Length == 0)
			{
				Debug.LogWarning($"No AnimationCallbackReceiver component(s) found on object '{animator.gameObject.name}' with animator '{animator.name}'. Receivers must be attached to the same object as the animator for the broadcast to work!");
				return null;
			}

			// Check if any of the receivers that were found match the broadcast tag OR work with any broadcast channel
			List<AnimationStateCallbackReceiver> receiversToMessage = new List<AnimationStateCallbackReceiver>();
			foreach(AnimationStateCallbackReceiver receiver in receiverComponents)
			{
				if (receiver.ReceiveAnyBroadcast || receiver.BroadcastChannelTag == m_BroadcastChannelTag)
				{
					receiversToMessage.Add(receiver);
				}
			}

			if (receiversToMessage.Count == 0)
			{
				Debug.LogWarning($"AnimationCallbackReceiver component(s) were found on object '{animator.gameObject.name}' with animator '{animator.name}'. However, none of them had the requested broadcast tag '{m_BroadcastChannelTag}' or were open to any broadcast!");
				return null;
			}

			return receiversToMessage;
		}
	}
}
