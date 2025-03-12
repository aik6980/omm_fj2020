using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Somewhat hacky extension methods to make checks on coordinate types null safe. (Because an extension
/// method doesn't need an object it can introspect the passed 'this' object for null.)
/// </summary>
public static class CoordinateExtensions
{
    public static bool IsPassable(this Coordinate coord) => coord?.Type.IsPassable() ?? false;

    public static bool IsPollutable(this Coordinate coord) => coord?.Type.IsPollutable() ?? false;

    public static bool IsPolluted(this Coordinate coord) => coord?.Type.IsPolluted() ?? false;

	public static bool IsDeepPolluted(this Coordinate coord) => coord?.m_Worldgrid.GetToxicLevel(coord) == GridTileBuilder.ToxicLevel.deep;
}

public class Coordinate
{
	public WorldGrid m_Worldgrid;
	public Vector2Int m_Position;

	//public Dictionary<Direction, Coordinate> m_Coordinates = new Dictionary<Direction, Coordinate>();
    private GridTileBuilder.TileType m_Type;

    public CoordinateRepresentation m_Representation;
    public GridPlayerCharacter m_PopulatedPlayer;
    public GridTileBuilder.ToxicLevel  m_ToxicLevel = GridTileBuilder.ToxicLevel.none;

    public GridTileBuilder.TileType Type { get => m_Type; set => m_Type = value; }

	public Coordinate(WorldGrid world_grid, Vector2Int position, GridTileBuilder.TileType type)
	{
		m_Worldgrid = world_grid;
		m_Position = position;
		m_Type = type;
	}

	public void AppendEmptyNeighbors(ref List<Vector2Int> neighbors)
	{
		for (int i = 0; i < System.Enum.GetValues(typeof(Direction)).Length; ++i)
		{
			Direction d = (Direction)i;
			var nextCoordinate = m_Worldgrid.GetAdjacentCoordinate(m_Position, d);
			if (nextCoordinate != null && nextCoordinate.IsPollutable())
			{
				var emptyNeighbor = nextCoordinate.m_Position;
				if (!neighbors.Contains(emptyNeighbor))
					neighbors.Add(emptyNeighbor);
			}
		}
	}

	public Vector2 GridPosition()
	{
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
	}
}