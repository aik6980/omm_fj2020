using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
	public GameObject m_CoordinatePrefab;
	public List<GridPiece> m_Pieces = new List<GridPiece>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();

	private void Awake()
	{
		var startPiece = GridPiece.GeneratePiece(this);
		startPiece.Place(Vector2.zero);
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

	public void RepresentPiece(GridPiece newPiece)
	{
		newPiece.m_Coordinates.ForEach((Coordinate coordinate) =>
		{
			var coordinateRepGO = GameObject.Instantiate(m_CoordinatePrefab);
			coordinateRepGO.GetComponent<CoordinateRepresentation>().Configure(coordinate, false);
		});

		m_Pieces.ForEach((GridPiece piece) =>
		{
			piece.PopulateCoords(this);
		});
	}

}
