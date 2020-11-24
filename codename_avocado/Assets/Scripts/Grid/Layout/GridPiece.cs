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


	public event System.Action<GridPiece> OnCoordinatesChanged;
	protected void SignalCoordsChanged()
    {
		OnCoordinatesChanged?.Invoke(this);
	}

	protected List<Coordinate>			m_Coordinates = new List<Coordinate>();

	public IEnumerable<Coordinate>		Coordinates
    {
		get => m_Coordinates;
	}

	public GridPiece(WorldGrid grid, Shape shape, Vector2Int coords, GridTileBuilder.TileType tileType)
	{
		m_Grid = grid;
		m_Shape = shape;
		m_origin_coords = coords;
		m_TileType = tileType;


		//m_Coordinates = GetCoordinatesForShape(m_origin_coords, Direction.North, shape.Coordinates());
		//m_Coordinates.ForEach(coord => coord.m_Type = tileType);
	}

	protected List<Coordinate> GetCoordinatesForShape(Vector2Int origin, Direction dir, List<Vector2Int> positions)
	{
		List<Coordinate> newCoordinates = new List<Coordinate>();
		for (int i = 0; i < positions.Count; ++i)
		{
			var transformed_position = Shape.Faceto(positions[i], dir);
			var newCoord = m_Grid.GetCoordinate(origin + transformed_position);
			if (newCoord != null)
				newCoordinates.Add(newCoord);
		}

		return newCoordinates;
	}

	//protected virtual Coordinate BuildCoordinate(Vector2 position)
	//{
	//	return m_Grid.GetCoordinate(position);
	//	//return new Coordinate(this, position);
	//}

	public virtual Color GetColor()
	{
		return m_Shape.GetColor();
	}

    //public void PopulateCoords(List<Coordinate> coordinates)
    //{
    //    for (int i = 0; i < m_Coordinates.Count; ++i)
    //    {
    //        m_Coordinates[i].Populate(coordinates);
    //    }
    //}

    public static GridPiece GeneratePiece(WorldGrid grid, Vector2Int position, GridTileBuilder.TileType tileType, Shape shapeOverride = null)
	{
		Shape shape = shapeOverride;
		if (shape == null)
			shape = Shape.RandomShape();

		GridPiece newPiece = new GridPiece(grid, shape, position, tileType);
		return newPiece;
	}

    public void Place(Vector2 position, Direction direction)
    {
		m_Coordinates = GetCoordinatesForShape(Vector2Int.RoundToInt(position), direction, m_Shape.Coordinates());
		var toxicity = m_Shape.Toxicity();

		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].SetCoordType(m_TileType, toxicity.Count > i ? toxicity[i] : GridTileBuilder.ToxicLevel.none);
		}

		OnCoordinatesChanged?.Invoke(this);
	}

    public PreviewPlacement PreviewPlacement(Vector2 position, Direction direction)
	{
		m_PlacedPosition = position;
		m_origin_coords = Vector2Int.RoundToInt(position);
		m_PlacedDirection = direction;

		m_Coordinates = GetCoordinatesForShape(m_origin_coords, direction, m_Shape.Coordinates());

		var preview = new PreviewPlacement(m_Grid.UpdateTileRepresentation(this));
		OnCoordinatesChanged?.Invoke(this);
		return preview;
	}

	public void RedecorateCords()
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Redecorate();
		}
	}

	public void CoordinateHealed(Coordinate coordinate)
	{
		Debug.LogError("WHOOPS!!!");
		Debug.Assert(false);
		return;
		m_Coordinates.Remove(coordinate);
		if (CheckHealedPiece(coordinate))
		{
			for (int i = 0; i < m_Coordinates.Count; ++i)
			{
				var co = m_Coordinates[i];
				if (co != coordinate)
				{
					m_Grid.m_Coordinates.Remove(co);
					co.Heal(true);
				}
			}

			m_Coordinates.Clear();
			OnPieceHealed();
		}

		//PopulateCoords(m_Coordinates);
		RedecorateCords();
		OnCoordinatesChanged?.Invoke(this);
	}

	protected virtual bool CheckHealedPiece(Coordinate coordinate)
	{
		return m_Coordinates.Count == 0;
	}

	protected virtual void OnPieceHealed()
	{
		m_Grid.m_Pieces.Remove(this);
	}
}


public class PollutionPiece : GridPiece
{
	public PollutionPiece(WorldGrid grid, Shape shape, Vector2Int position, GridTileBuilder.TileType tileType)
		: base(grid, shape, position, tileType)
	{
		m_PlacedDirection = Direction.North;
		//m_Grid.m_Polluter.m_PollutionCoordinates.AddRange(m_Coordinates);
		//m_Grid.RepresentCoordianates(this);
		//PopulateCoords(m_Grid.m_Polluter.m_PollutionCoordinates);
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

	}

	//protected override Coordinate BuildCoordinate(Vector2 position)
	//{
	//	return new BlockingCoordinate(this, position);
	//}

	protected override bool CheckHealedPiece(Coordinate coordinate)
	{
		return false;
	}

	protected override void OnPieceHealed()
	{
		//do nothing 
	}
}

public class ToxicPiece : PollutionPiece
{
	public float m_ExpansionTime = 9999.0f;
	private float m_LastExpansion = -1f;

	List<Vector2Int> m_CurrentExpansion = new List<Vector2Int>();

	public ToxicPiece(WorldGrid grid, Shape shape, Vector2Int position)
		: base(grid, shape, position, GridTileBuilder.TileType.toxic)
	{
		m_ToxicLevel = GridTileBuilder.ToxicLevel.pool;
		m_ExpansionTime = grid.m_Polluter.m_PollutionExpansionTime;
		// want random offset for each pollution
		//m_LastExpansion = Random.Range(Time.time, Time.time + (m_ExpansionTime / 2));
		var variation = grid.m_Polluter.m_PollutionExpansionTimeVariation;
		m_LastExpansion = Time.time + Random.Range(m_ExpansionTime - variation, m_ExpansionTime + variation);

		m_PlacedDirection = Direction.North;
		//m_Grid.m_Polluter.m_PollutionCoordinates.AddRange(m_Coordinates);
		//m_Grid.RepresentCoordianates(this);
		//PopulateCoords(m_Grid.m_Polluter.m_PollutionCoordinates);
		RedecorateCords();

        OnCoordinatesChanged += ToxicPiece_OnCoordinatesChanged;
	}

    private void ToxicPiece_OnCoordinatesChanged(GridPiece piece)
    {
		m_Coordinates.ForEach(coord =>
		{
			coord.OnCoordinateTypeChanged += Coord_OnCoordinateTypeChanged;
		});
    }

	private void Coord_OnCoordinateTypeChanged(Coordinate coordinate, GridTileBuilder.TileType previous_type, GridTileBuilder.ToxicLevel previous_level)
    {
		if (previous_type == GridTileBuilder.TileType.toxic &&
			coordinate.Type != GridTileBuilder.TileType.toxic)
		{
			coordinate.OnCoordinateTypeChanged -= Coord_OnCoordinateTypeChanged;
			m_Coordinates.Remove(coordinate);
		}

		// TODO: Fix this pool to healable_pool
		if (previous_level == GridTileBuilder.ToxicLevel.pool &&
			coordinate.ToxicLevel == GridTileBuilder.ToxicLevel.none)
        {
			var copy = new List<Coordinate>(m_Coordinates);
			m_Coordinates.Clear();
			copy.ForEach(coord =>
			{
				coord.SetCoordType(GridTileBuilder.TileType.floor, GridTileBuilder.ToxicLevel.none);
			});
			m_Grid.m_Polluter.m_Pollution.Remove(this);
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
		}
	}

	private void GenerateExpansion()
	{
		// get empty neighbors and build new neighbor there...
		m_CurrentExpansion = new List<Vector2Int>();
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].AppendEmptyNeighbors(ref m_CurrentExpansion);
		}
	}

	private void Expand()
	{
		if (m_CurrentExpansion.Count == 0)
			GenerateExpansion();

		List<Vector2Int> coordsToGrow = new List<Vector2Int>();
		// only do one for now...
		int randomExpand = Random.Range(0, m_CurrentExpansion.Count);
		var expansion = m_CurrentExpansion[randomExpand];
		m_CurrentExpansion.Remove(expansion);
		coordsToGrow.Add(expansion);

		List<Coordinate> newCoords = coordsToGrow.Select(vec => m_Grid.GetCoordinate(vec.x, vec.y)).ToList();
		m_Grid.m_Polluter.m_PollutionCoordinates.AddRange(newCoords);
		List<Vector2> gridNeighborPositions = new List<Vector2>();
		for (int i = 0; i < coordsToGrow.Count; ++i)
		{
			gridNeighborPositions.Add(/*m_PlacedPosition +*/ coordsToGrow[i]);
		}


		for (int i = 0; i < m_Grid.m_Coordinates.Count; ++i)
		{
			var expanded = m_Grid.m_Coordinates[i];
			if (gridNeighborPositions.Contains(expanded.GridPosition()))
			{
				// player consumed by lava
				if (expanded.m_PopulatedPlayer != null)
					expanded.m_PopulatedPlayer.Die();

				// heal any bridge piece neighbors (removes them from the board)...
				expanded.Heal();
			}
		}

		//PopulateCoords(m_Grid.m_Polluter.m_PollutionCoordinates);
		RedecorateCords();
		for (int i = 0; i < newCoords.Count; ++i)
		{
			m_Coordinates.Add(newCoords[i]);
			newCoords[i].SetCoordType(m_TileType, GridTileBuilder.ToxicLevel.small_spill);//toxicity.Count > i ? toxicity[i] : GridTileBuilder.ToxicLevel.none;
		}
		m_Coordinates.ForEach(coord =>
		{
			if (coord.ToxicLevel != GridTileBuilder.ToxicLevel.pool &&
				coord.ToxicLevel != GridTileBuilder.ToxicLevel.healable_pool)
			{
				coord.SetCoordType(coord.Type, coord.CanBeHealed() ? GridTileBuilder.ToxicLevel.small_spill : GridTileBuilder.ToxicLevel.big_spill);
			}
		});

		SignalCoordsChanged();
		//m_Grid.UpdateTileRepresentation(this);
	}

	//protected override Coordinate BuildCoordinate(Vector2 position)
	//{
	//	return new PollutionCoordinate(this, position);
	//}

	protected override bool CheckHealedPiece(Coordinate coordinate)
	{
		return coordinate.m_Position == Vector2.zero;
	}

	protected override void OnPieceHealed()
	{
		m_Grid.m_Polluter.m_Pollution.Remove(this);
	}

}

public class PreviewPlacement
{
	List<CoordinateRepresentation> m_Reps;

	public PreviewPlacement(List<CoordinateRepresentation> reps)
	{
		m_Reps = reps;
		m_Reps.ForEach((CoordinateRepresentation rep) =>
		{
			rep.SetColor(Color.white);
			rep.Offset(0.2f);
		});
	}

	public void Clear()
	{
		//m_Reps.ForEach((CoordinateRepresentation rep) => GameObject.Destroy(rep.gameObject));
		m_Reps.ForEach((CoordinateRepresentation rep) =>
		{
			rep.SetColor(Color.cyan);
			rep.Offset(0.0f);
		});
	}
}
