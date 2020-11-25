using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float timer;
    public float randomRange = 0;

    void Start()
    {
        Destroy(this.gameObject, timer + randomRange * (Random.value*2.0f -1.0f));
    }

}
