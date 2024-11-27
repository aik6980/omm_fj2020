using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public class WorldData
    {
		public List<GridPiece>		floor_pieces;
		public GridPiece			start_piece;
		public GridPiece			end_piece;
		public List<GridPiece>		island_pieces;
		public List<PollutionPiece> volcano_pieces;
		public List<PollutionPiece>	block_pieces;
	}

	public GameState m_GameState;
	public GameObject m_CoordinatePrefab;
	public List<GridPiece> m_Pieces = new List<GridPiece>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	public int m_Distance = 20;
	public GridPolluter m_Polluter;

	public GridTileBuilder m_GridTileBuilder;

	//public Vector
	public GridPiece m_FinalPiece;

	public event System.Action<IEnumerable<Coordinate>> OnLevelLoaded;
	public event System.Action<IEnumerable<Coordinate>> OnLevelReady;

	private WorldData					m_world_data;
	public Coordinate[,]				m_coord_grid;
	public CoordinateRepresentation[,]	m_coord_grid_representation;
	private GameObject					m_Environment;
	private GameObject					m_EnvironmentPostProcess;

	public TMPro.TMP_Text				m_Levelname;
    public bool m_LevelReady;

    public void InitialiseGrid(int level_num, Vector2Int dim, string environment_name, Vector3 env_offset, string sky_box_name)
    {
		if (m_Environment != null)
        {
			Destroy(m_Environment);
        }

		if (m_EnvironmentPostProcess != null)
        {
			Destroy(m_EnvironmentPostProcess);
        }

		GameObject env_prefab = Resources.Load<GameObject>(string.Format("Environments/{0}", environment_name));
		m_Environment = Instantiate(env_prefab);
		m_Environment.transform.position += env_offset;


		string sky_name = string.IsNullOrEmpty(sky_box_name) ? "Skybox01_day" : sky_box_name;
		//var sky = Resources.Load<Material>(string.Format("Environments/{0}", sky_name));
		//RenderSettings.skybox = sky;
		
		var post_process = Resources.Load<GameObject>(string.Format("Environments/{0}", sky_name));
		if (post_process != null)
			m_EnvironmentPostProcess = Instantiate(post_process);

		if (m_coord_grid_representation != null)
        {
			m_coord_grid_representation.ForEach((x, y, coord_rep) => Destroy(coord_rep.gameObject));
		}

		m_coord_grid = new Coordinate[dim.x, dim.y];
		m_coord_grid_representation = new CoordinateRepresentation[dim.x, dim.y];
		m_coord_grid.ForEach((x, y, coord) =>
		{
			m_coord_grid[x, y] = new Coordinate(this, new Vector2Int(x, y), GridTileBuilder.TileType.floor);
		});
		OnCoordinateTypeChanged += WorldGrid_OnCoordinateTypeChanged;

		var conveyorQueue = Resources.Load<ManualConveyorQueue>(string.Format("Levels/Lv{0}_Queue", level_num));
        if (conveyorQueue != null)
        {
            NextShapeQueue.Instance.conveyorQueue = conveyorQueue;
            NextShapeQueue.Instance.randomized = false;
            NextShapeQueue.Instance.loopQueue = conveyorQueue.loopQueue;

            NextShapeQueue.Instance.Awake();
        }
        else
        {
			//TODO: Reset
            NextShapeQueue.Instance.conveyorQueue = null;
            NextShapeQueue.Instance.loopQueue = false;
            NextShapeQueue.Instance.randomized = true;

			NextShapeQueue.Instance.Awake();
        }

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
		if (coord.Type == GridTileBuilder.TileType.toxic)
		{
			m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y]?.Configure(coord, m_GridTileBuilder);
		}
		else if (coord.Type == GridTileBuilder.TileType.floor)
		{
			IEnumerator ConfigureCoord()
			{
				yield return new WaitForSeconds(2.2f);
				m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y].Configure(coord, m_GridTileBuilder);
			}

			StartCoroutine(ConfigureCoord());
		}
	}

    private void Awake()
	{
		m_LevelReady = false;
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

	public Coordinate GetAdjacentCoordinate(Vector2Int src, Direction dir)
	{
		var dest = OffsetDirection(src, dir);
		return GetCoordinate(dest);
	}

	//public void BuildTileRepresentation()
	//{
	//	//m_coord_grid_representation = new CoordinateRepresentation[m_coord_grid.GetLength(0), m_coord_grid.GetLength(1)];
	//	m_coord_grid.ForEach((x, y, coord) =>
	//	{
	//		m_coord_grid_representation[x, y] = m_GridTileBuilder.InstantiateTile(coord);
	//	});
	//}

	public List<CoordinateRepresentation> UpdateTileRepresentationNow(GridPiece piece)
	{
		List<CoordinateRepresentation> reps = new List<CoordinateRepresentation>();
		if (!m_LevelReady)
			return reps;

		foreach (var coord in piece.Coordinates)
		{
			m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y].Configure(coord, m_GridTileBuilder);
			reps.Add(m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y]);
		}

		return reps;
	}

	public List<CoordinateRepresentation> UpdateTileRepresentation(GridPiece piece)
	{
		IEnumerator ConfigureCoords(List<Coordinate> coords)
        {
			yield return new WaitForSeconds(1.8f);
			foreach (var coord in coords)
			{
				m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y].Configure(coord, m_GridTileBuilder);
				//reps.Add(m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y]);
				yield return new WaitForSeconds(0.18f);
			}
		}

		List<CoordinateRepresentation> reps = new List<CoordinateRepresentation>();
		if (!m_LevelReady)
			return reps;

		foreach (var coord in piece.Coordinates)
        {
		//	m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y].Configure(coord, m_GridTileBuilder);
			reps.Add(m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y]);
        }

		StartCoroutine(ConfigureCoords(piece.Coordinates.ToList()));
		return reps;
	}

    public void LoadNextLevel()
    {
		//Debug.Log("LoadNextLevel");
		m_LevelReady = false;

		m_world_data = GetComponent<ILevelLoader>().LoadLevel(this);
		if (m_world_data == null)
        {
			m_GameState.Win();
			return;
		}

		m_FinalPiece = m_world_data.end_piece;

		m_Polluter.Reset();
		m_Polluter.AddVolcanoes(m_world_data.volcano_pieces);
		m_Polluter.AddBlocks(m_world_data.block_pieces);

		m_Coordinates.Clear();
		m_Coordinates.AddRange(m_world_data.start_piece.Coordinates);
		for (int y = 0; y < m_coord_grid.GetLength(1); ++y)
		{
			for (int x = 0; x < m_coord_grid.GetLength(0); ++x)
			{
				if (!m_Coordinates.Contains(m_coord_grid[x, y]))
					m_Coordinates.Add(m_coord_grid[x, y]);
			}
		}

		//BuildTileRepresentation();
		OnLevelLoaded?.Invoke(m_world_data.start_piece.Coordinates);
		StartCoroutine(TileAnimation(true));
	}

	private IEnumerator TileAnimation(bool forwards)
    {
		IEnumerator AnimateOne(int x, int y, bool fwds)
		{
			m_coord_grid_representation[x, y] = m_GridTileBuilder.InstantiateTile(m_coord_grid[x, y]);
			Vector3 final_pos = m_coord_grid_representation[x, y].transform.position;
			Vector3 start_pos = final_pos - new Vector3(0f, 10f, 10f);
			float a = 0.0f;
			while (a < 1.0f)
            {
				a = Mathf.Clamp(a + (Time.deltaTime * 1.8f), 0.0f, 1.0f);
				m_coord_grid_representation[x, y].transform.position =
					fwds ? Vector3.Slerp(start_pos, final_pos, a) : Vector3.Slerp(final_pos, start_pos, a);
				yield return null;
            }
		}

		IEnumerator AnimateRow(int y, bool fwds)
        {
			for (int x = 0; x < m_coord_grid.GetLength(0); ++x)
			{
				StartCoroutine(AnimateOne(x, y, forwards));
				yield return new WaitForSeconds(0.02f);
			}
        }

		for (int y = 0; y < m_coord_grid.GetLength(1); ++y)
		{
			StartCoroutine(AnimateRow(y, forwards));
				yield return new WaitForSeconds(0.06f);
		}


		m_LevelReady = true;
		OnLevelReady?.Invoke(m_world_data.start_piece.Coordinates);
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
		return piece.Coordinates.All(coord => CanBeHealed(coord, piece.m_SuperPiece));

		//foreach (var coord in piece.Coordinates)
		//{
		//	if (m_Polluter.Polluted(coord.m_Position))
		//		return false;
		//}
		//
		//return true;
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

	public bool TryMove(Coordinate from, Vector2 directionVec, out Coordinate nextCoordinate)
	{
		List<Direction> directions = new List<Direction>();
		if (directionVec.y > 0.1f)
			directions.Add(Direction.North);
		else if (directionVec.y < -0.1f)
			directions.Add(Direction.South);

		if (directionVec.x > 0.1f)
			directions.Add(Direction.East);
		else if (directionVec.x < -0.1f)
			directions.Add(Direction.West);

		// Sort the directions vector based on which has the larger magnitude.
		// i.e. the player is pushing more in that direction than any other.
		if (Mathf.Abs(directionVec.x) > Mathf.Abs(directionVec.y))
		{
			directions.Reverse();
		}

		if (directions.Count == 1)
		{
			nextCoordinate = GetAdjacentCoordinate(from.m_Position, directions[0]);
			if (nextCoordinate != null && nextCoordinate.IsPassable())
			{
				return true;
			}
		}
		else if (directions.Count == 2)
		{
			var firstCoord = GetAdjacentCoordinate(from.m_Position, directions[0]);
			if (firstCoord != null && firstCoord.IsPassable())
			{
				var nsewCoord = GetAdjacentCoordinate(firstCoord.m_Position, directions[1]);
				if (nsewCoord != null && nsewCoord.IsPassable())
				{
					nextCoordinate = nsewCoord;
					return true;
				}
			}

			var secondCoord = GetAdjacentCoordinate(from.m_Position, directions[1]);
			if (secondCoord != null && secondCoord.IsPassable())
			{
				var ewnsCoord = GetAdjacentCoordinate(secondCoord.m_Position, directions[0]);
				if (ewnsCoord != null && ewnsCoord.IsPassable())
				{
					nextCoordinate = ewnsCoord;
					return true;
				}
			}

			// If neither of the above worked use first over second.
			if (firstCoord != null && firstCoord.IsPassable())
			{
				nextCoordinate = firstCoord;
				return true;
			}

			if (secondCoord != null && secondCoord.IsPassable())
			{
				nextCoordinate = secondCoord;
				return true;
			}
		}

		nextCoordinate = null;
		return false;
	}

	public bool CanBeHealed(Coordinate coord, bool isSuperPowered)
	{
		switch (coord.Type)
		{
			case GridTileBuilder.TileType.start: // start and exit can be healed but cannot change the representation
			case GridTileBuilder.TileType.exit:
			case GridTileBuilder.TileType.floor:
			case GridTileBuilder.TileType.grass:
				return true;
			case GridTileBuilder.TileType.obstacle:
				return false;
			case GridTileBuilder.TileType.toxic:
				return isSuperPowered ? true : CanBeHealed_Toxic(coord);

		}

		Debug.Assert(false);
		return false;
	}

	bool CanBeHealed_Toxic(Coordinate coord)
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

		for (int i = 0; i < System.Enum.GetValues(typeof(Direction)).Length; ++i)
		{
			var nextCoordinate = GetAdjacentCoordinate(coord.m_Position, (Direction)i);
			if (nextCoordinate == null || nextCoordinate.Type != GridTileBuilder.TileType.toxic)
			{
				return true;
			}
		}

		return false;
	}

	public event System.Action<Coordinate, GridTileBuilder.TileType, GridTileBuilder.ToxicLevel> OnCoordinateTypeChanged;

	public void SetCoordType(Coordinate coord, GridTileBuilder.TileType type, GridTileBuilder.ToxicLevel toxic_level)
	{
		if (coord.Type == GridTileBuilder.TileType.start ||
			coord.Type == GridTileBuilder.TileType.exit)
		{
			return;
		}

		if (coord.Type != type || coord.ToxicLevel != toxic_level)
		{
			GridTileBuilder.TileType previous_type = coord.Type;
			GridTileBuilder.ToxicLevel previous_toxicity = coord.ToxicLevel;
			coord.Type = type;
			coord.ToxicLevel = toxic_level;
			OnCoordinateTypeChanged?.Invoke(coord, previous_type, previous_toxicity);

			for (int i = 0; i < System.Enum.GetValues(typeof(Direction)).Length; ++i)
			{
				Direction d = (Direction)i;
				var nextCoordinate = GetAdjacentCoordinate(coord.m_Position, d);
				if (nextCoordinate != null && nextCoordinate.Type == GridTileBuilder.TileType.toxic)
				{
					if (nextCoordinate.ToxicLevel == GridTileBuilder.ToxicLevel.pool ||
						nextCoordinate.ToxicLevel == GridTileBuilder.ToxicLevel.healable_pool)
					{
						var new_toxicity = CanBeHealed(nextCoordinate, false) ? GridTileBuilder.ToxicLevel.healable_pool : GridTileBuilder.ToxicLevel.pool;
						SetCoordType(nextCoordinate, nextCoordinate.Type, new_toxicity);
					}
					else
					{
						SetCoordType(nextCoordinate, nextCoordinate.Type, CanBeHealed(nextCoordinate, false) ? GridTileBuilder.ToxicLevel.small_spill : GridTileBuilder.ToxicLevel.big_spill);
					}
				}
			}
		}
	}
}
