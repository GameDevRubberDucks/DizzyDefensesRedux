/*
 * DebugButtons.cs
 * 
 * Description:
 * - Base class for creating debug buttons on a game screen
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using RubberDucks.Utilities.Extensions;

namespace RubberDucks.Utilities.RDDebug
{
	public abstract class DebugButtons : MonoBehaviour
	{
		//--- Helper Structures ---//
		[System.Serializable]
		public enum AnchorLocation
		{
			TopLeft,
			BottomLeft
		}

		//--- Properties ---//
		public Vector2 AnchorLocationCoords => (m_GridAnchorLocation == AnchorLocation.TopLeft) ? Vector2.zero : ExtensionsForVector2.Vec2_0Y(Screen.height);

		//--- Public Variables ---//

		//--- Protected Variables ---//
		[Header("Controls")]
		[SerializeField] protected bool m_ShouldDrawButtons = true;
		[SerializeField] protected bool m_StartWithFullGrid = false;
		[SerializeField] protected int m_ButtonFontSize = 10;

		[Header("Grid Positioning")]
		[SerializeField] protected Vector2 m_GridCellSize = new Vector2(150.0f, 75.0f);
		[SerializeField] protected AnchorLocation m_GridAnchorLocation = AnchorLocation.BottomLeft;

		protected bool m_ShowFullGrid = false;
		protected GUIStyle m_CustomStyle = default;

		//--- Private Variables ---//

		//--- Unity Methods ---//
#if DEBUG_BUTTONS
		private void Awake()
		{
			m_ShowFullGrid = m_StartWithFullGrid;
		}

		private void OnGUI()
		{
			if (m_CustomStyle == null)
			{
				m_CustomStyle = new GUIStyle(GUI.skin.button);
			}
			m_CustomStyle.fontSize = m_ButtonFontSize;

			if (m_ShouldDrawButtons)
			{
				GenerateToggleButton();

				if (m_ShowFullGrid)
				{
					GenerateFullGrid();
				}
			}
		}
#endif

		//--- Public Methods ---//

		//--- Protected Methods ---//
		protected Rect GetGridRect(int xCoord, int yCoord)
		{
			Vector2 anchorCoord = AnchorLocationCoords;

			if (m_GridAnchorLocation == AnchorLocation.TopLeft)
			{
				Vector2 offset = new Vector2((float)xCoord * m_GridCellSize.x, (float)yCoord * m_GridCellSize.y);

				return new Rect(anchorCoord + offset, m_GridCellSize);
			}
			else if (m_GridAnchorLocation == AnchorLocation.BottomLeft)
			{
				float offsetX = (float)xCoord * m_GridCellSize.x;
				float offsetY = (float)-(yCoord + 1) * m_GridCellSize.y;
				Vector2 offset = new Vector2(offsetX, offsetY);

				return new Rect(anchorCoord + offset, m_GridCellSize);
			}

			Debug.LogError("Invalid RectAnchor in GetGridRect()! Returning an empty Rect object.");
			return new Rect();
		}

		protected virtual void GenerateToggleButton()
		{
			if (CreateButton(0, 0, "Toggle Debug Menu"))
			{
				m_ShowFullGrid = !m_ShowFullGrid;
			}
		}

		protected virtual void GenerateFullGrid()
		{
			// Stub
		}

		protected bool CreateButton(int gridX, int gridY, string text)
		{
			return GUI.Button(GetGridRect(gridX, gridY), text, m_CustomStyle);
		}

		//--- Private Methods ---//
	}
}
