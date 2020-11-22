using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece
{
	public Direction m_PlacedDirection;
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
		BuildCoordinates(m_Positions);
	}

	protected List<CoordinateRepresentation> RepresentCoordianates(List<Coordinate> coordinaates)
	{
		List<CoordinateRepresentation> reps = new List<CoordinateRepresentation>();
		coordinaates.ForEach((Coordinate coordinate) =>
		{
			var coordinateRepGO = GameObject.Instantiate(m_Grid.m_CoordinatePrefab);
			var rep = coordinateRepGO.GetComponent<CoordinateRepresentation>();
			reps.Add(rep);
			rep.Configure(coordinate);
		});

		return reps;

	}

	protected List<Coordinate> BuildCoordinates(List<Vector2> positions)
	{
		List<Coordinate> newCoordinates = new List<Coordinate>();
		for (int i = 0; i < positions.Count; ++i)
		{
			var newCoord = BuildCoordinate(positions[i]);
			newCoordinates.Add(newCoord);
		}

		m_Coordinates.AddRange(newCoordinates);
		return newCoordinates;
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

	public void Place(Vector2 position, Direction direction)
	{
		m_PlacedPosition = position;
		m_PlacedDirection = direction;
		m_Grid.LinkPiece(this);
		RepresentCoordianates(m_Coordinates);
	}

	public PreviewPlacement PreviewPlacement(Vector2 position, Direction direction)
	{
		m_PlacedPosition = position;
		m_PlacedDirection = direction;
		var preview = new PreviewPlacement(RepresentCoordianates(m_Coordinates));
		return preview;
	}

	public void RedecorateCords()
	{
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].Redecorate();
		}
	}

	public void CoordinateHealed(Coordinate coordinate)
	{
		m_Coordinates.Remove(coordinate);
		if (CheckHealedPiece(coordinate))
		{
			for (int i = 0; i < m_Coordinates.Count; ++i)
			{
				var co = m_Coordinates[i];
				if (co != coordinate)
				{
					m_Grid.m_Coordinates.Remove(co);
					co.Heal(true);
				}
			}

			m_Coordinates.Clear();
			OnPieceHealed();
		}

		PopulateCoords(m_Coordinates);
		RedecorateCords();
	}

	protected virtual bool CheckHealedPiece(Coordinate coordinate)
	{
		return m_Coordinates.Count == 0;
	}

	protected virtual void OnPieceHealed()
	{
		m_Grid.m_Pieces.Remove(this);
	}
}


public class BlockingPiece : PollutionPiece
{
	public BlockingPiece(WorldGrid grid, Shape shape, Vector2 position)
		: base(grid, shape, position)
	{
	}

	public override void TickPollution()
	{
	}

	protected override Coordinate BuildCoordinate(Vector2 position)
	{
		return new BlockingCoordinate(this, position);
	}

	protected override bool CheckHealedPiece(Coordinate coordinate)
	{
		return false;
	}

	protected override void OnPieceHealed()
	{
		//m_Grid.m_Polluter.m_Pollution.Remove(this);
	}
}

public class PollutionPiece : GridPiece
{
	public float m_ExpansionTime = 5f;
	private float m_LastExpansion = -1f;

	List<Vector2> m_CurrentExpansion = new List<Vector2>();

	public PollutionPiece(WorldGrid grid, Shape shape, Vector2 position)
		: base(grid, shape)
	{
		m_ExpansionTime = grid.m_Polluter.m_PollutionExpansionTime;
		// want random offset for each pollution
		m_LastExpansion = Random.Range(Time.time, Time.time + (m_ExpansionTime / 2));

		m_PlacedDirection = Direction.North;
		m_PlacedPosition = position;
		m_Grid.m_Polluter.m_PollutionCoordinates.AddRange(m_Coordinates);
		RepresentCoordianates(m_Coordinates);
		PopulateCoords(m_Grid.m_Polluter.m_PollutionCoordinates);
		RedecorateCords();
	}

	public virtual void TickPollution()
	{
		if (m_LastExpansion > 0f)
		{
			float delta = Time.time - m_LastExpansion;
			if (delta >= m_ExpansionTime)
			{
				m_LastExpansion = Time.time;
				// grow outwards...
				Expand();
				RedecorateCords();
			}
		}
	}

	private void GenerateExpansion()
	{
		// get empty neighbors and build new neighbor there...
		m_CurrentExpansion = new List<Vector2>();
		for (int i = 0; i < m_Coordinates.Count; ++i)
		{
			m_Coordinates[i].AppendEmptyNeighbors(ref m_CurrentExpansion);
		}
	}

	private void Expand()
	{
		if (m_CurrentExpansion.Count == 0)
			GenerateExpansion();

		List<Vector2> coordsToGrow = new List<Vector2>();
		// only do one for now...
		int randomExpand = Random.Range(0, m_CurrentExpansion.Count);
		var expansion = m_CurrentExpansion[randomExpand];
		m_CurrentExpansion.Remove(expansion);
		coordsToGrow.Add(expansion);

		List<Coordinate> newCoords = BuildCoordinates(coordsToGrow);
		m_Grid.m_Polluter.m_PollutionCoordinates.AddRange(newCoords);
		List<Vector2> gridNeighborPositions = new List<Vector2>();
		for (int i = 0; i < coordsToGrow.Count; ++i)
		{
			gridNeighborPositions.Add(m_PlacedPosition + coordsToGrow[i]);
		}


		for (int i = 0; i < m_Grid.m_Coordinates.Count; ++i)
		{
			if (gridNeighborPositions.Contains(m_Grid.m_Coordinates[i].GridPosition()))
			{
				// heal any bridge piece neighbors... (todo: handle other pollution pieces if we have multiple polluters...)
				m_Grid.m_Coordinates[i].Heal();
			}
		}

		RepresentCoordianates(newCoords);
		PopulateCoords(m_Grid.m_Polluter.m_PollutionCoordinates);
		RedecorateCords();
	}

	protected override Coordinate BuildCoordinate(Vector2 position)
	{
		return new PollutionCoordinate(this, position);
	}

	protected override bool CheckHealedPiece(Coordinate coordinate)
	{
		return coordinate.m_Position == Vector2.zero;
	}

	protected override void OnPieceHealed()
	{
		m_Grid.m_Polluter.m_Pollution.Remove(this);
	}

}

public class PreviewPlacement
{
	List<CoordinateRepresentation> m_Reps;

	public PreviewPlacement(List<CoordinateRepresentation> reps)
	{
		m_Reps = reps;
		m_Reps.ForEach((CoordinateRepresentation rep) =>
		{
			rep.SetColor(Color.white);
			rep.Offset(0.2f);
		});
	}

	public void Clear()
	{
		m_Reps.ForEach((CoordinateRepresentation rep) => GameObject.Destroy(rep.gameObject));
	}
}
