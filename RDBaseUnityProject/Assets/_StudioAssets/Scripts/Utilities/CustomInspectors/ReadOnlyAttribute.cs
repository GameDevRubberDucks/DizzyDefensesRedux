/*
 * ReadOnlyAttribute.cs
 * 
 * Description:
 * - An attribute that can be added to a property to make it read only
 * - Optionally, can be tied to a bool on the same serialized object to conditionally lock it
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

namespace RubberDucks.Utilities.CustomInspectors
{
	public class ReadOnlyAttribute : PropertyAttribute
	{
		//--- Properties ---//

		//--- Public Variables ---//
		public string m_ControlBoolName = default;
		public bool m_IsEditableWhenControlBoolIsTrue = false;
		
		//--- Protected Variables ---//
		
		//--- Private Variables ---//
		
		//--- Unity Methods ---//
		
		//--- Public Methods ---//
		public ReadOnlyAttribute()
		{
			m_ControlBoolName = null;
			m_IsEditableWhenControlBoolIsTrue = false;
		}

		public ReadOnlyAttribute(string controlBoolName, bool isEditableWhenControlBoolIsTrue = true)
		{
			m_ControlBoolName = controlBoolName;
			m_IsEditableWhenControlBoolIsTrue = isEditableWhenControlBoolIsTrue;
		}
		
		//--- Protected Methods ---//
		
		//--- Private Methods ---//
	}
}
