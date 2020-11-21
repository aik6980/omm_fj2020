using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece
{
	public Vector2 m_PlacedPosition;
	public List<Vector2> m_Positions = new List<Vector2>();
	public List<Coordinate> m_Coordinates = new List<Coordinate>();
	
	public WorldGrid m_Grid;

	public Shape m_Shape;

	public GridPiece(WorldGrid grid, Shape shape)
	{
		m_Shape = shape;
		m_Grid = grid;
		m_Positions = shape.Coordinates();
		for (int i = 0; i < m_Positions.Count; ++i)
		{
			var newCoord = BuildCoordinate(m_Positions[i]);
			m_Coordinates.Add(newCoord);
		}
	}

	protected virtual Coordinate BuildCoordinate(Vector2 position)
	{
		return new Coordinate(this, position);
	}

	public virtual Color GetColor()
	{
		return m_Shape.GetColor();
	}

	public void PopulateCoords(List<Coordinate> coordinates)
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Populate(coordinates);
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
		m_Grid.LinkPiece(this);
		m_Grid.RepresentPiece(this);
	}

	public PreviewPlacement PreviewPlacement(Vector2 position)
	{
		m_PlacedPosition = position;
		var preview = new PreviewPlacement(m_Grid.RepresentPiece(this));
		return preview;
	}

	public void RedecorateCords()
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Redecorate();
		}
	}
}


public class PollutionPiece : GridPiece
{
	public PollutionPiece(WorldGrid grid, Shape shape, Vector2 position)
		: base(grid, shape)
	{
		m_PlacedPosition = position;
		m_Grid.RepresentPiece(this);
		// pollution populated with itself...
		PopulateCoords(m_Coordinates);
		RedecorateCords();
	}

	protected override Coordinate BuildCoordinate(Vector2 position)
	{
		return new PollutionCoordinate(this, position);
	}

	public void CoordinateHealed(PollutionCoordinate coordinate)
	{
		if (coordinate.m_Position == Vector2.zero)
		{
			// center healed...
			for (int i = 0; i < m_Coordinates.Count; ++i)
			{
				var co = m_Coordinates[i];
				if (co != coordinate)
				{
					co.Heal(true);
				}
			}

			m_Coordinates.Clear();
			m_Grid.m_Polluter.m_Pollution.Remove(this);
		}
		else
		{
			m_Grid.m_Coordinates.Remove(coordinate);
			m_Coordinates.Remove(coordinate);
		}

		PopulateCoords(m_Coordinates);
		RedecorateCords();
	}

}


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
