using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public GameObject m_CoordinatePrefab;
	public List<GridPiece> m_Pieces = new List<GridPiece>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	public List<PollutionPiece> m_Pollution = new List<PollutionPiece>();

	private int m_Distance = 20;

	private int m_ObstacleRange = 3;
	private int m_ObstacleCount = 1;

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
			AddPollution();
		}
	}

	private void AddPollution()
	{
		int randomX = Random.Range(0, m_ObstacleRange);
		int randomY = Random.Range(4, m_Distance - 4);
		var pollutionPiece = new PollutionPiece(this, new Volcano(), new Vector2(randomX, randomY));
		m_Pollution.Add(pollutionPiece);
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
			for (int p = 0; p < m_Pollution.Count; ++p)
			{
				var pollution = m_Pollution[p];
				for (int o = 0; o < pollution.m_Coordinates.Count; ++o)
				{
					var obstacle = pollution.m_Coordinates[o];
					if (obstacle.GridPosition() == coPosition && !obstacle.CanBeHealed())
						return false;
				}
			}
		}

		return true;
	}

	public bool HandleHealing()
	{
		List<Coordinate> coordsToHeal = new List<Coordinate>();
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			var coPosition = m_Coordinates[i].GridPosition();
			for (int p = 0; p < m_Pollution.Count; ++p)
			{
				var pollution = m_Pollution[p];
				for (int o = 0; o < pollution.m_Coordinates.Count; ++o)
				{
					var pollutionCoord = pollution.m_Coordinates[o];
					if (pollutionCoord.GridPosition() == coPosition && pollutionCoord.CanBeHealed())
					{
						coordsToHeal.Add(pollutionCoord);
					}
				}
			}
		}

		coordsToHeal.ForEach((Coordinate c) => c.Heal(false));
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
			p.PopulateCoords(this.m_Coordinates);
		});
	}

}
