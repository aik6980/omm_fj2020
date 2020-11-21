using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPolluter : MonoBehaviour
{
	public WorldGrid m_Grid;
	public List<PollutionPiece> m_Pollution = new List<PollutionPiece>();

	private int m_ObstacleRange = 3;
	private int m_ObstacleCount = 1;

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
		for (int i = 0; i < m_ObstacleCount; ++i)
		{
			AddPollution();
		}
	}

	public bool Polluted(Vector2 coPosition)
	{
		for (int p = 0; p < m_Pollution.Count; ++p)
		{
			var pollution = m_Pollution[p];
			for (int o = 0; o < pollution.m_Coordinates.Count; ++o)
			{
				var obstacle = pollution.m_Coordinates[o];
				if (obstacle.GridPosition() == coPosition && !obstacle.CanBeHealed())
					return true;
			}
		}
		return false;
	}

	private void HealPosition(Vector2 coPosition, ref List<Coordinate> coordsToHeal)
	{
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

	public void AddPollution()
	{
		int randomX = Random.Range(0, m_ObstacleRange);
		int randomY = Random.Range(4, m_Grid.m_Distance - 4);
		var pollutionPiece = new PollutionPiece(m_Grid, new Volcano(), new Vector2(randomX, randomY));
		m_Pollution.Add(pollutionPiece);
	}

}
