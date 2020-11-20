using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GridBridge : MonoBehaviour
{
	public NavMeshObstacle m_Obstacle;

	private void Awake()
	{
		ToggleBridgeActive(false);
	}

	public void ToggleBridgeActive(bool active)
	{
		m_Obstacle.enabled = !active;
	}

	public bool BridgeActive()
	{
		return !m_Obstacle.enabled;
	}
}
