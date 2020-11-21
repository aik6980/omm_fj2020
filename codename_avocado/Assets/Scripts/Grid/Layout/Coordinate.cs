using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coordinate
{
	public Vector2 m_Position;

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
		if (m_Piece != null)
			return m_Piece.m_PlacedPosition + m_Position;
		else
			return m_Position;
	}

	public void Decorate(CoordinateRepresentation rep)
	{
		// set to color of shape
	}
}

public class BlockingCoordinate : Coordinate
{

	public BlockingCoordinate(Vector2 position) 
		: base(null, position)
	{

	}
}