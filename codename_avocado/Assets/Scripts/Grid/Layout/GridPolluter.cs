using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPolluter : MonoBehaviour
{
	public WorldGrid m_Grid;
	public List<Coordinate> m_PollutionCoordinates = new List<Coordinate>();
	public List<PollutionPiece> m_Pollution = new List<PollutionPiece>();

	private int m_ObstacleRange = 3;

	public float m_PollutionExpansionTime = 9999.0f;
	public float m_PollutionExpansionTimeVariation = 0.0f;

    public void Start()
    {
		StartCoroutine(PlayToxicSFXInterval());
    }

	int GetNumToxicPollution()
    {
		var toxic_count = 0;
		foreach(PollutionPiece p in m_Pollution)
        {
			if(p.m_TileType == GridTileBuilder.TileType.toxic)
            {
				toxic_count++;
            }
        }

		return toxic_count;
    }

	IEnumerator PlayToxicSFXInterval()
	{
		while (true)
		{

			var time_variation = UnityEngine.Random.Range(-1.0f, 1.0f) * AudioManager.GetOrCreateInstance().toxic_sfx_interval_variation;
			var random_interval = AudioManager.GetOrCreateInstance().toxic_sfx_interval + time_variation;

			yield return new WaitForSeconds(random_interval);


			// if no toxic don't play sfx
			if(GetNumToxicPollution() > 0)
            {
				AudioManager.GetOrCreateInstance().PlayToxicSFX();
			}
		}
	}

	private void Update()
	{
		CheckExpandPollution();
	}

	public void CheckExpandPollution()
	{
		var copy = new List<PollutionPiece>(m_Pollution);
		copy.ForEach((PollutionPiece piece) =>
		{
			piece.TickPollution();
		});
	}

	private void HealPosition(Vector2 coPosition, ref List<Coordinate> coordsToHeal)
	{
		for (int p = 0; p < m_PollutionCoordinates.Count; ++p)
		{
			var pollutionCoord = m_PollutionCoordinates[p];
			if (pollutionCoord.GridPosition() == coPosition && m_Grid.CanBeHealed(pollutionCoord, false))
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

		return true;
	}

	private Vector2 RandomPollutant()
	{
		int randomX = Random.Range(0, m_ObstacleRange);
		int randomY = Random.Range(4, m_Grid.m_Distance - 4);
		var position = new Vector2(randomX, randomY);
		return position;
	}

    public void Reset()
    {
		m_Pollution.Clear();
		m_PollutionCoordinates.Clear();
	}

    public void AddObstacles(List<BlockingPiece> pieces)
	{
		var pollutant = RandomPollutant();
		while (m_PollutionCoordinates.Find((Coordinate c) => c.GridPosition() == pollutant) != null)
		{
			pollutant = RandomPollutant();
		}

		pieces.ForEach(piece =>
		{
			m_Pollution.Add(piece);
			m_PollutionCoordinates.AddRange(piece.Coordinates);
		});
	}

	public void AddToxicPools(List<ToxicPiece> pieces)
	{
		pieces.ForEach(piece =>
		{
			m_Pollution.Add(piece);
			m_PollutionCoordinates.AddRange(piece.Coordinates);

			piece.Coordinates.ForEach(coord => m_Grid.SetCoordType(coord, GridTileBuilder.TileType.toxic_pool));
			piece.GenerateExpansion();
			for (int i = 0; i < Random.Range(8, 11); ++i)
				piece.Expand();
		});
	}
}