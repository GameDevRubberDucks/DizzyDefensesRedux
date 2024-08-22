/*
 * GridCell2D.cs
 * 
 * Description:
 * - Base class for different types of cells that work within a Grid2D object
 * - Meant to be subclassed, not used directly
 * 
 * Author(s): 
 * - Dan
*/

using UnityEngine;

using RubberDucks.Utilities.CustomInspectors;

namespace RubberDucks.Utilities.Grids
{
	public abstract class GridCell2D : MonoBehaviour
	{
		[SerializeField][ReadOnly] private Vector2Int m_CoordGridSpace = default;
		private Vector3 m_CoordLocalSpace = default;

		public Vector2Int GridCoord { get => m_CoordGridSpace; set => m_CoordGridSpace = value; }
		public Vector3 LocalSceneCoord
		{
			get => m_CoordLocalSpace; 
			set
			{
				m_CoordLocalSpace = value;
				transform.localPosition = m_CoordLocalSpace;
			}
		}
		public virtual bool IsAvailable { get => !IsOccupied; }
		public virtual bool IsOccupied { get => false; }
		public virtual bool CanBeMoved { get => IsAvailable; }
		public Vector3 WorldSceneCoord => Root.position;
		public Transform Root => transform;

		public virtual void Construct(Vector2Int coordGridSpace, Vector3 coordLocalSpace)
		{
			GridCoord = coordGridSpace;
			LocalSceneCoord = coordLocalSpace;

			gameObject.name = this.ToString();
		}

		public virtual void SetupWithParams(object[] parameters)
		{
			// Stub
		}

		public override string ToString()
		{
			return $"GridCell2D [{m_CoordGridSpace.x}, {m_CoordGridSpace.y}]";
		}
	}
}
