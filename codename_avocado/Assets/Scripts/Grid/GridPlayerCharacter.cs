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
	public GameState m_GameState;
	public WorldGrid m_Grid;
	public CharacterController m_Controller;
	private Coordinate m_CurrentCoordinte;
	public GridPiece m_PlayerPiece;

	public Direction m_Facing = Direction.North;
	public float m_Speed;

	private PreviewPlacement m_PreviewPlacement;

	private void Start()
	{
		Respawn();
		m_CurrentCoordinte = m_Grid.m_Coordinates[0];
	}

	void Update()
	{
		Movement();
		Interactions();
	}

	private void Movement()
	{
		bool north = Input.GetKeyDown(KeyCode.W);
		bool south = Input.GetKeyDown(KeyCode.S);
		bool east = Input.GetKeyDown(KeyCode.D);
		bool west = Input.GetKeyDown(KeyCode.A);

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
		m_CurrentCoordinte = coordinate;
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
		m_PreviewPlacement = m_PlayerPiece.PreviewPlacement(placePostition);
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
			// TODO: overlapping piece handling...
			m_PlayerPiece.Place(placePostition);
			Respawn();
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
		MoveToCoordinate(m_Grid.m_Coordinates[0]);
		m_PlayerPiece = GridPiece.GeneratePiece(m_Grid);
	}
}
