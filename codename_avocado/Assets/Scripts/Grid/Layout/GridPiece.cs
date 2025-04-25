using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece
{
	public Direction m_PlacedDirection;
	public Vector2 m_PlacedPosition;
	
	public WorldGrid				m_Grid;
	public Shape					m_Shape;
	public Vector2Int				m_origin_coords;
	public GridTileBuilder.TileType		m_TileType;
	public GridTileBuilder.ToxicLevel m_ToxicLevel = GridTileBuilder.ToxicLevel.none;

	public bool m_SuperPiece;


	protected List<Coordinate>			m_Coordinates = new List<Coordinate>();

	public List<Coordinate>		Coordinates
    {
		get => m_Coordinates;
	}

	public GridPiece(WorldGrid grid, Shape shape, Vector2Int coords, GridTileBuilder.TileType tileType, bool superPiece)
	{
		m_SuperPiece = superPiece;
		m_Grid = grid;
		m_Shape = shape;
		m_origin_coords = coords;
		m_TileType = tileType;

		m_Coordinates = m_Grid.GetCoordinatesForShape(m_origin_coords, Direction.North, shape.Coordinates());
	}

    public static GridPiece GeneratePiece(WorldGrid grid, Vector2Int position, GridTileBuilder.TileType tileType, Shape shapeOverride = null)
	{
		Shape shape = shapeOverride;
		if (shape == null)
			shape = Shape.RandomShape();

		GridPiece newPiece = new GridPiece(grid, shape, position, tileType, shape.Coordinates().Count > 6);
		return newPiece;
	}

    public PreviewPlacement PreviewPlacement(Vector2 position, Direction direction)
	{
		m_PlacedPosition = position;
		m_origin_coords = Vector2Int.RoundToInt(position);
		m_PlacedDirection = direction;

		m_Coordinates = m_Grid.GetCoordinatesForShape(m_origin_coords, direction, m_Shape.Coordinates());

		var preview = new PreviewPlacement(m_Grid.UpdateTileRepresentationNow(this));
		return preview;
	}

	public void RedecorateCords()
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Redecorate();
		}
	}
}


public class PollutionPiece : GridPiece
{
	public PollutionPiece(WorldGrid grid, Shape shape, Vector2Int position)
		: this(grid, shape, position, GridTileBuilder.TileType.toxic)
	{
	}

	protected PollutionPiece(WorldGrid grid, Shape shape, Vector2Int position, GridTileBuilder.TileType tileType)
		: base(grid, shape, position, tileType, false)
	{
		m_PlacedDirection = Direction.North;
		RedecorateCords();
	}

	public virtual void TickPollution()
    {
    }
}


public class BlockingPiece : PollutionPiece
{
	public BlockingPiece(WorldGrid grid, Shape shape, Vector2Int position)
		: base(grid, shape, position, GridTileBuilder.TileType.obstacle)
	{
		m_PlacedDirection = Direction.North;
		RedecorateCords();
	}
}

public class ToxicPiece : PollutionPiece
{
	public float m_ExpansionTime = 9999.0f;
	private float m_LastExpansion = -1f;
	private int m_MaxSpread = 0;

	List<Coordinate> m_SourceCoordinates;
	List<Vector2Int> m_CurrentExpansion = new List<Vector2Int>();

	public ToxicPiece(WorldGrid grid, Shape shape, Vector2Int position, int max_spread)
		: base(grid, shape, position, GridTileBuilder.TileType.toxic_pool)
	{
		m_SourceCoordinates = m_Coordinates;
		m_MaxSpread = max_spread;
		m_ToxicLevel = GridTileBuilder.ToxicLevel.deep;
		m_ExpansionTime = grid.m_Polluter.m_PollutionExpansionTime;
		var variation = grid.m_Polluter.m_PollutionExpansionTimeVariation;
		m_LastExpansion = Time.time + Random.Range(m_ExpansionTime - variation, m_ExpansionTime + variation);

		m_PlacedDirection = Direction.North;
		RedecorateCords();

		m_Grid.OnCoordinateTypeChanged += Coord_OnCoordinateTypeChanged;
	}

	private void Coord_OnCoordinateTypeChanged(Coordinate coordinate, GridTileBuilder.TileType previous_type)
    {
		if (m_Coordinates.Contains(coordinate) == false)
			return;

		if (previous_type == GridTileBuilder.TileType.toxic &&
			coordinate.Type != GridTileBuilder.TileType.toxic)
		{
			m_Coordinates.Remove(coordinate);
		}

		if (previous_type == GridTileBuilder.TileType.toxic_pool &&
			coordinate.Type != GridTileBuilder.TileType.toxic_pool)
        {
			var copy = new List<Coordinate>(m_Coordinates);
			m_Coordinates.Clear();
			copy.ForEach(coord =>
			{
				// Don't overwrite the coordinate that the pool occupied, it has been changed already!
				if (coord != coordinate)
					m_Grid.SetCoordType(coord, GridTileBuilder.TileType.floor);
			});
			m_Grid.m_Polluter.HealPositions(copy);
		}
	}

    public override void TickPollution()
	{
		if (m_LastExpansion > 0f)
		{
			float delta = Time.time - m_LastExpansion;
			if (delta >= m_ExpansionTime)
			{
				m_LastExpansion = Time.time;
				// grow outwards...
				Expand();
				RedecorateCords();
			}
			m_SourceCoordinates.ForEach(coord => m_Grid.GetCoordinateRepresentation(coord)?.UpdateTimer(m_ExpansionTime - delta));
		}
	}

	public void GenerateExpansion()
	{
		// get empty neighbors and build new neighbor there...
		m_CurrentExpansion = new List<Vector2Int>();
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].AppendEmptyNeighbors(ref m_CurrentExpansion);
		}

		if (m_MaxSpread > 0)
		{
			var source_tiles = m_Coordinates
				.Where(coord => coord.Type == GridTileBuilder.TileType.toxic_pool)
				.Select(coord => coord.m_Position)
				.ToList();
			m_CurrentExpansion.RemoveAll(vec =>
			{
				int max_dist = source_tiles.Aggregate(9999999, (acc, src_vec) => System.Math.Min(ManhattenDistance(vec, src_vec), acc));
				return max_dist > m_MaxSpread;
			});
		}

		int ManhattenDistance(Vector2Int a, Vector2Int b)
        {
			return System.Math.Abs(a.x - b.x) + System.Math.Abs(a.y - b.y);
        }
	}

	public void Expand()
	{
		if (m_CurrentExpansion.Count == 0)
			GenerateExpansion();

		if (m_CurrentExpansion.Count == 0)
			return;

		List<Vector2Int> coordsToGrow = new List<Vector2Int>();
		// only do one for now...
		int randomExpand = Random.Range(0, m_CurrentExpansion.Count);
		var expansion = m_CurrentExpansion[randomExpand];
		m_CurrentExpansion.Remove(expansion);
		coordsToGrow.Add(expansion);

		List<Coordinate> newCoords = coordsToGrow.Select(vec => m_Grid.GetCoordinate(vec.x, vec.y)).ToList();
		for (int i = 0; i < newCoords.Count; ++i)
		{
			// player consumed by lava
			if (newCoords[i].m_PopulatedPlayer != null)
				newCoords[i].m_PopulatedPlayer.Die();
		}

		RedecorateCords();
		for (int i = 0; i < newCoords.Count; ++i)
		{
			m_Coordinates.Add(newCoords[i]);
			m_Grid.SetCoordType(newCoords[i], GridTileBuilder.TileType.toxic);
		}

		m_Grid.UpdateTileRepresentationNow(this);
	}

}

public class PreviewPlacement
{
	List<CoordinateRepresentation> m_Reps;

	public PreviewPlacement(List<CoordinateRepresentation> reps)
	{
		m_Reps = reps;
	}

	public void Clear()
	{
	}
}
