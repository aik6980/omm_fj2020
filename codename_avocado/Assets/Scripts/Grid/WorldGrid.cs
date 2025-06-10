using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T> fn)
    {
        if (enumerable != null)
        {
            var enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                fn(enumerator.Current);
            }
        }
    }
}

public class WorldGrid : MonoBehaviour
{
    public GameState m_GameState;
    public GameObject m_CoordinatePrefab;
    public List<Coordinate> m_Coordinates = new List<Coordinate>();
    public Coordinate m_start_coordinate = null;
    public Coordinate m_exit_coordinate = null;

    public int m_Distance = 20;
    private IGridPolluter m_Polluter = null;

    public GridTileBuilder m_GridTileBuilder;

    public event System.Action<IEnumerable<Coordinate>> OnLevelLoaded;
    public event System.Action<IEnumerable<Coordinate>> OnLevelReady;

    //private LevelReader.LevelData		m_level_data;
    private LevelDataObject             m_level_data;
    public Coordinate[,]				m_coord_grid;
    public CoordinateRepresentation[,]	m_coord_grid_representation;
    private GameObject					m_Environment;
    private GameObject					m_EnvironmentPostProcess;

    public TMPro.TMP_Text				m_Levelname;
    public bool m_LevelReady;

    public event System.Action<Coordinate, GridTileBuilder.TileType> OnCoordinateTypeChanged;

    public TutorialUiController tutorialControllerUi;
    public TutorialDataPart[] tutorialPartsy;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Time.timeScale *= 10.0f;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Time.timeScale = 1.0f;
        }

        if (m_Polluter != null)
        {
            m_Polluter.TickPollution();
        }
    }

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
        
        var post_process = Resources.Load<GameObject>(string.Format("Environments/{0}", sky_name));
        if (post_process != null)
            m_EnvironmentPostProcess = Instantiate(post_process);

        m_coord_grid_representation?.ForEach((x, y, coord_rep) => Destroy(coord_rep?.gameObject));

        Debug.Log(string.Format("Levels/Lv{0}_Queue", level_num));
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
    }

    public List<Coordinate> GetCoordinatesForShape(Vector2Int origin, Direction dir, List<Vector2Int> positions)
    {
        List<Coordinate> newCoordinates = new List<Coordinate>();
        for (int i = 0; i < positions.Count; ++i)
        {
            var transformed_position = Shape.Faceto(positions[i], dir);
            var newCoord = GetCoordinate(origin + transformed_position);
            if (newCoord != null)
                newCoordinates.Add(newCoord);
        }

        return newCoordinates;
    }

    public virtual void Place(Vector2Int position, Direction direction, Shape shape, GridTileBuilder.TileType tileType)
    {
        IEnumerator ConfigureCoord()
        {
            var coordinates = GetCoordinatesForShape(position, direction, shape.Coordinates());
            var enumerator = coordinates.GetEnumerator();
            yield return new WaitForSeconds(1.0f);
            while (enumerator.MoveNext())
            {
                yield return new WaitForSeconds(0.2f);
                SetCoordType(enumerator.Current, tileType);
            }
        }

        StartCoroutine(ConfigureCoord());
    }

    private void Awake()
    {
        m_LevelReady = false;
        LaunchGameScript ls = LaunchGameScript.singleton;
        LoadNextLevel(ls.levelToLoad);
    }

    public CoordinateRepresentation GetCoordinateRepresentation(Coordinate coord)
    {
        return m_coord_grid_representation[coord.m_Position.x, coord.m_Position.y];
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

    public List<CoordinateRepresentation> UpdateTileRepresentation(GridPiece piece)
    {
        IEnumerator ConfigureCoords(List<Coordinate> coords)
        {
            yield return new WaitForSeconds(1.8f);
            foreach (var coord in coords)
            {
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

    public void LoadNextLevel(int levelToLoad)
    {
        Debug.Log($"LoadNextLevel ({levelToLoad})");
        m_LevelReady = false;

        var levelLoader = GetComponent<ILevelLoader>();
        m_level_data = levelLoader.LoadLevel(this, levelToLoad);
        if (m_level_data == null)
        {
            m_GameState.Win();
            return;
        }

        m_Polluter = new TimedGridPolluter(this, m_level_data.ToxicSpreadTime, m_level_data.ToxicSpreadTimeVariation);
        m_Levelname.text = m_level_data.Name;

        InitialiseGrid(levelToLoad, m_level_data.Tiles.Dimensions, m_level_data.EnvironmentName, new Vector3(m_level_data.EnvironmentOffsetX, m_level_data.EnvironmentOffsetY, m_level_data.EnvironmentOffsetZ), m_level_data.SkyName);

        m_Polluter.Clear();
        m_Coordinates.Clear();
        m_coord_grid = new Coordinate[m_level_data.Tiles.Dimensions.x, m_level_data.Tiles.Dimensions.y];
        m_coord_grid_representation = new CoordinateRepresentation[m_level_data.Tiles.Dimensions.x, m_level_data.Tiles.Dimensions.y];

        List<Coordinate> toxic_sources = new List<Coordinate>();
        for (int row = 0; row < m_level_data.Tiles.Dimensions.y; ++row)
        {
            for (int column = 0; column < m_level_data.Tiles.Dimensions.x; ++column)
            {
                var coord = new Coordinate(this, new Vector2Int(column, (m_level_data.Tiles.Dimensions.y - 1) - row), m_level_data.Tiles[row, column]);
                m_Coordinates.Add(coord);
                m_coord_grid[column, (m_level_data.Tiles.Dimensions.y - 1) - row] = coord;

                switch (m_level_data.Tiles[row, column])
                {
                    case GridTileBuilder.TileType.start: m_start_coordinate = coord; break;
                    case GridTileBuilder.TileType.exit: m_exit_coordinate = coord; break;
                    case GridTileBuilder.TileType.toxic_pool: toxic_sources.Add(coord); break;
                }
            }
        }

        m_Polluter.AddToxicPools(toxic_sources.Select(c => new ToxicPiece(this, new Shape(), c.m_Position, m_level_data.MaxSpreadDistance)).ToList());

        //m_level_data = GetComponent<ILevelLoader>().LoadLevel(this);
        //if (m_level_data == null)
        //{
        //    m_GameState.Win();
        //    return;
        //}
        //
        //m_Polluter = new TimedGridPolluter(this, m_level_data.Config.ToxicSpreadTime, m_level_data.Config.ToxicSpreadTimeVariation);
        //m_Levelname.text = m_level_data.Config.Name;
        //
        //InitialiseGrid(m_level_data.levelNumber, m_level_data.Dimension, m_level_data.Config.EnvironmentName, new Vector3(m_level_data.Config.EnvironmentOffsetX, m_level_data.Config.EnvironmentOffsetY, m_level_data.Config.EnvironmentOffsetZ), m_level_data.Config.SkyName);
        //
        //m_Polluter.Clear();
        //m_Coordinates.Clear();
        //
        //// Populate the Coordinates, starting with the start position and ending with the end position.
        //m_Coordinates.Add(new Coordinate(this, m_level_data.Start.ToVector2Int(), GridTileBuilder.TileType.start));
        //
        //m_level_data.ToxicSource.ForEach(c => m_Coordinates.Add(new(this, c.ToVector2Int(), GridTileBuilder.TileType.toxic_pool)));
        //m_level_data.Block.ForEach(c => m_Coordinates.Add(new(this, c.ToVector2Int(), GridTileBuilder.TileType.obstacle)));
        //m_level_data.Solid.ForEach(c => m_Coordinates.Add(new(this, c.ToVector2Int(), GridTileBuilder.TileType.grass)));
        //
        //m_Coordinates.Add(new Coordinate(this, m_level_data.End.ToVector2Int(), GridTileBuilder.TileType.exit));
        //
        //// Populate the coordinate grid from the coordinates, then add any remaining floor.
        //m_coord_grid = new Coordinate[m_level_data.Dimension.x, m_level_data.Dimension.y];
        //m_Coordinates.ForEach(c => m_coord_grid[c.m_Position.x, c.m_Position.y] = c);
        //m_coord_grid.ForEach((x, y, coord) => m_coord_grid[x, y] = m_coord_grid[x, y] ?? new Coordinate(this, new(x, y), GridTileBuilder.TileType.floor));
        //
        //// Populate the game tiles from the coordinates.
        //m_coord_grid_representation = new CoordinateRepresentation[m_level_data.Dimension.x, m_level_data.Dimension.y];
        //
        //// Update the polluter.
        //m_Polluter.AddToxicPools(m_level_data.ToxicSource.Select(c => new ToxicPiece(this, new Shape(), c.ToVector2Int(), m_level_data.Config.MaxSpreadDistance)).ToList()/*m_world_data.toxic_pool_pieces*/);

        OnLevelLoaded?.Invoke(/*m_world_data.start_piece.Coordinates*/null);
        StartCoroutine(TileAnimation(true));
    }

    private IEnumerator TileAnimation(bool forwards)
    {
        IEnumerator AnimateOne(int x, int y, bool fwds)
        {
            m_coord_grid_representation[x, y] = m_GridTileBuilder.InstantiateTile(this, m_coord_grid[x, y]);

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

        tutorialControllerUi.SetupUi(tutorialPartsy);
        tutorialControllerUi.ShowUi();

        for (int y = 0; y < m_coord_grid.GetLength(1); ++y)
        {
            StartCoroutine(AnimateRow(y, forwards));
                yield return new WaitForSeconds(0.06f);
        }


        m_LevelReady = true;
        OnLevelReady?.Invoke(/*m_world_data.start_piece.Coordinates*/null);
    }


    public bool IsFinalCoordinate(Coordinate coordinate)
    {
        //return coordinate.m_Position == m_level_data.End.ToVector2Int();//m_FinalPiece.Coordinates.Contains(coordinate);
        return coordinate.Type == GridTileBuilder.TileType.exit;
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
            case GridTileBuilder.TileType.toxic_pool:
                return isSuperPowered ? true : CanBeHealed_Toxic(coord);

        }

        Debug.Assert(false);
        return false;
    }

    public void HealPositions(List<Coordinate> coordinates) => m_Polluter?.HealPositions(coordinates);
    public bool HasAnyPollution() => m_Polluter?.HasAnyPollution() ?? false;

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

        return GetOrthogonalNeighbours(coord).Any(c => c == null || !c.IsPolluted());
    }

    public void SetCoordType(Coordinate coord, GridTileBuilder.TileType type)
    {
        if (coord.Type == GridTileBuilder.TileType.start ||
            coord.Type == GridTileBuilder.TileType.exit)
        {
            return;
        }

        if (coord.Type != type)
        {
            GridTileBuilder.TileType previous_type = coord.Type;
            coord.Type = type;
            OnCoordinateTypeChanged?.Invoke(coord, previous_type);

            // Check all of the neighbours and their neighbours updating the representations as needed (this fixes the
            // neighbours which need to connect to new deep toxic tiles etc.)
            List<Coordinate> changedCoordinates = new() { coord };
            for (int i = 0; i < changedCoordinates.Count; ++i)
            {
                var nextCoord = changedCoordinates[i];
                if (m_coord_grid_representation[nextCoord.m_Position.x, nextCoord.m_Position.y]?.UpdateRepresentation() ?? false)
                {
                    foreach (var neighbour in GetOrthogonalNeighbours(nextCoord).Concat(GetDiagonalNeighbours(nextCoord)).Where(c => c != null))
                    {
                        if (!changedCoordinates.Contains(neighbour))
                        {
                            changedCoordinates.Add(neighbour);
                        }
                    }
                }

            }

        }
    }

    public GridTileBuilder.ToxicLevel GetToxicLevel(Coordinate coord)
    {
        if (coord.IsPolluted())
        {
            return GetOrthogonalNeighbours(coord)
                .All(n => n.IsPolluted()) ? GridTileBuilder.ToxicLevel.deep : GridTileBuilder.ToxicLevel.shallow;
        }

        return GridTileBuilder.ToxicLevel.none;
    }

    public IEnumerable<Coordinate> GetOrthogonalNeighbours(Coordinate coord)
    {
        yield return GetCoordinate(coord.m_Position.x, coord.m_Position.y + 1); // North
        yield return GetCoordinate(coord.m_Position.x + 1, coord.m_Position.y); // East
        yield return GetCoordinate(coord.m_Position.x, coord.m_Position.y - 1); // South
        yield return GetCoordinate(coord.m_Position.x - 1, coord.m_Position.y); // West
    }

    public IEnumerable<Coordinate> GetDiagonalNeighbours(Coordinate coord)
    {
        yield return GetCoordinate(coord.m_Position.x + 1, coord.m_Position.y + 1); // North-East
        yield return GetCoordinate(coord.m_Position.x + 1, coord.m_Position.y - 1);	// South-East
        yield return GetCoordinate(coord.m_Position.x - 1, coord.m_Position.y - 1);	// South-West
        yield return GetCoordinate(coord.m_Position.x - 1, coord.m_Position.y + 1);	// North-West
    }
}
