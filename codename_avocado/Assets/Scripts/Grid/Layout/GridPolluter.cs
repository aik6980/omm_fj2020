using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGridPolluter
{
	void TickPollution();
	void AddToxicPools(List<ToxicPiece> pieces);
	void HealPositions(List<Coordinate> coordinates);
	bool HasAnyPollution();
	void Clear();
}


public class GridPolluter : IGridPolluter
{
	class ToxicSource
	{
		public ToxicPiece piece;
		public float nextExpansionTime;
	}

	private readonly WorldGrid m_Grid;
	private readonly List<ToxicSource> m_Pollution = new List<ToxicSource>();

	private readonly float m_PollutionExpansionTime = 9999.0f;
	private readonly float m_PollutionExpansionTimeVariation = 0.0f;

	public GridPolluter(WorldGrid grid, float pollutionExpansionTime, float pollutionExpansionTimeVariation)
	{
		m_Grid = grid;
		m_PollutionExpansionTime = pollutionExpansionTime;
		m_PollutionExpansionTimeVariation = pollutionExpansionTimeVariation;
	}

	public void TickPollution()
	{
		CheckExpandPollution();
	}

	public void AddToxicPools(List<ToxicPiece> pieces)
	{
		pieces.ForEach(piece =>
		{
			m_Pollution.Add(new ToxicSource() { piece = piece, nextExpansionTime = CalculateNextExpansion(Time.time) });

			piece.Coordinates.ForEach(coord => m_Grid.SetCoordType(coord, GridTileBuilder.TileType.toxic_pool));
			piece.GenerateExpansion();
			for (int i = 0; i < Random.Range(8, 11); ++i)
				piece.Expand();
		});
	}

	public void HealPositions(List<Coordinate> coordinates)
	{
		for (int p = 0; p < m_Pollution.Count; ++p)
		{
			m_Pollution[p].piece.Coordinates.RemoveAll(c => coordinates.Contains(c));
		}
		m_Pollution.RemoveAll(p => p.piece.Coordinates.Count == 0);
	}

	public bool HasAnyPollution()
	{
		return m_Pollution.Any(p => p.piece.Coordinates.Any(c => c.Type.IsPolluted()));
	}

	public void Clear()
	{
		m_Pollution.Clear();
	}

	private void CheckExpandPollution()
	{
		m_Pollution.ForEach(source =>
		{
			if (Time.time >= source.nextExpansionTime)
			{
				source.piece.Expand();
				source.piece.RedecorateCords();
				source.nextExpansionTime = CalculateNextExpansion(Time.time);
			}
			source.piece.UpdateTimer(source.nextExpansionTime - Time.time);
		});
	}

	private float CalculateNextExpansion(float initialTime) => initialTime + Random.Range(m_PollutionExpansionTime - m_PollutionExpansionTimeVariation, m_PollutionExpansionTime + m_PollutionExpansionTimeVariation);
}