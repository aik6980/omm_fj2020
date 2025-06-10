using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum Direction
{
	North,
	East,
	South,
	West
}

public static class DirectionExtensions
{
	private static float[] heading = { 0.0f, 90.0f, 180.0f, -90.0f };
	public static float Heading(this Direction d) => heading[(int)d];

	public static int NextCWIndex(this Direction d) => ((d.Index() + 1) % 4);

	public static Direction NextCW(this Direction d) => (Direction)NextCWIndex(d);

	public static int NextACWIndex(this Direction d) => ((4 + d.Index() - 1) % 4);

	public static Direction NextACW(this Direction d) => (Direction)NextACWIndex(d);
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
    public delegate void Vector2Function(Vector2 pos);
    public event Vector2Function OnPlaceDelegate = null;
    public UnityEvent OnSpawnDelegate;
    public UnityEvent OnDeathDelegate;

	public bool waitingForLevelToLoad = true;
    public bool waitingToRespawn = false;
    public bool canUnfold = false;
    public bool hasMoved = false;

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
		m_CurrentCoordinte = m_Grid.m_start_coordinate;
		PreparePlacement();
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


		var directionVec = Vector2.zero;
		directionVec += Input.GetAxisRaw("Vertical") * Vector2.up;
		directionVec += Input.GetAxisRaw("Vertical") * Vector2.up;
		directionVec += Input.GetAxisRaw("Horizontal") * Vector2.right;
		directionVec += Input.GetAxisRaw("Horizontal") * Vector2.right;

		if (directionVec.sqrMagnitude < 0.16f)
			return;

        AttemptMove(directionVec);
        PreparePlacement();
	}

	private bool AttemptMove(Vector2 directionVec)
	{
		var newFacing = Direction.North;
		if (Mathf.Abs(directionVec.y) >= Mathf.Abs(directionVec.x))
        {
			newFacing = directionVec.y > 0.0f ? Direction.North : Direction.South;
		}
		else
        {
			newFacing = directionVec.x > 0.0f ? Direction.East : Direction.West;
		}

		if (newFacing != m_Facing)
		{//just turn
            m_Facing = newFacing;
            transform.rotation = Quaternion.Euler(0, newFacing.Heading(), 0);

            MoveToCoordinate(m_CurrentCoordinte);
            CheckWinCondition();
            return true;
        }

		if (m_Grid.TryMove(m_CurrentCoordinte, directionVec, out var nextCoordinate))
		{
			//Debug.Log("moving to coordinate: " + nextCoordinate.GridPosition().x.ToString() + "," + nextCoordinate.GridPosition().y.ToString());
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

            AudioManager.GetOrCreateInstance().PlaySFX("UI_Level_Complete");

            waitingForLevelToLoad = true;
			//m_Grid.LoadNextLevel();
		}
	}

	private void PreparePlacement()
	{
		// always recreate for now, might switch directions...
		Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
		m_PlayerPiece.UpdatePreviewPlacement(placePostition, m_Facing);
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
		if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Jump") > 0.1f)
		{
			TryPlacePiece();
		}
	}

	private void TryPlacePiece()
	{
		// check if can place piece in front of player...
		//if (!m_CurrentCoordinte.TryMove(m_Facing, ref nextCoordinate))
		{
			Vector2 placePostition = WorldGrid.OffsetDirection(m_CurrentCoordinte.GridPosition(), m_Facing);
			if (m_Grid.SupportsPlacement(placePostition, m_PlayerPiece, m_Facing))
			{
                OnPlaceDelegate?.Invoke(placePostition);

				// TODO: overlapping piece handling...
				m_Grid.Place(Vector2Int.RoundToInt(placePostition), m_Facing, m_PlayerPiece.m_Shape, GridTileBuilder.TileType.grass);
				m_Grid.UpdateTileRepresentation(m_PlayerPiece);

                //Respawn();
                waitingToRespawn = true;
			}
		}
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

        m_PlayerPiece = GridPiece.GeneratePiece(m_Grid, m_Grid.m_start_coordinate.m_Position, GridTileBuilder.TileType.grass, unfoldScript ? new UnfoldedShape(unfoldScript) : null);
		MoveToCoordinate(m_Grid.m_start_coordinate);

        m_Facing = Direction.North;
        AttemptMove(Vector2.right);
        PreparePlacement();

        //Debug.Log("gpc_spawn");
        OnSpawnDelegate?.Invoke();
	}
}
