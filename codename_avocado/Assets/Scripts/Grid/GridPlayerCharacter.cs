using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public delegate void Vector2Function(Vector2 pos);
    public event Vector2Function OnPlaceDelegate = null;
    public UnityEvent OnSpawnDelegate;
    public UnityEvent OnDeathDelegate;

    private PreviewPlacement m_PreviewPlacement;

	public bool waitingForLevelToLoad;
    public bool waitingToRespawn;
    public bool canUnfold;
    public bool hasMoved;

    private void Awake()
    {
		//Debug.Log("Awake");
		CheckLink();
		waitingForLevelToLoad = true;
        m_Grid.OnLevelLoaded += M_Grid_OnLevelLoaded;
        m_Grid.OnLevelReady += M_Grid_OnLevelReadyLoaded;
	}

    private void M_Grid_OnLevelLoaded(IEnumerable<Coordinate> obj)
    {
		Respawn();
		m_AnimatedCharacter.gameObject.SetActive(false);
	}

	private void M_Grid_OnLevelReadyLoaded(IEnumerable<Coordinate> obj)
	{
		m_AnimatedCharacter.gameObject.SetActive(true);
		waitingForLevelToLoad = false;
	}

	private void Start()
	{
        CheckLink();
		m_CurrentCoordinte = m_Grid.m_Coordinates[0];
        canUnfold = false;

    }

    void CheckLink()
    {
        if (m_AnimatedCharacter && m_AnimatedCharacter.gridPC != this)
        {
            unfoldScript = m_AnimatedCharacter.unfold;
            m_AnimatedCharacter.SetPC(this);
        }
    }

    void Update()
	{
		if (waitingForLevelToLoad)
			return;

        //ugly hack, sorry :oP
        if (waitingToRespawn)
        {
            if (m_AnimatedCharacter.jumping || m_AnimatedCharacter.unfolding || m_AnimatedCharacter.dying)
                return; //wait
            waitingToRespawn = false;
            Respawn();
            PreparePlacement();
        }

        Movement();
		Interactions();
	}

	private void Movement()
	{
		if (!m_AnimatedCharacter.ReachedDestination())
			return;

		bool north = Input.GetKey(KeyCode.W) || Input.GetAxisRaw("Vertical") > 0.1f;
		bool south = Input.GetKey(KeyCode.S) || Input.GetAxisRaw("Vertical") < -0.1f;
		bool east = Input.GetKey(KeyCode.D) || Input.GetAxisRaw("Horizontal") > 0.1f;
		bool west = Input.GetKey(KeyCode.A) || Input.GetAxisRaw("Horizontal") < -0.1f;

		if (!north && !south && !east && !west)
			return;

		Direction direction = Direction.North;
		if (south)
			direction = Direction.South;
		if (east)
			direction = Direction.East;
		if (west)
			direction = Direction.West;

		//m_Facing = direction;
        //transform.rotation = Quaternion.Euler(0, heading[(int)direction], 0);

        //if (!AttemptMove(direction))
        AttemptMove(direction);
        PreparePlacement();
	}

	private bool AttemptMove(Direction direction)
	{
		Coordinate nextCoordinate = null;

        if (m_Facing != direction)
        {//just turn
            m_Facing = direction;
            transform.rotation = Quaternion.Euler(0, heading[(int)direction], 0);

            ClearPreview();
            MoveToCoordinate(m_CurrentCoordinte);
            CheckWinCondition();
            return true;
        }

        if (m_CurrentCoordinte.TryMove(direction, ref nextCoordinate))
		{
			//Debug.Log("moving to coordinate: " + nextCoordinate.GridPosition().x.ToString() + "," + nextCoordinate.GridPosition().y.ToString());
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
		m_PlayerPiece.m_origin_coords = m_CurrentCoordinte.m_Position;
	}

	private void CheckWinCondition()
	{
		if (m_Grid.IsFinalCoordinate(m_CurrentCoordinte))
		{
            //m_GameState.GameOver();

            //ToDo: activate win dialog instead and let THAT handle this
            m_GameState.Success();

            //AudioManager.GetOrCreateInstance().PlaySFX("UI_Level_Complete");

            waitingForLevelToLoad = true;
			//m_Grid.LoadNextLevel();
		}
	}

	private void PreparePlacement()
	{
		// always recreate for now, might switch directions...
		ClearPreview();

		Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
		//if (m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing))
		{
			m_PreviewPlacement = m_PlayerPiece.PreviewPlacement(placePostition, m_Facing);
		}

        //bool canPlace = m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing);
        //unfoldScript.ShowPrevis(canPlace ? Color.green : Color.red);
        canUnfold = m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing);
        //Debug.Log(canUnfold + " at " + placePostition.ToString() + m_Facing.ToString());
    }

    public bool CanUnfold()
    {
        //Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
        //return m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing);
        return canUnfold;
    }

	private void Interactions()
	{
		if (/*m_PreviewPlacement != null &&*/ Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Jump") > 0.1f)
		{
			TryPlacePiece();
		}
	}

	private void TryPlacePiece()
	{
		// check if can place piece in front of player...
		Coordinate nextCoordinate = null;
		//if (!m_CurrentCoordinte.TryMove(m_Facing, ref nextCoordinate))
		{
			ClearPreview();

			Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
			if (m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing))
			{
                OnPlaceDelegate?.Invoke(placePostition);

				// TODO: overlapping piece handling...
				m_PlayerPiece.Place(placePostition, m_Facing);
				//m_Grid.UpdateTileRepresentation(m_PlayerPiece);
				//m_Grid.m_Pieces.Add(m_PlayerPiece);
				m_Grid.m_Polluter.HandleHealing();

                //Respawn();
                waitingToRespawn = true;
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

        //unfoldScript.HidePrevis();
    }

    public void Die()
    {
        OnDeathDelegate?.Invoke();
        waitingToRespawn = true;
        //Respawn();
    }

	public void Respawn()
	{
        if (unfoldScript && unfoldScript.UnfoldShapeDefinitionAmount() > 0)
        {
            int shapeDefinitionIndex = NextShapeQueue.Instance.PopNext();
            m_currentUnfoldShapeDef = unfoldScript.shapeDefinitions[shapeDefinitionIndex];
            unfoldScript.UseUnfoldShapeDefinition(shapeDefinitionIndex);
        }

		m_PlayerPiece = GridPiece.GeneratePiece(m_Grid, m_Grid.m_Coordinates[0].m_Position, GridTileBuilder.TileType.grass, unfoldScript ? new UnfoldedShape(unfoldScript) : null);
		MoveToCoordinate(m_Grid.m_Coordinates[0]);
        canUnfold = false;

        //Debug.Log("gpc_spawn");
        OnSpawnDelegate?.Invoke();
	}
}
