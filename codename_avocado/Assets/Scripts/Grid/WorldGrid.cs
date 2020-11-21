using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public GameObject m_CoordinatePrefab;
	public List<GridPiece> m_Pieces = new List<GridPiece>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	public List<BlockingCoordinate> m_Obstacles = new List<BlockingCoordinate>();

	public int m_Distance = 10;

	public int m_ObstacleRange = 3;
	public int m_ObstacleCount = 5;

	//public Vector
	public GridPiece m_FinalPiece;

	private void Awake()
	{
		var startPiece = GridPiece.GeneratePiece(this, new Square());
		startPiece.Place(Vector2.zero);

		m_FinalPiece = GridPiece.GeneratePiece(this, new Square());
		m_FinalPiece.Place(new Vector2(0, m_Distance));

		for (int i = 0; i < m_ObstacleCount; ++i)
		{
			AddObstacle();
		}
	}

	private void AddObstacle()
	{
		int randomX = Random.Range(0, m_ObstacleRange);
		int randomY = Random.Range(2, m_Distance - 2);
		var newObstacle = new BlockingCoordinate(new Vector2(randomX, randomY));
		var coordinateRepGO = GameObject.Instantiate(m_CoordinatePrefab);
		var rep = coordinateRepGO.GetComponent<CoordinateRepresentation>();
		rep.Configure(newObstacle);
		rep.SetColor(Color.red);
		m_Obstacles.Add(newObstacle);
	}

	public bool IsFinalCoordinate(Coordinate coordinate)
	{
		return m_FinalPiece.m_Coordinates.Contains(coordinate);
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

	public bool SupportsPlacement(Vector2 placement, GridPiece piece)
	{
		for (int i = 0; i < piece.m_Coordinates.Count; ++i)
		{
			var coPosition = piece.m_Coordinates[i].m_Position + placement;
			for (int o = 0; o < m_Obstacles.Count; ++o)
			{
				var obstacle = m_Obstacles[o];
				if (obstacle.GridPosition() == coPosition)
					return false;
			}
		}

		return true;
	}

	public List<CoordinateRepresentation> RepresentPiece(GridPiece newPiece)
	{
		List<CoordinateRepresentation> reps = new List<CoordinateRepresentation>();
		newPiece.m_Coordinates.ForEach((Coordinate coordinate) =>
		{
			var coordinateRepGO = GameObject.Instantiate(m_CoordinatePrefab);
			var rep = coordinateRepGO.GetComponent<CoordinateRepresentation>();
			reps.Add(rep);
			rep.Configure(coordinate);
		});

		return reps;
	}

	public void LinkPiece(GridPiece piece)
	{
		m_Pieces.Add(piece);
		m_Coordinates.AddRange(piece.m_Coordinates);
		m_Pieces.ForEach((GridPiece p) =>
		{
			p.PopulateCoords(this);
		});
	}

}
