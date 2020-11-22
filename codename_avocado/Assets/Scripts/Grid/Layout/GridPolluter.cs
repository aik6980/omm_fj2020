using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPolluter : MonoBehaviour
{
	public WorldGrid m_Grid;
	public List<Coordinate> m_PollutionCoordinates = new List<Coordinate>();
	public List<PollutionPiece> m_Pollution = new List<PollutionPiece>();

	private int m_ObstacleRange = 3;
	private int m_ObstacleCount = 3;

	public float m_PollutionExpansionTime = 3f;

	private void Update()
	{
		CheckExpandPollution();
	}

	public void CheckExpandPollution()
	{
		m_Pollution.ForEach((PollutionPiece piece) =>
		{
			piece.TickPollution();
		});
	}

	public void InitialPollution()
	{
		// only one volcano for now...
		AddVolcano();


		for (int i = 0; i < m_ObstacleCount; ++i)
		{
			AddBlock();
		}
	}

	public bool Polluted(Vector2 coPosition)
	{
		for (int p = 0; p < m_PollutionCoordinates.Count; ++p)
		{
			var pollution = m_PollutionCoordinates[p];
			if (pollution.GridPosition() == coPosition && !pollution.CanBeHealed())
				return true;
		}
		return false;
	}

	private void HealPosition(Vector2 coPosition, ref List<Coordinate> coordsToHeal)
	{
		for (int p = 0; p < m_PollutionCoordinates.Count; ++p)
		{
			var pollutionCoord = m_PollutionCoordinates[p];
			if (pollutionCoord.GridPosition() == coPosition && pollutionCoord.CanBeHealed())
			{
				coordsToHeal.Add(pollutionCoord);
			}
		}
	}

	public bool HandleHealing()
	{
		List<Coordinate> coordsToHeal = new List<Coordinate>();
		for (int i = 0; i < m_Grid.m_Coordinates.Count; ++i)
		{
			var coPosition = m_Grid.m_Coordinates[i].GridPosition();
			HealPosition(coPosition, ref coordsToHeal);
		}

		coordsToHeal.ForEach((Coordinate c) => c.Heal(false));
		return true;
	}

	public void AddVolcano()
	{
		var pollutionPiece = new PollutionPiece(m_Grid, new Volcano(), RandomPollutant(), GridTileBuilder.TileType.toxic);
		m_Pollution.Add(pollutionPiece);
	}

	public void AddBlock()
	{
		var pollutant = RandomPollutant();
		while (m_PollutionCoordinates.Find((Coordinate c) => c.GridPosition() == pollutant) != null)
		{
			pollutant = RandomPollutant();
		}

		var pollutionPiece = new BlockingPiece(m_Grid, new Shape(), pollutant);
		m_Pollution.Add(pollutionPiece);
		m_PollutionCoordinates.AddRange(pollutionPiece.m_Coordinates);
	}

	private Vector2 RandomPollutant()
	{
		int randomX = Random.Range(0, m_ObstacleRange);
		int randomY = Random.Range(4, m_Grid.m_Distance - 4);
		var position = new Vector2(randomX, randomY);
		return position;
	}

	public void AddBlocks(List<PollutionPiece> pieces)
	{
		var pollutant = RandomPollutant();
		while (m_PollutionCoordinates.Find((Coordinate c) => c.GridPosition() == pollutant) != null)
		{
			pollutant = RandomPollutant();
		}

		pieces.ForEach(piece =>
		{
			m_Pollution.Add(piece);
			m_PollutionCoordinates.AddRange(piece.m_Coordinates);
		});
	}

	public void AddVolcanoes(List<PollutionPiece> pieces)
	{
		pieces.ForEach(piece =>
		{
			m_Pollution.Add(piece);
			//m_PollutionCoordinates.AddRange(piece.m_Coordinates);
		});
	}
}