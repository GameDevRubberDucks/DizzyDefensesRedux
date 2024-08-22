/*
 * Timer.cs
 * 
 * Description:
 * - Simple timer utility that needs to be updated manually and has callbacks for when it's done
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.Events;

using RubberDucks.Utilities.CustomInspectors;

namespace RubberDucks.Utilities.Timing
{
	public class Timer : MonoBehaviour
	{
		//--- Events ---//
		[System.Serializable]
		public class EventList
		{
			public UnityEvent OnFinished;
			public UnityEvent OnStoppedEarly;

			public UnityEvent<float, float> OnTimeLeftUpdated; // time in seconds, percentage elapsed
		}
		[Header("Events")]
		public EventList m_Events = default;

		//--- Properties ---//
		public float Duration { get => m_Duration; set => m_Duration = value; }
		public float TimeLeft 
		{
			get => m_TimeLeft;
			set
			{
				m_TimeLeft = value;
				m_Events.OnTimeLeftUpdated.Invoke(TimeLeft, PercentageElapsed);
			} 
		}
		public bool IsRunning { get => m_IsRunning; set => m_IsRunning = value; }
		public bool ShouldLoop { get => m_ShouldLoop; set => m_ShouldLoop = value; }
		public float PercentageElapsed => 1.0f - (TimeLeft / Duration);
		public float PercentageElapsedInverse => 1.0f - PercentageElapsed;
		public bool IsDone => m_TimeLeft <= 0.0f;

		//--- Public Variables ---// 

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[Header("Timing Settings")]
#pragma warning disable CS0414 // Says that the bool isn't used since it is only used in the conditional visibility attribute
		[SerializeField] private bool m_SetDurationFromOtherScript = false;
#pragma warning restore CS0414
		[SerializeField][ConditionalVisibility("m_SetDurationFromOtherScript", false)] private float m_Duration = 10.0f;

		[Header("Loop Settings")]
		[SerializeField] private bool m_ShouldLoop = false;

		private float m_TimeLeft = default;
		private bool m_IsRunning = false;
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		public void StartTimer()
		{
			TimeLeft = Duration;
			IsRunning = true;
		}

		public void StartTimer(float newDuration)
		{
			Duration = newDuration;
			StartTimer();
		}

		public void RestartTimer()
		{
			StartTimer();
		}
		
		public void OverrideRemainingTimePercentage(float timePercentage)
		{
			TimeLeft = Duration * timePercentage;
			
			ResumeTimer();
		}

		public void PauseTimer()
		{
			IsRunning = false;
		}

		public void ResumeTimer()
		{
			IsRunning = true;
		}

		public void UpdateTimer(float elapsedTime)
		{
			if (IsRunning)
			{
				TimeLeft -= elapsedTime;

				if (IsDone)
				{
					FinishTimer();
				}
			}
		}

		public void StopTimer()
		{
			PauseTimer();

			m_Events.OnStoppedEarly.Invoke();
		}

		public void FinishTimer(bool forceStopLooping = false)
		{
			IsRunning = false;

			m_Events.OnFinished.Invoke();

			if (ShouldLoop && !forceStopLooping)
			{
				StartTimer(); 
			}
		}
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
