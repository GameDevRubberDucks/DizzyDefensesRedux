/*
 * PauseManager.cs
 * 
 * Description:
 * - functionalities relating to pausing the game
 * 
 * Author(s): 
 * - Bo
*/

using UnityEngine;

using RubberDucks.Utilities;

namespace RubberDucks.Utilities.Timing
{
	public class PauseManager : Singleton<PauseManager>
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

		//--- Unity Methods ---//

		//--- Public Methods ---//
		public void TimeScalePause(bool isPause)
        {
            if (isPause)
            {
				Time.timeScale = 0.0f;
            }
            else
            {
				Time.timeScale = 1.0f;
            }
        }

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}