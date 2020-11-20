using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoordinateRepresentation : MonoBehaviour
{
	public MeshRenderer m_Mesh;
	public Coordinate m_Coordinate;

	public void ToggleReaction(bool reacting)
	{
		if (m_Coordinate.CanInteract())
			m_Coordinate.HandleReacting(this, reacting);
	}

	public void PlayerInteracted(GridPlayerCharacter player)
	{
		if (m_Coordinate.CanInteract())
			m_Coordinate.HandleInteraction(this, player);
	}

	public void Configure(Coordinate coordinate)
	{
		m_Coordinate = coordinate;
		transform.position = new Vector3(m_Coordinate.m_Position.x, -.5f, m_Coordinate.m_Position.y);
		m_Coordinate.Decorate(this);
		Debug.Log("Spawned Coordinate: " + coordinate.m_Position.x.ToString() + "," +  coordinate.m_Position.y.ToString());
	}

	void OnTriggerEnter(Collider other)
	{
		GridPlayerCharacter player = other.gameObject.GetComponent<GridPlayerCharacter>();
		if (player != null)
		{
			player.SetRelevantCoordinate(this);
		}
	}

	void OnTriggerExit(Collider other)
	{
		GridPlayerCharacter player = other.gameObject.GetComponent<GridPlayerCharacter>();
		if (player != null && player.m_RelevantCoordinate == this)
		{
			player.SetRelevantCoordinate(null);
		}
	}
}
