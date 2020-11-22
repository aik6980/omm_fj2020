using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public struct WorldData
    {
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



	//public Vector
	public GridPiece m_FinalPiece;

	private void Awake()
	{
		var world = GetComponent<ILevelLoader>().LoadLevel(this);
		m_FinalPiece = world.end_piece;
		m_Polluter.AddVolcanoes(world.volcano_pieces);
		m_Polluter.AddBlocks(world.block_pieces);
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

	public bool SupportsPlacement(Vector2 placement, GridPiece piece, Direction direction)
	{
		for (int i = 0; i < piece.m_Coordinates.Count; ++i)
		{
			var coPosition = piece.m_Coordinates[i].TranslatedPosition(placement, direction);
			if (m_Polluter.Polluted(coPosition))
				return false;
		}

		return true;
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
