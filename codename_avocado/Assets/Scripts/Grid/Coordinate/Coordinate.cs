using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate
{
	Vector2 m_Position;

	public Dictionary<Direction, Coordinate> m_Coordinates = new Dictionary<Direction, Coordinate>();
	GridPiece m_Piece;

	public Coordinate(GridPiece piece, Vector2 position)
	{
		m_Position = position;
		m_Piece = piece;
	}

	public void Populate(WorldGrid grid)
	{
		FindCoord(grid, Direction.North );
		FindCoord(grid, Direction.South );
		FindCoord(grid, Direction.East );
		FindCoord(grid, Direction.West );
	}

	private void FindCoord(WorldGrid grid, Direction direction)
	{
		var position = WorldGrid.OffsetDirection(GridPosition(), direction);
		for (int i = 0; i < grid.m_Coordinates.Count; ++i)
		{
			var cord = grid.m_Coordinates[i];
			if (cord.m_Piece.m_Placed && cord.GridPosition() == position)
			{
				m_Coordinates[direction] = cord;
				break;
			}
		}
	}

	public bool TryMove(Direction direction, ref Coordinate nextCoordinate)
	{
		if (m_Coordinates.TryGetValue(direction, out Coordinate coord))
		{
			nextCoordinate = coord;
			return true;
		}

		return false;
	}

	public Vector2 GridPosition()
	{
		return m_Piece.m_PlacedPosition + m_Position;
	}

	public void Decorate(CoordinateRepresentation rep)
	{
		// set to color of shape
	}
}

public class GridPiece
{
	public bool m_Placed = false;
	public Vector2 m_PlacedPosition;
	public List<Vector2> m_Positions = new List<Vector2>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	private WorldGrid m_Grid;

	public GridPiece(WorldGrid grid, List<Vector2> positions)
	{
		m_Grid = grid;
		m_Grid.m_Pieces.Add(this);
		m_Positions = positions;
		for (int i = 0; i < m_Positions.Count; ++i)
		{
			var newCoord = new Coordinate(this, m_Positions[i]);
			m_Coordinates.Add(newCoord);
			grid.m_Coordinates.Add(newCoord);
		}
	}

	public void PopulateCoords(WorldGrid grid)
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Populate(grid);
		}
	}

	public static GridPiece GeneratePiece(WorldGrid grid)
	{
		List<Vector2> coords = new List<Vector2>();
		int randomShape = Random.Range(0, 2);
		coords.Add(new Vector2(0, 0));
		switch (randomShape)
		{
			case 0:
				// make random shape...
				coords.Add(new Vector2(0, 1));
				coords.Add(new Vector2(0, 2));
				coords.Add(new Vector2(0, 3));
				break;
			case 1:
				// make random shape...
				coords.Add(new Vector2(0, 1));
				coords.Add(new Vector2(0, 2));
				coords.Add(new Vector2(1, 2));
				break;
		}
		GridPiece newPiece = new GridPiece(grid, coords);
		return newPiece;
	}

	public void Place(Vector2 position)
	{
		m_PlacedPosition = position;
		m_Placed = true;
		m_Grid.RepresentPiece(this);
	}
}