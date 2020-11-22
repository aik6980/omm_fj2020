using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
	North,
	South,
	East,
	West
}

public class GridPlayerCharacter : MonoBehaviour
{
	public AnimatedCharacter m_AnimatedCharacter;
	public GameState m_GameState;
	public WorldGrid m_Grid;
	public CharacterController m_Controller;
	private Coordinate m_CurrentCoordinte;
	public GridPiece m_PlayerPiece;
    public Unfold unfoldScript;

    public UnfoldShapeDefinition m_currentUnfoldShapeDef;

    public Direction m_Facing = Direction.North;
	public float m_Speed;

    //config
    public float[] heading = { 0, 180, 90, -90 };

    public delegate void VoidFunction();
    public delegate void Vector2Function(Vector2 pos);
    public event Vector2Function OnPlaceDelegate = null;
    public event VoidFunction OnSpawnDelegate = null;

    private PreviewPlacement m_PreviewPlacement;

    public bool unfolding;

	private void Start()
	{
		Respawn();
		m_CurrentCoordinte = m_Grid.m_Coordinates[0];
	}

	void Update()
	{
        //ugly hack, sorry :oP
        if (unfolding)
        {
            if (m_AnimatedCharacter.jumping || m_AnimatedCharacter.unfolding)
                return; //wait
            unfolding = false;
            Respawn();
        }

		Movement();
		Interactions();
	}

	private void Movement()
	{
		if (!m_AnimatedCharacter.ReachedDestination())
			return;

		bool north = Input.GetKey(KeyCode.W);
		bool south = Input.GetKey(KeyCode.S);
		bool east = Input.GetKey(KeyCode.D);
		bool west = Input.GetKey(KeyCode.A);

		if (!north && !south && !east && !west)
			return;

		Direction direction = Direction.North;
		if (south)
			direction = Direction.South;
		if (east)
			direction = Direction.East;
		if (west)
			direction = Direction.West;

		m_Facing = direction;
        transform.rotation = Quaternion.Euler(0, heading[(int)direction], 0);

		if (!AttemptMove(direction))
			PreparePlacement();
	}

	private bool AttemptMove(Direction direction)
	{
		Coordinate nextCoordinate = null;
		if (m_CurrentCoordinte.TryMove(direction, ref nextCoordinate))
		{
			Debug.Log("moving to coordinate: " + nextCoordinate.GridPosition().x.ToString() + "," + nextCoordinate.GridPosition().y.ToString());
			ClearPreview();
			MoveToCoordinate(nextCoordinate);
			CheckWinCondition();
			return true;
		}
		return false;
	}

	private void MoveToCoordinate(Coordinate coordinate)
	{
		if (m_CurrentCoordinte != null)
			m_CurrentCoordinte.m_PopulatedPlayer = null;

		m_CurrentCoordinte = coordinate;
		m_CurrentCoordinte.m_PopulatedPlayer = this;
		Vector3 desiredPosition = new Vector3(m_CurrentCoordinte.GridPosition().x, 0f, m_CurrentCoordinte.GridPosition().y);
		transform.position = desiredPosition;
	}

	private void CheckWinCondition()
	{
		if (m_Grid.IsFinalCoordinate(m_CurrentCoordinte))
		{
			m_GameState.GameOver();
		}
	}

	private void PreparePlacement()
	{
		// always recreate for now, might switch directions...
		ClearPreview();
		Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
		if (m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing))
		{
			m_PreviewPlacement = m_PlayerPiece.PreviewPlacement(placePostition, m_Facing);
		}
	}

	private void Interactions()
	{
		if (m_PreviewPlacement != null && Input.GetKeyDown(KeyCode.Space))
		{
			TryPlacePiece();
		}
	}

	private void TryPlacePiece()
	{
		// check if can place piece in front of player...
		Coordinate nextCoordinate = null;
		if (!m_CurrentCoordinte.TryMove(m_Facing, ref nextCoordinate))
		{
			ClearPreview();

			Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
			if (m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing))
			{
                OnPlaceDelegate?.Invoke(placePostition);

				// TODO: overlapping piece handling...
				m_PlayerPiece.Place(placePostition, m_Facing);
				m_Grid.m_Polluter.HandleHealing();
                
                //Respawn();
                unfolding = true;
			}
		}
	}

	private void ClearPreview()
	{
		if (m_PreviewPlacement != null)
		{
			m_PreviewPlacement.Clear();
			m_PreviewPlacement = null;
		}
	}

	public void Respawn()
	{
        if (unfoldScript && unfoldScript.UnfoldShapeDefinitionAmount() > 0)
        {
            int shapeDefinitionIndex = Random.Range(0, unfoldScript.UnfoldShapeDefinitionAmount());
            m_currentUnfoldShapeDef = unfoldScript.shapeDefinitions[shapeDefinitionIndex];
            unfoldScript.UseUnfoldShapeDefinition(shapeDefinitionIndex);
        }

		MoveToCoordinate(m_Grid.m_Coordinates[0]);
		m_PlayerPiece = GridPiece.GeneratePiece(m_Grid, unfoldScript ? new UnfoldedShape(unfoldScript) : null);
		OnSpawnDelegate?.Invoke();
	}
}
