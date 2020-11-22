using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coordinate
{
	public Vector2 m_Position;

	public Dictionary<Direction, Coordinate> m_Coordinates = new Dictionary<Direction, Coordinate>();
	public GridPiece m_Piece;

	public CoordinateRepresentation m_Representation;

	public Coordinate(GridPiece piece, Vector2 position)
	{
		m_Position = position;
		m_Piece = piece;
	}

	public void Populate(List<Coordinate> coordinates)
	{
		FindCoord(coordinates, Direction.North);
		FindCoord(coordinates, Direction.South);
		FindCoord(coordinates, Direction.East);
		FindCoord(coordinates, Direction.West);
	}

	private void FindCoord(List<Coordinate> coordinates, Direction direction)
	{
		var position = WorldGrid.OffsetDirection(GridPosition(), direction);
		for (int i = 0; i < coordinates.Count; ++i)
		{
			var cord = coordinates[i];
			if (cord.GridPosition() == position)
			{
				m_Coordinates[direction] = cord;
				return;
			}
		}
		m_Coordinates[direction] = null;
	}

	public void AppendEmptyNeighbors(ref List<Vector2> neighbors)
	{
		Coordinate coord = null;
		for (int i = 0; i < 5; ++i)
		{
			Direction d = (Direction)i;
			if (!TryMove(d, ref coord))
			{
				var emptyNeighbor = WorldGrid.OffsetDirection(m_Position, d);
				if (!neighbors.Contains(emptyNeighbor))
					neighbors.Add(emptyNeighbor);
			}
		}
	}

	public bool TryMove(Direction direction, ref Coordinate nextCoordinate)
	{
		nextCoordinate = null;
		if (m_Coordinates.TryGetValue(direction, out Coordinate coord))
		{
			if (coord != null)
			{
				nextCoordinate = coord;
				return true;
			}
		}

		return false;
	}

	public Vector2 GridPosition()
	{
		if (m_Piece != null)
			return TranslatedPosition(m_Piece.m_PlacedPosition, m_Piece.m_PlacedDirection);
		else
			return TranslatedPosition(Vector2.zero, Direction.North);
	}

	public Vector2 TranslatedPosition(Vector2 rootPosition, Direction direction)
	{
		Vector2 translated = rootPosition;
		switch (direction)
		{
			case Direction.North:
				translated += m_Position;
				break;
			case Direction.East:
				translated += new Vector2(m_Position.y, -m_Position.x);
				break;
			case Direction.South:
				translated += new Vector2(m_Position.x, -m_Position.y);
				break;
			case Direction.West:
				translated += new Vector2(-m_Position.y, m_Position.x);
				break;
		}

		return translated;
	}

	public void Redecorate()
	{
		if (m_Representation != null)
			Decorate(m_Representation);
	}

	public virtual void Decorate(CoordinateRepresentation rep)
	{
		m_Representation = rep;
		// set to color of shape
		rep.SetColor(GetColor());
	}

	public virtual Color GetColor()
	{
		return m_Piece.GetColor();
	}

	public virtual bool CanBeHealed()
	{
		return true;
	}

	public virtual void Heal(bool fromPiece = false)
	{
		m_Piece.m_Grid.m_Coordinates.Remove(this);

		if (!fromPiece)
			m_Piece.CoordinateHealed(this);

		if (m_Representation != null && m_Representation.gameObject != null)
			GameObject.Destroy(m_Representation.gameObject);
	}
}

public class PollutionCoordinate : Coordinate
{
	public PollutionCoordinate(PollutionPiece piece, Vector2 position) 
		: base(piece, position)
	{
	}

	public override Color GetColor()
	{
		Color color = CanBeHealed() ? Color.yellow : Color.red;
		// we are the source
		if (IsCenterPollutant())
		{
			// need a spout at the center?
			color = CanBeHealed() ? Color.black : Color.red;
		}

		return color;
	}

	public virtual bool IsCenterPollutant()
	{
		return m_Position == Vector2.zero;
	}

	public override bool CanBeHealed()
	{
		if (m_Position == Vector2.zero)
			Debug.Log("HERE");

		bool blocked = m_Coordinates.TryGetValue(Direction.North, out var n);
		blocked &= m_Coordinates.TryGetValue(Direction.South, out var s);
		blocked &= m_Coordinates.TryGetValue(Direction.East, out var e);
		blocked &= m_Coordinates.TryGetValue(Direction.West, out var w);
		blocked &= n != null && s != null && e != null & w != null;
		return !blocked;
	}

	public override void Heal(bool fromPiece = false)
	{
		m_Piece.m_Grid.m_Polluter.m_PollutionCoordinates.Remove(this);
		base.Heal(fromPiece);
	}

	public override void Decorate(CoordinateRepresentation rep)
	{
		base.Decorate(rep);
		if (IsCenterPollutant())
		{
			rep.Offset(0.1f);
		}
	}
}

public class BlockingCoordinate : PollutionCoordinate
{
	public BlockingCoordinate(PollutionPiece piece, Vector2 position)
	: base(piece, position)
	{
	}

	public override Color GetColor()
	{
		return Color.black;
	}

	public override bool CanBeHealed()
	{
		return false;
	}

	public override bool IsCenterPollutant()
	{
		// raise all blocks...
		return true;
	}
}