using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public struct WorldData
    {
		public List<GridPiece>		floor_pieces;
		public GridPiece			start_piece;
		public GridPiece			end_piece;
		public List<GridPiece>		island_pieces;
		public List<PollutionPiece> volcano_pieces;
		public List<PollutionPiece>	block_pieces;
	}

	public GameObject m_CoordinatePrefab;
	public List<GridPiece> m_Pieces = new List<GridPiece>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	public int m_Distance = 20;
	public GridPolluter m_Polluter;

	public GridTileBuilder m_GridTileBuilder;

	//public Vector
	public GridPiece m_FinalPiece;

	public event System.Action<IEnumerable<Coordinate>> OnLevelLoaded;

	private WorldData					m_world_data;
	public Coordinate[,]				m_coord_grid;
	public CoordinateRepresentation[,]	m_coord_grid_representation;
	
	public void InitialiseGrid(Vector2Int dim)
    {
		m_coord_grid = new Coordinate[dim.x, dim.y];
		m_coord_grid_representation = new CoordinateRepresentation[dim.x, dim.y];
		m_coord_grid.ForEach((x, y, coord) =>
		{
			m_coord_grid[x, y] = new Coordinate(this, new Vector2Int(x, y), GridTileBuilder.TileType.floor);
            m_coord_grid[x, y].OnCoordinateTypeChanged += WorldGrid_OnCoordinateTypeChanged;
		});
		//for (int y = 0; y < dim.y; ++y)
		//{
		//	for (int x = 0; x < dim.x; ++x)
		//	{
		//		m_coord_grid[x, y] = new Coordinate(new Vector2Int(x, y), GridTileBuilder.TileType.floor);
		//	}
		//}
	}

    private void WorldGrid_OnCoordinateTypeChanged(Coordinate coord, GridTileBuilder.TileType previous_type, GridTileBuilder.ToxicLevel toxicity)
	{
		m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y]?.Configure(coord, m_GridTileBuilder);
	}

    private void Awake()
	{
		LoadNextLevel();
	}

	public Coordinate GetCoordinate(Vector2 pos)
    {
		return GetCoordinate(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y));
    }

	public Coordinate GetCoordinate(int x, int y)
    {
		if (x < 0 || x >= m_coord_grid.GetLength(0) ||
			y < 0 || y >= m_coord_grid.GetLength(1))
		{
			return null;
		}

		return m_coord_grid[x, y];
	}

	public void BuildTileRepresentation()
	{
		//m_coord_grid_representation = new CoordinateRepresentation[m_coord_grid.GetLength(0), m_coord_grid.GetLength(1)];
		m_coord_grid.ForEach((x, y, coord) =>
		{
			m_coord_grid_representation[x, y] = m_GridTileBuilder.InstantiateTile(coord);
		});
	}

	public List<CoordinateRepresentation> UpdateTileRepresentation(GridPiece piece)
	{
		List<CoordinateRepresentation> reps = new List<CoordinateRepresentation>();
		foreach (var coord in piece.Coordinates)
        {
			m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y].Configure(coord, m_GridTileBuilder);
			reps.Add(m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y]);
        }

		return reps;
	}

    public void LoadNextLevel()
    {
		m_world_data = GetComponent<ILevelLoader>().LoadLevel(this);
		m_FinalPiece = m_world_data.end_piece;

		m_Polluter.AddVolcanoes(m_world_data.volcano_pieces);
		m_Polluter.AddBlocks(m_world_data.block_pieces);

		m_Coordinates.AddRange(m_world_data.start_piece.Coordinates);
		for (int y = 0; y < m_coord_grid.GetLength(1); ++y)
		{
			for (int x = 0; x < m_coord_grid.GetLength(0); ++x)
			{
				if (!m_Coordinates.Contains(m_coord_grid[x, y]))
					m_Coordinates.Add(m_coord_grid[x, y]);
			}
		}

		OnLevelLoaded?.Invoke(m_world_data.start_piece.Coordinates);
		BuildTileRepresentation();
	}

    public List<CoordinateRepresentation> RepresentCoordianates(GridPiece piece)
	{
		List<CoordinateRepresentation> reps = new List<CoordinateRepresentation>();
		//piece.m_Coordinates.ForEach((Coordinate coordinate) =>
		//{
		//	var coordinateRepGO = m_GridTileBuilder.InstantiateTile(piece.m_TileType);
		//	var rep = coordinateRepGO.GetComponent<CoordinateRepresentation>();
		//	reps.Add(rep);
		//	rep.Configure(coordinate);
		//});

		return reps;
	}


	public bool IsFinalCoordinate(Coordinate coordinate)
	{
		return m_FinalPiece.Coordinates.Contains(coordinate);
	}

	public static Vector2 OffsetDirection(Vector2 start, Direction direction)
	{
		switch (direction)
		{
			case Direction.North:
				return new Vector2(start.x, start.y + 1);
			case Direction.South:
				return new Vector2(start.x, start.y - 1);
			case Direction.East:
				return new Vector2(start.x + 1, start.y);
			case Direction.West:
				return new Vector2(start.x - 1, start.y);
			default:
				return start;
		}
	}

	public bool SupportsPlacement(Vector2 placement, GridPiece piece, Direction direction)
	{
		foreach (var coord in piece.Coordinates)
		{
			if (m_Polluter.Polluted(coord.m_Position))
				return false;
		}

		return true;
	}

	public void LinkPiece(GridPiece piece)
	{
		m_Pieces.Add(piece);
		//m_Coordinates.AddRange(piece.m_Coordinates);
		//m_Pieces.ForEach((GridPiece p) =>
		//{
		//	p.PopulateCoords(this.m_Coordinates);
		//});
	}


	public Coordinate GetNextCoordinate(Vector2Int src, Direction dir)
    {
		var dest = OffsetDirection(src, dir);
		return GetCoordinate(dest);
    }
}
