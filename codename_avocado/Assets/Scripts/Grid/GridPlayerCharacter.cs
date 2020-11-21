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
	public WorldGrid m_Grid;
	public CharacterController m_Controller;
	private Coordinate m_CurrentCoordinte;
	public GridPiece m_PlayerPiece;

	public Direction m_Facing = Direction.North;

	public GridSpawn m_SpawnPoint;
	public float m_Speed;

	private void Start()
	{
		Respawn();
		m_CurrentCoordinte = m_Grid.m_Coordinates[0];
	}

	void FixedUpdate()
	{
		Movement();
		Interactions();
	}

	private void Movement()
	{
		if (!Input.GetKeyDown(KeyCode.W) && !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.S) && !Input.GetKeyDown(KeyCode.D))
			return;

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Direction direction = vertical > 0f ? Direction.North : Direction.South;
		if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
		{
			direction = horizontal > 0 ? Direction.East : Direction.West;
		}

		m_Facing = direction;
		AttemptMove(direction);
	}

	private void AttemptMove(Direction direction)
	{
		Coordinate nextCoordinate = null;
		if (m_CurrentCoordinte.TryMove(direction, ref nextCoordinate))
		{
			m_CurrentCoordinte = nextCoordinate;
			Vector3 desiredPosition = new Vector3(m_CurrentCoordinte.GridPosition().x, 0f, m_CurrentCoordinte.GridPosition().y);
			transform.position = desiredPosition;
		}
	}

	private void Interactions()
	{
		if (Input.GetKeyDown(KeyCode.Space))
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
			Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
			// TODO: overlapping piece handling...
			m_PlayerPiece.Place(placePostition);
			Respawn();
		}
	}


	public void Respawn()
	{
		transform.position = m_SpawnPoint.transform.position;
		m_PlayerPiece = GridPiece.GeneratePiece(m_Grid);
	}
}
