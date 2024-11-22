/*
 * Grid2D.cs
 * 
 * Description:
 * - Simple 2D grid system which can be used with different types for what is actually in each cell
 * - The coordinates are built to have a center so there is a (0, 0) location. So, this works best with odd grids for now
 * - TODO Dan: Add better support for even grids in the future
 * 
 * Author(s): 
 * - Dan
*/
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using RubberDucks.Utilities;
using RubberDucks.Utilities.Extensions;

namespace RubberDucks.Utilities.Grids
{
	public class Grid2D<TGridCell> : Singleton<Grid2D<TGridCell>>
		where TGridCell : GridCell2D
	{
		//--- Events ---//
		[System.Serializable]
		public class EventList
		{
			public UnityEvent OnGridGenerated;
			public UnityEvent OnGridDestroyed;
		}
		[Header("Events")]
		public EventList m_Events = default;

		//--- Helper Structures ---//

		//--- Statics and Constants ---//
		protected static readonly Vector2Int[] m_CardinalDirections = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
		protected static readonly Vector2Int[] m_DiagonalDirections = {  Vector2Int.up   + Vector2Int.right,
																Vector2Int.down + Vector2Int.right,
																Vector2Int.down + Vector2Int.left,
																Vector2Int.up   + Vector2Int.left};
		protected static readonly Vector2Int[] m_CardinalAndDiagonalDirections = {m_CardinalDirections[0], 																		  m_DiagonalDirections[0],
																				m_CardinalDirections[1], m_DiagonalDirections[1],
																				m_CardinalDirections[2], m_DiagonalDirections[2],
																				m_CardinalDirections[3], m_DiagonalDirections[3]};

		//--- Properties ---//
		public int HalfGridWidth { get => m_RowAndColCount.x / 2; }
		public int HalfGridHeight { get => m_RowAndColCount.y / 2; }
		public Vector3 HalfCellSize { get => m_CellSizingLocalSpace * 0.5f; }
		public static Vector2Int[] CardinalDirections { get => m_CardinalDirections; }
		public static Vector2Int[] DiagonalDirections { get => m_DiagonalDirections; }
		public static Vector2Int[] CardinalAndDiagonalDirections { get => m_CardinalAndDiagonalDirections; }
		public List<TGridCell> CellsAsList => new List<TGridCell>(m_Cells.Values);

		//--- Public Variables ---//

		//--- Protected Variables ---//
		[Header("Lifecycle Controls")]
		[SerializeField] protected UnityLifeCycle.InitialStages m_RegenerationTime = UnityLifeCycle.InitialStages.Awake; 

		[Header("Internal Grid Data")]
		[SerializeField] protected Vector2Int m_RowAndColCount = new Vector2Int(7, 7);
		[SerializeField] protected bool m_CenterCoordinatesAroundMiddle = true;

		[Header("Scene Visuals")]
		[SerializeField] protected Transform m_CellParent = default;
		[SerializeField] protected TGridCell m_CellPrefab = default;
		[SerializeField] protected Vector2 m_CellSizingLocalSpace = new Vector2(1.0f, 1.0f);
		[SerializeField] protected Vector2 m_CellSpacingLocalSpace = new Vector2(0.0f, 0.0f);

		protected Dictionary<Vector2Int, TGridCell> m_Cells = default;

		//--- Private Variables ---//

		//--- Unity Methods ---//
		private void Awake()
		{
			if (m_RegenerationTime == UnityLifeCycle.InitialStages.Awake)
			{
				GenerateGrid();
			}
		}

		private void Start()
		{
			if (m_RegenerationTime == UnityLifeCycle.InitialStages.Start)
			{
				GenerateGrid();
			}
		}

		//--- Public Methods ---//
		public virtual void GenerateGrid(object[] parameters = null)
		{
			DestroyGrid();
			GenerateCells(parameters);

			m_Events.OnGridGenerated.Invoke();
		}

		public virtual void DestroyGrid()
		{
			m_CellParent.DestroyChildren();

			m_Events.OnGridDestroyed.Invoke();
		}

		public virtual bool TryGetCell(Vector2Int coord, out TGridCell outCell)
		{
			if (!m_Cells.ContainsKey(coord))
			{
				outCell = null;
				return false;
			}

			outCell = m_Cells[coord];
			return true;
		}

		public virtual bool AreCellsCardinallyAdjacent(Vector2Int coordA, Vector2Int coordB)
		{
			Vector2 coordDiff = CalculateCoordDifferences(coordA, coordB);
			return ((coordDiff.x == 0 && coordDiff.y == 1) || (coordDiff.x == 1 && coordDiff.y == 0));
		}

		public virtual bool AreCellsCardinallyAdjacent(TGridCell cellA, TGridCell cellB)
		{
			return AreCellsCardinallyAdjacent(cellA.GridCoord, cellB.GridCoord);
		}

		public virtual bool AreCellsDiagonallyAdjacent(Vector2Int coordA, Vector2Int coordB)
		{
			Vector2 coordDiff = CalculateCoordDifferences(coordA, coordB);
			return (coordDiff.x == 1 && coordDiff.y == 1);
		}

		public virtual bool AreCellsDiagonallyAdjacent(TGridCell cellA, TGridCell cellB)
		{
			return AreCellsDiagonallyAdjacent(cellA.GridCoord, cellB.GridCoord);
		}

		public virtual bool FindNearestOccupiedCell(Vector2Int startCoord, Vector2Int direction, out TGridCell outCell)
		{
			Vector2Int nextCoord = startCoord + direction;
			outCell = null;

			while (TryGetCell(nextCoord, out var cell))
			{
				nextCoord += direction;

				if (cell.IsOccupied)
				{
					outCell = cell;
					break;
				}
			}

			return (outCell != null);
		}

		public virtual Vector2Int GetRandomCoordOnGrid()
		{
			return new Vector2Int(Random.Range(-HalfGridWidth, HalfGridWidth + 1), Random.Range(-HalfGridHeight, HalfGridHeight + 1));
		}

		public virtual Vector3 GetWorldDirectionFromCardinalDirection(Vector2Int cardinalDirection)
		{
			if (cardinalDirection == Vector2Int.up)
			{
				return m_CellParent.forward;
			}
			else if (cardinalDirection == Vector2Int.right)
			{
				return m_CellParent.right;
			}
			else if (cardinalDirection == Vector2.down)
			{
				return -m_CellParent.forward;
			}
			else if (cardinalDirection == Vector2.left)
			{
				return -m_CellParent.right;
			}
			else
			{
				Debug.LogWarning("Non-cardinal direction sent to Grid2D.GetWorldDirectionFromCardinalDirection(). Returning Vector3.zero instead!");
				return Vector3.zero;
			}
		}
		
		public List<TGridCell> GetAdjacentCells(Vector2Int anchorCoord, List<Vector2Int> directions)
		{
			List<TGridCell> adjacentCells = new List<TGridCell>();

			foreach (var direction in directions)
			{
				if (TryGetCell(anchorCoord + direction, out var boardCell))
				{
					adjacentCells.Add(boardCell);
				}
			}

			return adjacentCells;
		}

		private bool ValidateAdjacentCells(Vector2Int anchorCoord, List<Vector2Int> directionsToCheck)
		{
			if (TryGetCell(anchorCoord, out var anchorCell))
			{
				return directionsToCheck.TrueForAll(dir => TryGetCell(anchorCoord + dir, out var tile));
			}

			return false;
		}

		public bool Is3x3RotationValid(Vector2Int centerTileCoord)
		{
			return ValidateAdjacentCells(centerTileCoord, new List<Vector2Int>(CardinalAndDiagonalDirections));
		}

		public bool Try3x3Rotation(Vector2Int centerTileCoord, bool clockwise, int numQuarterTurns = 1)
		{
			if (!Is3x3RotationValid(centerTileCoord))
			{
				return false;
			}

			if (TryGetCell(centerTileCoord, out var centerCell))
			{
				// TODO: Maybe swap AllDirections so it gives these directions in this order
				// The order matters since the shuffling algorithm assumes they are lined up sequentially
				List<Vector2Int> directions = new List<Vector2Int> { Vector2Int.up, Vector2Int.up + Vector2Int.right,
																	 Vector2Int.right, Vector2Int.right + Vector2Int.down,
																	 Vector2Int.down, Vector2Int.down + Vector2Int.left,
																	 Vector2Int.left, Vector2Int.left + Vector2Int.up};
				
				List<TGridCell> adjacentCells = GetAdjacentCells(centerTileCoord, directions);
				RotateTileData3x3(adjacentCells, clockwise, numQuarterTurns);

				return true;
			}

			return false;
		}

		public bool Is2x2RotationValid(Vector2Int bottomLeftCoord)
		{
			return ValidateAdjacentCells(bottomLeftCoord, new List<Vector2Int> { Vector2Int.up, Vector2Int.right, Vector2Int.up + Vector2Int.right });
		}

		public bool Try2x2Rotation(Vector2Int bottomLeftCoord, bool clockwise, int numQuarterTurns = 1)
		{
			if (!Is2x2RotationValid(bottomLeftCoord))
			{
				return false;
			}

			List<TGridCell> cellsToRotate = new List<TGridCell>();
			if (TryGetCell(bottomLeftCoord, out var bottomLeftCell))
			{
				// NOTE: Like with the 3x3, the order of the directions matter. They need to be sequential 
				cellsToRotate.Add(bottomLeftCell);
				cellsToRotate.AddRange(GetAdjacentCells(bottomLeftCoord, new List<Vector2Int> { Vector2Int.up, Vector2Int.up + Vector2Int.right, Vector2Int.right }));
				
				RotateTileData2x2(cellsToRotate, clockwise, numQuarterTurns);

				return true;
			}

			return false;
		}

		// TODO Dan: Add an optional spacing parameter to the swap checks to allow for the other set of planned cards that skip a tile
		public bool IsHorizontalSwapValid(Vector2Int tileACoord, Vector2Int tileBCoord)
		{
			return (tileACoord.y == tileBCoord.y) && AreCellsCardinallyAdjacent(tileACoord, tileBCoord);
		}

		public bool TryHorizontalSwap(Vector2Int tileACoord, Vector2Int tileBCoord)
		{
			if (!IsHorizontalSwapValid(tileACoord, tileBCoord))
			{
				return false;
			}

			return TrySwapTiles(tileACoord, tileBCoord);
		}

		public bool IsVerticalSwapValid(Vector2Int tileACoord, Vector2Int tileBCoord)
		{
			return (tileACoord.x == tileBCoord.x) && AreCellsCardinallyAdjacent(tileACoord, tileBCoord);
		}

		public bool TryVerticalSwap(Vector2Int tileACoord, Vector2Int tileBCoord)
		{
			if (!IsVerticalSwapValid(tileACoord, tileBCoord))
			{
				return false;
			}

			return TrySwapTiles(tileACoord, tileBCoord);
		}

		//--- Protected Methods ---//
		protected virtual void GenerateCells(object[] parameters)
		{
			m_Cells = new Dictionary<Vector2Int, TGridCell>();

			// If using an odd # of cols and rows, centering around the middle makes sense so the middle is 0,0
			// If using an even #, anchoring in the bottom left makes sense instead
			if (m_CenterCoordinatesAroundMiddle)
			{
				for (int xCoord = -HalfGridWidth; xCoord <= HalfGridWidth; xCoord++)
				{
					for (int yCoord = -HalfGridHeight; yCoord <= HalfGridHeight; yCoord++)
					{
						SpawnCell(xCoord, yCoord);
					}
				}
			}
			else
			{
				for (int xCoord = 0; xCoord < m_RowAndColCount.x; xCoord++)
				{
					for (int yCoord = 0; yCoord < m_RowAndColCount.y; yCoord++)
					{
						SpawnCell(xCoord, yCoord);
					}
				}
			}
		}
		
		protected virtual void SpawnCell(int xCoord, int yCoord)
		{
			Vector2Int cellCoord = new Vector2Int(xCoord, yCoord);
			Vector3 cellLocalSpacePos = CalculateLocalSpacePosition(cellCoord);

			TGridCell cellComp = Instantiate(m_CellPrefab, m_CellParent); // TODO Dan: switch this to use pools
			cellComp.Construct(cellCoord, cellLocalSpacePos);

			m_Cells.Add(cellCoord, cellComp);
		}
		
		protected Vector3 CalculateLocalSpacePosition(Vector2Int coord)
		{
			Vector3 cellLocalSpacePos = new Vector3();
			
			float xSpacingOffset = m_CellSpacingLocalSpace.x * coord.x;
			float xCellSizeOffset = m_CellSizingLocalSpace.x * coord.x;
			cellLocalSpacePos.x = xSpacingOffset + xCellSizeOffset;

			cellLocalSpacePos.y = 0.0f;

			float zSpacingOffset = m_CellSpacingLocalSpace.y * coord.y;
			float zCellSizeOffset = m_CellSizingLocalSpace.y * coord.y;
			cellLocalSpacePos.z = zSpacingOffset + zCellSizeOffset;

			return cellLocalSpacePos;
		}

		protected Vector2Int CalculateCoordDifferences(Vector2Int coordA, Vector2Int coordB)
		{
			Vector2Int diff = new Vector2Int();
			diff.x = Mathf.Abs(coordA.x - coordB.x);
			diff.y = Mathf.Abs(coordA.y - coordB.y);
			return diff;
		}
		
		protected void RotateTileData3x3(List<TGridCell> tilesToSwap, bool clockwise, int numQuarterTurns)
		{
			// For every quarter turn, the tiles will move 2 around the circle for a 3x3
			// ie: if the tile is top left, after quarter clockwise rotation it will be top right which is 2 tiles over around the ring
			// ie: if the tile is top left, after 180 clockwise rotation it will be bottom right which is 4 tiles over around the ring
			RotateTiles(tilesToSwap, clockwise, numQuarterTurns, 2);
		}

		protected void RotateTileData2x2(List<TGridCell> tilesToSwap, bool clockwise, int numQuarterTurns)
		{
			// For every quarter turn, the tiles will move 1 around the circle for a 2x2
			// ie: if the tile is top left, after quarter clockwise rotation, it will be top right which is 1 tile over (not 2 like in the 3x3 case)
			RotateTiles(tilesToSwap, clockwise, numQuarterTurns, 1);
		}

		protected void RotateTiles(List<TGridCell> tilesToSwap, bool clockwise, int numQuarterTurns, int numSpacesPerQuarterTurn)
		{
			int numSpacesToMove = numSpacesPerQuarterTurn * numQuarterTurns;

			// Store the positions in order of the tiles
			List<Vector2Int> tileCoords = new List<Vector2Int>();
			tilesToSwap.ForEach(delegate (TGridCell tile) { tileCoords.Add(tile.GridCoord); });

			if (!clockwise)
			{
				tileCoords.Reverse();
				tilesToSwap.Reverse();
			}

			// Loop through the tiles and assign the positions to be the number of spaces offset from their original placement
			for (int tileIdx = 0; tileIdx < tilesToSwap.Count; ++tileIdx)
			{
				int newPositionIdx = tileIdx + numSpacesToMove;
				int newPositionIdxWrapped = newPositionIdx % tilesToSwap.Count;

				TGridCell tile = tilesToSwap[tileIdx];
				tile.GridCoord = tileCoords[newPositionIdxWrapped];

				m_Cells[tile.GridCoord] = tile;
			}
		}

		protected bool TrySwapTiles(Vector2Int tileACoord, Vector2Int tileBCoord)
		{
			if (TryGetCell(tileACoord, out var cellA) && TryGetCell(tileBCoord, out var cellB))
			{
				SwapTileData(cellA, cellB);

				return true;
			}

			return false;
		}

		protected void SwapTileData(TGridCell cellA, TGridCell cellB)
		{
			Vector2Int cellACoord = cellA.GridCoord;
			cellA.GridCoord = cellB.GridCoord;
			cellB.GridCoord = cellACoord;

			m_Cells[cellA.GridCoord] = cellA;
			m_Cells[cellB.GridCoord] = cellB;
		}
		
		//--- Private Methods ---//
	}
}
