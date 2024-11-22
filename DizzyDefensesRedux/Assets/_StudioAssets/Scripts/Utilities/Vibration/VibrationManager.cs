/*
 * VibrationManager.cs
 * 
 * Description:
 * - Singleton manager for all vibration. Cross-platform as it uses the #if platforms to interface with other vibration scripts
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.Events;

using RubberDucks.Utilities;

namespace RubberDucks.Utilities.Vibration
{
	public class VibrationManager : Singleton<VibrationManager>
	{
		//--- Constants ---//
		private const string VIBRATION_ENABLED_SAVE_KEY = "SAVED_VIBRATION_ENABLED";

		//--- Events ---//
		[System.Serializable]
		public class EventList
		{
		}
		[Header("Events")]
		public EventList Events = default;

		//--- Properties ---//
		public bool VibrationEnabled
		{
			get => m_VibrationEnabled;
			set
			{
				m_VibrationEnabled = value;

#if UNITY_ANDROID
				if (!m_VibrationEnabled)
				{
					VibrateAndroid.Cancel();
				}
#endif
			}
		}

		//--- Public Variables ---//

		//--- Protected Variables ---//

		//--- Private Variables ---//
		private bool m_VibrationEnabled = true;

		//--- Unity Methods ---//
		private void Awake()
		{
			m_VibrationEnabled = PlayerPrefs.GetInt(VIBRATION_ENABLED_SAVE_KEY, 1) == 1;
		}

		private void OnApplicationQuit()
		{
			PlayerPrefs.SetInt(VIBRATION_ENABLED_SAVE_KEY, m_VibrationEnabled ? 1 : 0);
		}

		//--- Public Methods ---//
		public void Vibrate(long durationMs) // TODO Dan: Change this to seconds (and a float) for the next project. This project has all values already in ms
		{
			if (!m_VibrationEnabled)
			{
				return;
			}

#if UNITY_ANDROID
			VibrateAndroid.Vibrate(durationMs);
#endif
		}

		//--- Protected Methods ---//

		//--- Private Methods ---//
	}
}
