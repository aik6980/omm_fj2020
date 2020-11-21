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

	public GridPiece(WorldGrid grid, Shape shape)
	{
		m_Color = shape.GetColor();
		m_Grid = grid;
		m_Positions = shape.Coordinates();
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

	public static GridPiece GeneratePiece(WorldGrid grid, Shape shapeOverride = null)
	{
		Shape shape = shapeOverride;
		if (shape == null)
			shape = Shape.RandomShape();

		GridPiece newPiece = new GridPiece(grid, shape);
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