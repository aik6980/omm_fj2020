using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPlacement
{
	List<CoordinateRepresentation> m_Reps;

	public PreviewPlacement(List<CoordinateRepresentation> reps)
	{
		m_Reps = reps;
		m_Reps.ForEach((CoordinateRepresentation rep) => rep.SetColor(Color.white));
	}

	public void Clear()
	{
		m_Reps.ForEach((CoordinateRepresentation rep) => GameObject.Destroy(rep.gameObject));
	}
}

public class GridPiece
{
	public bool m_Placed = false;
	public Vector2 m_PlacedPosition;
	public List<Vector2> m_Positions = new List<Vector2>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();
	
	private WorldGrid m_Grid;

	private Color m_Color;

	public GridPiece(WorldGrid grid, List<Vector2> positions)
	{
		m_Color = Random.ColorHSV();
		m_Grid = grid;
		m_Positions = positions;
		for (int i = 0; i < m_Positions.Count; ++i)
		{
			var newCoord = new Coordinate(this, m_Positions[i]);
			m_Coordinates.Add(newCoord);
		}
	}

	public void PopulateCoords(WorldGrid grid)
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Populate(grid);
		}
	}

	public static GridPiece GeneratePiece(WorldGrid grid)
	{
		List<Vector2> coords = new List<Vector2>();
		int randomShape = Random.Range(0, 2);
		coords.Add(new Vector2(0, 0));
		switch (randomShape)
		{
			case 0:
				// make random shape...
				coords.Add(new Vector2(0, 1));
				coords.Add(new Vector2(0, 2));
				coords.Add(new Vector2(0, 3));
				break;
			case 1:
				// make random shape...
				coords.Add(new Vector2(0, 1));
				coords.Add(new Vector2(0, 2));
				coords.Add(new Vector2(1, 2));
				break;
		}

		GridPiece newPiece = new GridPiece(grid, coords);
		return newPiece;
	}

	public void Place(Vector2 position)
	{
		m_PlacedPosition = position;
		m_Placed = true;
		m_Grid.LinkPiece(this);
		var coordinates = m_Grid.RepresentPiece(this);
		coordinates.ForEach((CoordinateRepresentation rep) => rep.SetColor(m_Color));
	}

	public PreviewPlacement PreviewPlacement(Vector2 position)
	{
		m_PlacedPosition = position;
		var preview = new PreviewPlacement(m_Grid.RepresentPiece(this));
		return preview;
	}
}