/*
 * AutomaticTimer.cs
 * 
 * Description:
 * - A timer that handles itself automatically, using Unity's API to update the time
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using RubberDucks.Utilities;

namespace RubberDucks.Utilities.Timing
{
	public class AutomaticTimer : Timer
	{
		//--- Properties ---//

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		[Header("Automatic Controls")]
		[SerializeField] private UnityLifeCycle.InitialStages m_AutomaticStartStage = UnityLifeCycle.InitialStages.None;
		[SerializeField] private UnityLifeCycle.EveryFrameStages m_AutomaticUpdateStage = UnityLifeCycle.EveryFrameStages.Update;

		//--- Unity Methods ---//
		private void Awake()
		{
			if (m_AutomaticStartStage == UnityLifeCycle.InitialStages.Awake)
			{
				StartTimer();
			}
		}

		private void Start()
		{
			if (m_AutomaticStartStage == UnityLifeCycle.InitialStages.Start)
			{
				StartTimer();
			}
		}

		private void Update()
		{
			if (m_AutomaticUpdateStage == UnityLifeCycle.EveryFrameStages.Update)
			{
				UpdateTimer(Time.deltaTime);
			}
		}

		private void LateUpdate()
		{
			if (m_AutomaticUpdateStage == UnityLifeCycle.EveryFrameStages.LateUpdate)
			{
				UpdateTimer(Time.deltaTime);
			}
		}

		private void FixedUpdate()
		{
			if (m_AutomaticUpdateStage == UnityLifeCycle.EveryFrameStages.FixedUpdate)
			{
				UpdateTimer(Time.fixedDeltaTime);
			}
		}

		//--- Public Methods ---//

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
