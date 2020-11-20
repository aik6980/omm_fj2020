using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayerCharacter : MonoBehaviour
{
	public float m_Speed;
	public CharacterController m_Controller;

	[HideInInspector]
	public CoordinateRepresentation m_RelevantCoordinate;

	void FixedUpdate()
	{
		Movement();
		Interactions();
	}

	private void Movement()
	{
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		m_Controller.Move(move * Time.deltaTime * m_Speed);
	}
	private void Interactions()
	{
		if (m_RelevantCoordinate != null && Input.GetKeyDown(KeyCode.Space))
			m_RelevantCoordinate.PlayerInteracted(this);
	}

	public void SetRelevantCoordinate(CoordinateRepresentation rep)
	{
		if (m_RelevantCoordinate != null && m_RelevantCoordinate != rep)
			m_RelevantCoordinate.ToggleReaction(false);

		m_RelevantCoordinate = rep;
		if (m_RelevantCoordinate != null)
		{
			Debug.Log("rep reacting");
			m_RelevantCoordinate.ToggleReaction(true);
		}
		else
		{
			Debug.Log("rep reacting finished");
		}
	}
}
