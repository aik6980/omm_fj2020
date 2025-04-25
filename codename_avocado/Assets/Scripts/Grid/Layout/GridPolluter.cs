using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GridPolluter : MonoBehaviour
{
	public WorldGrid m_Grid;
	private List<PollutionPiece> m_Pollution = new List<PollutionPiece>();

	public float m_PollutionExpansionTime = 9999.0f;
	public float m_PollutionExpansionTimeVariation = 0.0f;

    public void Start()
    {
		StartCoroutine(PlayToxicSFXInterval());
    }

	IEnumerator PlayToxicSFXInterval()
	{
		while (true)
		{

			var time_variation = UnityEngine.Random.Range(-1.0f, 1.0f) * AudioManager.GetOrCreateInstance().toxic_sfx_interval_variation;
			var random_interval = AudioManager.GetOrCreateInstance().toxic_sfx_interval + time_variation;

			yield return new WaitForSeconds(random_interval);


			// if no toxic don't play sfx
			if (m_Pollution.Any(p => p.Coordinates.Any(c => c.Type.IsPolluted())))
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

	public void HealPositions(List<Coordinate> coordinates)
	{
		for (int p = 0; p < m_Pollution.Count; ++p)
		{
			m_Pollution[p].Coordinates.RemoveAll(c => coordinates.Contains(c));
		}
		m_Pollution.RemoveAll(p => p.Coordinates.Count == 0);
	}

    public void Reset()
    {
		m_Pollution.Clear();
	}

	public void AddToxicPools(List<ToxicPiece> pieces)
	{
		pieces.ForEach(piece =>
		{
			m_Pollution.Add(piece);

			piece.Coordinates.ForEach(coord => m_Grid.SetCoordType(coord, GridTileBuilder.TileType.toxic_pool));
			piece.GenerateExpansion();
			for (int i = 0; i < Random.Range(8, 11); ++i)
				piece.Expand();
		});
	}
}