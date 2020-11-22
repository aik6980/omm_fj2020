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

    public bool moving = false;
    public bool jumping = false;
    //public bool needSpawn = false;

    public GameObject face;
    public GameObject armL;
    public GameObject armR;

    public Vector3 offset;

    void Start()
    {
        if (gridPC)
        {
            gridPC.OnPlaceDelegate += OnPlace;
            gridPC.OnSpawnDelegate += OnSpawn;
        }

        Spawn();
    }

    void OnSpawn()
    {
        if (!jumping)
            Spawn();
        //needSpawn = true;
        //this.transform.rotation = player.transform.rotation;
        //this.transform.position = player.transform.position;
        //animation.Play(animClips[0]);
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
        animation.Play(animClips[0]);
        int n = Random.Range(0, 1000) % models.Length;
        for (int i = 0; i < models.Length; i++)
            models[i].SetActive(i == n);

        PickDecor();
    }

    void PickDecor()
    {
        Debug.Log("pick");
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
            if (animation.IsPlaying(animClips[2]))
            { //wait
                Debug.Log("jumping...");
                return;
            }
            else
            {
                Debug.Log("jump done.");
                jumping = false;
                Spawn();
            }
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

}
