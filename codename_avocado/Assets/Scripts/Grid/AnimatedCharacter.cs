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

    public bool moving = false;
    public bool jumping = false;
    //public bool needSpawn = false;

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
    }

    void Update()
    {
        //hack
        animation.transform.localPosition = Vector3.down * 0.8f;

        float dT = Time.deltaTime;

        if (jumping)
        {
            if (animation.IsPlaying(animClips[2]))
            { //wait
                return;
            }
            else
            {
                jumping = false;
                Spawn();
            }
        }

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


        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, player.transform.rotation, 400.0f * dT);
        this.transform.position = Vector3.MoveTowards(this.transform.position, player.transform.position, 5.0f * dT);
    }

    public bool ReachedDestination()
    {
        return (this.transform.position - player.transform.position).sqrMagnitude < 0.1f;
    }

}
