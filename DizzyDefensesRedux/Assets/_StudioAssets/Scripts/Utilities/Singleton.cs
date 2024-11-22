/*
 * Singleton.cs
 * 
 * Description:
 * - Basic utility class that holds most of the functionality of a singleton so we can just subclass this instead of remaking the functions every time
 * 
 * Author(s): 
 * - Dan
*/

using System;
using UnityEngine;

namespace RubberDucks.Utilities
{
	public class Singleton<T> : MonoBehaviour 
		where T : UnityEngine.Component // TODO Dan: Maybe subclass this one more time to have a UnitySingleton<> so the base Singleton<> can work with everything, not just Unity components
	{
		//--- Static and Constant Variables ---//
		protected static T m_Instance = default;

		//--- Properties ---//
		public static T Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = FindObjectOfType<T>(); // TODO Dan: Also destroy any other instances

					if (m_Instance == null)
					{
						Type singletonType = typeof(T);

						GameObject instanceObj = new GameObject($"GNERATED Singleton - {singletonType.Name}");
						m_Instance = instanceObj.AddComponent<T>();
					}
				}

				return m_Instance;
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
