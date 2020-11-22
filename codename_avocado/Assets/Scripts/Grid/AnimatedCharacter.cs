using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedCharacter : MonoBehaviour
{
    public GameObject player;       // follow this
    public GridPlayerCharacter gridPC;
    public Animation animation;     // set the animations in this

    public Unfold unfold;           // should it be here?

    public string[] animClips;
    public GameObject[] models;

    public Transform facePivot;
    public Transform armLPivot;
    public Transform armRPivot;

    public GameObject[] faces;
    public GameObject[] arms;

    //yeah i know, will refactor it :oP
    public bool moving = false;
    public bool jumping = false;
    public bool unfolding = false;

    public GameObject face;
    public GameObject armL;
    public GameObject armR;

    public Vector3 offset;

    void Start()
    {
        if (gridPC)
        {
            gridPC.OnPlaceDelegate += OnPlace;
            gridPC.OnSpawnDelegate.AddListener(OnSpawn);
        }

        Spawn();
    }

    void OnSpawn()
    {
        if (!jumping)
            Spawn();
    }

    void OnPlace(Vector2 pos)
    {
        Debug.Log("jump", this);
        moving = false;
        jumping = true;
        animation.Play(animClips[2]);
    }

    void Spawn()
    {
        this.transform.rotation = player.transform.rotation;
        this.transform.position = player.transform.position;
        animation.gameObject.SetActive(true);
        animation.Play(animClips[0]);

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

            default:
                n = Random.Range(0, 1000) % models.Length;
                break;
        }
        for (int i = 0; i < models.Length; i++)
            models[i].SetActive(i == n);

        PickDecor();
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
                animation.gameObject.SetActive(false);
                return;
                //Spawn();
            }
        }
        if (unfolding)
        {
            if (unfold.Finished())
            {
                unfolding = false;
                unfold.UnSpawnFaces();
                Spawn();
            }
            unfold.UnfoldStep(dT * 5.0f);
            return;
        }

        if (Random.value < 0.01f)
            PickDecor();

        offset = player.transform.position - this.transform.position;
        //offset.z = ((player.transform.rotation.y - this.transform.rotation.y) + 180.0f) % 360.0f - 180.0f;

        if (!moving && offset.magnitude > 0.1f)
        {
            moving = true;
            animation.Play(animClips[1]);
        } else
        if (moving && offset.magnitude <= 0.1f)
        {
            moving = false;
            animation.Play(animClips[0]);
        }


        Quaternion faceDir = moving ? Quaternion.LookRotation(offset) : player.transform.rotation;
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, faceDir, 400.0f * dT);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 5.0f * dT);
    }

    public bool ReachedDestination()
    {
        return (this.transform.position - player.transform.position).sqrMagnitude < 0.1f;
    }

}
