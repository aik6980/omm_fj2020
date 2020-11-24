using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coordinate
{
	public WorldGrid m_Worldgrid;
	public Vector2Int m_Position;

	//public Dictionary<Direction, Coordinate> m_Coordinates = new Dictionary<Direction, Coordinate>();
	public GridPiece m_Piece;
	public GridTileBuilder.TileType m_Type;

	public CoordinateRepresentation m_Representation;
	public GridPlayerCharacter m_PopulatedPlayer;
    public GridTileBuilder.ToxicLevel  m_ToxicLevel = GridTileBuilder.ToxicLevel.none;

    public Coordinate(WorldGrid world_grid, GridPiece piece, Vector2Int position, bool bustin_makes_me_feel_good)
	{
		m_Worldgrid = world_grid;
		m_Position = position;
		m_Piece = piece;
		m_Type = m_Piece.m_TileType;
	}

	public Coordinate(WorldGrid world_griid, Vector2Int position, GridTileBuilder.TileType type)
	{
		m_Worldgrid = world_griid;
		m_Position = position;
		m_Piece = null;
		m_Type = type;
	}

    //public void Populate(List<Coordinate> coordinates)
    //{
    //    FindCoord(coordinates, Direction.North);
    //    FindCoord(coordinates, Direction.South);
    //    FindCoord(coordinates, Direction.East);
    //    FindCoord(coordinates, Direction.West);
    //}

    //private void FindCoord(List<Coordinate> coordinates, Direction direction)
    //{
    //    var position = WorldGrid.OffsetDirection(GridPosition(), direction);
    //    for (int i = 0; i < coordinates.Count; ++i)
    //    {
    //        var cord = coordinates[i];
    //        if (cord.GridPosition() == position)
    //        {
    //            m_Coordinates[direction] = cord;
    //            return;
    //        }
    //    }
    //    m_Coordinates[direction] = null;
    //}

    public void AppendEmptyNeighbors(ref List<Vector2Int> neighbors)
	{
		Coordinate coord = null;
		for (int i = 0; i < System.Enum.GetValues(typeof(Direction)).Length; ++i)
		{
			Direction d = (Direction)i;
			//if (!TryMove(d, ref coord))
			var nextCoordinate = m_Worldgrid.GetNextCoordinate(m_Position, d);
			if (nextCoordinate != null && nextCoordinate.IsPollutable())
			{
				var emptyNeighbor = /*Vector2Int.RoundToInt(WorldGrid.OffsetDirection(m_Position, d))*/nextCoordinate.m_Position;
				if (!neighbors.Contains(emptyNeighbor))
					neighbors.Add(emptyNeighbor);
			}
		}
	}

	public bool IsPassable()
    {
		return
			m_Type == GridTileBuilder.TileType.grass ||
			m_Type == GridTileBuilder.TileType.exit ||
			m_Type == GridTileBuilder.TileType.start;
	}

	public bool IsPollutable()
	{
		return
			m_Type == GridTileBuilder.TileType.grass ||
			m_Type == GridTileBuilder.TileType.floor;
	}

	public bool TryMove(Direction direction, ref Coordinate nextCoordinate)
	{
		//nextCoordinate = null;
		//// 
		//if (m_Coordinates.TryGetValue(direction, out Coordinate coord))
		//{
		//	if (coord != null && coord.IsPassable())
		//	{
		//		nextCoordinate = coord;
		//		return true;
		//	}
		//}

		// should store World grid here as a parent
		nextCoordinate = m_Worldgrid.GetNextCoordinate(m_Position, direction);
		if(nextCoordinate != null && nextCoordinate.IsPassable())
        {
			return true;
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
		if (m_Piece == null)
			return Color.clear;

		return m_Piece.GetColor();
	}

	public bool CanBeHealed()
	{
		switch(m_Type)
        {
			case GridTileBuilder.TileType.start: // start and exit can be healed but cannot change the representation
			case GridTileBuilder.TileType.exit:
			case GridTileBuilder.TileType.floor:
			case GridTileBuilder.TileType.grass:
				return true;
			case GridTileBuilder.TileType.obstacle:
				return false;
			case GridTileBuilder.TileType.toxic:
				return CanBeHealed_Toxic();

        }

		Debug.Assert(false);
		return false;
	}

	bool CanBeHealed_Toxic()
	{
		//if (m_Position == Vector2.zero)
		//	Debug.Log("HERE");

		// Need to figure this bit out
		//bool blocked = m_Coordinates.TryGetValue(Direction.North, out var n);
		//blocked &= m_Coordinates.TryGetValue(Direction.South, out var s);
		//blocked &= m_Coordinates.TryGetValue(Direction.East, out var e);
		//blocked &= m_Coordinates.TryGetValue(Direction.West, out var w);
		//blocked &= n != null && s != null && e != null & w != null;
		//return !blocked;
		
		for(int i=0; i< System.Enum.GetValues(typeof(Direction)).Length; ++i)
        {
			var coord = m_Worldgrid.GetNextCoordinate(m_Position, (Direction)i);
			if(coord.m_Type != GridTileBuilder.TileType.toxic)
            {
				return true;
            }
        }

		return false;
	}


	public virtual void Heal(bool fromPiece = false)
	{
		if (m_Piece != null)
		{
			m_Piece.m_Grid.m_Coordinates.Remove(this);
			m_Type = GridTileBuilder.TileType.floor;

			if (!fromPiece)
				m_Piece.CoordinateHealed(this);

			if (m_Representation != null && m_Representation.gameObject != null)
				GameObject.Destroy(m_Representation.gameObject);
		}
	}
}

public class PollutionCoordinate : Coordinate
{
	public PollutionCoordinate(WorldGrid world_grid, PollutionPiece piece, Vector2Int position) 
		: base(world_grid, piece, position, true)
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

	public bool CanBeHealed_obsoleted()
	{
		//if (m_Position == Vector2.zero)
		//	Debug.Log("HERE");

		// Need to figure this bit out
		//bool blocked = m_Coordinates.TryGetValue(Direction.North, out var n);
		//blocked &= m_Coordinates.TryGetValue(Direction.South, out var s);
		//blocked &= m_Coordinates.TryGetValue(Direction.East, out var e);
		//blocked &= m_Coordinates.TryGetValue(Direction.West, out var w);
		//blocked &= n != null && s != null && e != null & w != null;
		//return !blocked;

		Coordinate next_c = null;
		bool blocked = TryMove(Direction.North, ref next_c);
		blocked &= TryMove(Direction.South, ref next_c);
		blocked &= TryMove(Direction.East, ref next_c);
		blocked &= TryMove(Direction.West, ref next_c);

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


//obsoleted
//public class BlockingCoordinate : PollutionCoordinate
//{
//	public BlockingCoordinate(WorldGrid world_grid, PollutionPiece piece, Vector2Int position)
//	: base(world_grid, piece, position)
//	{
//	}

//	public override Color GetColor()
//	{
//		return Color.black;
//	}

//	public override bool CanBeHealed()
//	{
//		return false;
//	}

//	public override bool IsCenterPollutant()
//	{
//		// raise all blocks...
//		return true;
//	}
//}