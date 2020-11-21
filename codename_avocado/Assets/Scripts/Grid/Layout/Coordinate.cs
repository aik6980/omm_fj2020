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

	public bool TryMove(Direction direction, ref Coordinate nextCoordinate)
	{
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
			return m_Piece.m_PlacedPosition + m_Position;
		else
			return m_Position;
	}

	public void Redecorate()
	{
		if (m_Representation != null)
			Decorate(m_Representation);
	}

	public void Decorate(CoordinateRepresentation rep)
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
		return false;
	}

	public virtual void Heal(bool fromPiece = false)
	{
	}
}

public class PollutionCoordinate : Coordinate
{

	private PollutionPiece m_PollutionPiece;

	public PollutionCoordinate(PollutionPiece piece, Vector2 position) 
		: base(piece, position)
	{
		m_PollutionPiece = piece;
	}

	public override Color GetColor()
	{
		Color color = CanBeHealed() ? Color.yellow : Color.red;

		// source is back when healable
		if (CanBeHealed() && m_Position == Vector2.zero)
			color = Color.black;

		return color;
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
		if (!fromPiece)
			m_PollutionPiece.CoordinateHealed(this);

		if (m_Representation != null && m_Representation.gameObject != null)
			GameObject.Destroy(m_Representation.gameObject);
	}
}