using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coordinate
{
	// serialize this in firebase
	public Vector2 m_Position;
	public WorldGrid m_Grid;

	public Coordinate(WorldGrid grid, Vector2 position)
	{
		m_Grid = grid;
		m_Position = position;
		m_Grid.CoordinateRegistration(this, true);
	}

	public virtual void HandleReacting(CoordinateRepresentation rep, bool reacting)
	{
		rep.m_Mesh.material.color = reacting ? Color.green : Color.red;
	}

	public virtual void HandleInteraction(CoordinateRepresentation rep, GridPlayerCharacter player)
	{
		
	}

	public virtual void Decorate(CoordinateRepresentation rep)
	{
	}

	public virtual bool CanInteract()
	{
		return true;
	}
}

// needs to be healed, can be many types of coordinates...
public class BridgeCoordinate : Coordinate
{
	private GridBridge m_Bridge;

	public BridgeCoordinate(WorldGrid grid, Vector2 position, GridBridge bridge) 
		: base(grid, position)
	{
		m_Bridge = bridge;
	}

	public override void Decorate(CoordinateRepresentation rep)
	{
		rep.m_Mesh.material.color = Color.red;
	}

	public override bool CanInteract()
	{
		return !m_Bridge.BridgeActive();
	}

	public override void HandleReacting(CoordinateRepresentation rep, bool reacting)
	{
		rep.m_Mesh.material.color = reacting ? Color.blue : Color.red;
	}

	public override void HandleInteraction(CoordinateRepresentation rep, GridPlayerCharacter player)
	{
		player.SetRelevantCoordinate(null);
		m_Grid.CoordinateRegistration(this, false);
		m_Bridge.ToggleBridgeActive(true);
		rep.m_Mesh.material.color = Color.green;
		player.Respawn();
	}

}



// needs to be healed, can be many types of coordinates...
public class BrokenCoordinate : Coordinate
{
	public BrokenCoordinate(WorldGrid grid, Vector2 position) : base(grid, position) { }

	public override void Decorate(CoordinateRepresentation rep)
	{
		rep.m_Mesh.material.color = Color.red;
	}

	public override void HandleReacting(CoordinateRepresentation rep, bool reacting)
	{
		rep.m_Mesh.material.color = reacting ? Color.green : Color.red;
	}

	public override void HandleInteraction(CoordinateRepresentation rep, GridPlayerCharacter player)
	{
		// heal what is broken
		player.SetRelevantCoordinate(null);
		m_Grid.CoordinateRegistration(this, false);
		GameObject.Destroy(rep.gameObject);
	}

}

