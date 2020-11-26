using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    public GameObject player;       // follow this
    public GridPlayerCharacter gridPC;
    //to be removed
    public Animation animation;     // set the animations in this
    //new
    public Animator animtr;

    public Unfold unfold;           // should it be here?

    public string[] animClips;
    public GameObject[] models;

    public Transform facePivot;
    public Transform armLPivot;
    public Transform armRPivot;

    public GameObject[] faces;
    public GameObject[] arms;
    public GameObject deathFace;

    public Material mat_canUnfold;
    public Material mat_cannotUnfold;

    //yeah i know, will refactor it :oP
    public bool moving = false;
    public bool jumping = false;
    public bool unfolding = false;
    public bool dying = false;
    public bool havePrevis = false;

    public GameObject face;
    public GameObject armL;
    public GameObject armR;

    public Vector3 offset;

    public float unfoldedWait = 0.5f;
    public float unfoldedTimer;

    void Start()
    {
        if (gridPC)
        {
            gridPC.OnPlaceDelegate += OnPlace;
            gridPC.OnSpawnDelegate.AddListener(OnSpawn);
            gridPC.OnDeathDelegate.AddListener(OnDie);
        }

        //Spawn();
        if (animtr)
            animtr.gameObject.SetActive(true);

    }

    public void SetPC(GridPlayerCharacter gpc)
    {
        gridPC = gpc;
        player = gpc.gameObject;
        if (gridPC)
        {
            //Debug.Log("setpc");
            gridPC.OnPlaceDelegate += OnPlace;
            gridPC.OnSpawnDelegate.AddListener(OnSpawn);
        }
    }

    void OnSpawn()
    {
        //Debug.Log("onspawn", this);
        if (!jumping)
            Spawn();
    }
    void OnDie()
    {
        Debug.Log("ondie");
        dying = true;
        if (animtr)
            animtr.SetBool("dead", true);
        if (face) Destroy(face);
        face = Instantiate(deathFace, facePivot.position, facePivot.rotation, facePivot);

    }

    void OnPlace(Vector2 pos)
    {
        Debug.Log("jump", this);
        moving = false;
        jumping = true;
        if (animation)
        {
            animation.Play(animClips[2]);
        }
        if (animtr)
            animtr.SetBool("jumping", true);
    }

    void Spawn()
    {
        Debug.Log("spawn?", this);
        if (player == null) return;
        if (gridPC == null) return;

        Debug.Log("spawn", this);

        this.transform.rotation = player.transform.rotation;
        this.transform.position = player.transform.position;

        moving = false;
        jumping = false;
        unfolding = false;
        dying = false;

        if (animation)
        {
            //show model
            animation.gameObject.SetActive(true);
            animation.Play(animClips[0]);
        }
        if (animtr)
        {
            //show model
            animtr.gameObject.SetActive(true);

            animtr.SetBool("moving", false);
            animtr.SetBool("jumping", false);
            animtr.SetBool("unfolding", false);
            animtr.SetBool("dead", false);
            animtr.SetTrigger("Reset");
            animtr.Play("Spawn");
            Debug.Log("reset");
        }

        //pick shape
        //ToDo: based on unfold shape
        int n = Random.Range(0, 1000) % models.Length;
        //i know, right now it's one to one :oP might not stay like that
        switch (gridPC.m_currentUnfoldShapeDef.bodyShape)
        {
            case BodyShape.Cube:
                n = 0;
                break;
            case BodyShape.L:
                n = 1;
                break;
            case BodyShape.Long:
                n = 2;
                break;
            case BodyShape.T:
                n = 3;
                break;
            case BodyShape.Z:
                n = 4;
                break;

            default:
                n = Random.Range(0, 1000) % models.Length;
                break;
        }
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(i == n);
            if (models[i].active)
            {
                models[i].GetComponent<Renderer>().materials[0].color = gridPC.m_currentUnfoldShapeDef.color;
            }
        }

        PickDecor();

        unfold.HidePrevis();
        havePrevis = false;
    }

    void PickDecor()
    {
        //Debug.Log("pick");
        int faceType = Random.Range(0, faces.Length);
        int armType = Random.Range(0, arms.Length);
        if (face) Destroy(face);
        if (armL) Destroy(armL);
        if (armR) Destroy(armR);
        face = Instantiate(faces[faceType], facePivot.position, facePivot.rotation, facePivot);
        armL = Instantiate(arms[armType], armLPivot.position, armLPivot.rotation * arms[armType].transform.rotation, armLPivot);
        armR = Instantiate(arms[armType], armRPivot.position, armRPivot.rotation * arms[armType].transform.rotation, armRPivot);
    }

    void Update()
    {
        if (player == null) return;

        //hack
        //animation.transform.localPosition = Vector3.down * 0.8f;

        float dT = Time.deltaTime;

        if (jumping)
        {
            //there's a strict sequence here:
            //1. play and finish faceplant anim
            //2. activate Unfold and hide the character mesh
            //3. play the procedural unfolding from 0 to 100% over some time
            //4. THEN ready to respawn

            if (havePrevis)
                unfold.HidePrevis();

            if (animation)
            {
                if (animation.IsPlaying(animClips[2]))
                { //wait
                  //Debug.Log("jumping...");
                    return;
                }
                else
                {
                    Debug.Log("jump done.");
                    jumping = false;

                    unfolding = true;
                    unfold.progress = 0;
                    unfold.SpawnFaces();
                    unfoldedTimer = 0;
                    //hide model
                    animation.gameObject.SetActive(false);
                    return;
                    //Spawn();
                }
            }
            if (animtr)
            {
                animtr.SetBool("jumping", true);
                AnimatorStateInfo asi = animtr.GetCurrentAnimatorStateInfo(0);
                if (!asi.IsName("Jump") || asi.normalizedTime < 0.99f)
                {//wait
                    //ToDo: make sure it reaches the target transform by the time the anim ends
                    float timeLeft = asi.length * 1.0f - asi.normalizedTime;
                    //Debug.Log("jumping..." + timeLeft);
                } else
                {//finished
                    Debug.Log("jump done.");
                    jumping = false;

                    unfolding = true;
                    unfold.progress = 0;
                    unfold.SpawnFaces();
                    //hide model
                    animtr.SetBool("unfolding", true);
                    animtr.gameObject.SetActive(false);
                    //since it's disabled (to hide), wouldn't play on animtr.gameobject
                    unfold.gameObject.SendMessage("Anim_Unfold", SendMessageOptions.DontRequireReceiver);
                    return;
                }
            }
        }

        if (unfolding)
        {
            if (unfold.Finished())
            {
                unfoldedTimer += Time.deltaTime;
                if (unfoldedTimer < unfoldedWait) return;
                unfolding = false;
                unfold.UnSpawnFaces();
                Spawn();
            }
            unfoldedTimer = 0;
            int preFlats = unfold.NumFlats();
            unfold.UnfoldStep(4.0f * dT / Mathf.Max(1, unfold.maxGenerations));
            int numFlats = unfold.NumFlats();
            if (numFlats > preFlats)
            {
                Debug.Log(preFlats + " -> " + numFlats);
                unfold.gameObject.SendMessage("Anim_Unfold", SendMessageOptions.DontRequireReceiver);
            }
            return;
        }

        if (dying)
        {
            animtr.SetBool("dead", true);
            AnimatorStateInfo asi = animtr.GetCurrentAnimatorStateInfo(0);
            if (!asi.IsName("Death") || asi.normalizedTime < 0.99f)
            {//wait
             //ToDo: make sure it reaches the target transform by the time the anim ends
                float timeLeft = asi.length * 1.0f - asi.normalizedTime;
                //Debug.Log("dying..." + timeLeft);
            }
            else
            {//finished
                Debug.Log("death done.");
                dying = false;
            }
            return;
        }

        if (animtr)
            animtr.ResetTrigger("Reset");

        if (!dying)
        {
            if (Random.value < 0.01f)
                PickDecor();

            if (Random.value < 0.005f)
                if (animtr)
                    animtr.SetBool("alt_idle", Random.value < 0.5f);
        }

        offset = player.transform.position - this.transform.position;
        //offset.z = ((player.transform.rotation.y - this.transform.rotation.y) + 180.0f) % 360.0f - 180.0f;

        if (animation)
        {
            if (!moving && offset.magnitude > 0.1f)
            {
                moving = true;
                animation.Play(animClips[1]);
            }
            else
            if (moving && offset.magnitude <= 0.1f)
            {
                moving = false;
                animation.Play(animClips[0]);
            }
        }


        if (animtr)
        {
            moving = offset.magnitude > 0.1f || Quaternion.Angle(this.transform.rotation, player.transform.rotation) > 10.0f;
            animtr.SetBool("moving", moving);
        }

        Quaternion faceDir = offset.magnitude > 0.1f ? Quaternion.LookRotation(offset) : player.transform.rotation;
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, faceDir, 400.0f * dT);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 5.0f * dT);

        if (moving)
        {
            if (havePrevis)
                unfold.HidePrevis();
            havePrevis = false;
        } else
        {
            if (!havePrevis)
                unfold.ShowPrevis(gridPC.canUnfold ? mat_canUnfold : mat_cannotUnfold);
            havePrevis = true;
        }
    }

    public bool ReachedDestination()
    {
        return (this.transform.position - player.transform.position).sqrMagnitude < 0.1f;
    }

}
