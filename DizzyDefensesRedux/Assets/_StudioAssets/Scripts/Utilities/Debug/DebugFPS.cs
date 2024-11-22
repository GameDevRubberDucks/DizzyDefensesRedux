/*
 * DebugFPS.cs
 * 
 * Description:
 * - Simple script that shows an FPS counter in the corner of the screen
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.Events;

using System.Collections.Generic;

namespace RubberDucks.Utilities.RDDebug
{
	public class DebugFPS : MonoBehaviour
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
		[SerializeField] private int m_FPSRollingAverageLength = 30;

		private List<int> m_RecordedFPSList = new List<int>();

		//--- Unity Methods ---//
#if DEBUG_FPS
		private void OnGUI()
		{
			float deltaTime = Time.deltaTime;
			float fps = 1.0f / deltaTime;
			int fpsInt = (int)fps;

			m_RecordedFPSList.Add(fpsInt);
			int rollingFPSAvg = CalcFPSRollingAverage();

			Rect fpsRect = new Rect(Vector2.zero, new Vector2(200.0f, 50.0f));
			GUI.Box(fpsRect, $"FPS = {fpsInt.ToString("D3")} | DT = {deltaTime.ToString("F5")} \n AvgFPS = {rollingFPSAvg.ToString("D3")}");

			int width = Screen.width;
			int height = Screen.height;

			Rect resolutionRect = new Rect(new Vector2(0.0f, 50.0f), new Vector2(200.0f, 25.0f));
			GUI.Box(resolutionRect, $"Res = {width.ToString()} x {height.ToString()}");
		}
#endif

		//--- Public Methods ---//

		//--- Protected Methods ---//

		//--- Private Methods ---//
		private int CalcFPSRollingAverage()
		{
			// If not enough entries, return 0
			if (m_RecordedFPSList.Count < m_FPSRollingAverageLength)
			{
				return 0;
			}

			// If too many entries, delete the oldest ones
			while (m_RecordedFPSList.Count > m_FPSRollingAverageLength)
			{
				m_RecordedFPSList.RemoveAt(0);
			}

			// Calculate and return average
			int sum = 0;
			foreach(var fps in m_RecordedFPSList)
			{
				sum += fps;
			}

			return sum / m_RecordedFPSList.Count;
		}
	}
}
