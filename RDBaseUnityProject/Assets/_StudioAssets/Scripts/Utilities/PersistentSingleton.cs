/*
 * PersistentSingleton.cs
 * 
 * Description:
 * - Singleton but also has the DontDestroyOnLoad properties so it exists across scenes
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;
using UnityEngine.Events;

namespace RubberDucks.Utilities
{
	public class PersistentSingleton<T> : Singleton<T>
		where T : UnityEngine.Component
	{
		//--- Properties ---//
		public static new T Instance
		{
			get
			{
				// Only register for persistence if the instance is null since that means we will create the very first one
				if (m_Instance == null)
				{
					// The instance will be created right here when we call it
					m_Instance = (T)Singleton<T>.Instance;
					DontDestroyOnLoad(m_Instance.gameObject);
				}

				// Destroy any others that are NOT the instance so we only have one
				foreach(var instance in FindObjectsOfType<PersistentSingleton<T>>())
				{
					if (instance != m_Instance)
					{
						Destroy(instance.gameObject);
					}
				}

				return (T)m_Instance;
			}
		}
		
		//--- Public Variables ---//
		
		//--- Protected Variables ---//
		
		//--- Private Variables ---//
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
