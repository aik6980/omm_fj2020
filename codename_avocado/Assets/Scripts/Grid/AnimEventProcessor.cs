using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventProcessor : MonoBehaviour
{
    public GameObject soundPrefab;
    public AudioClip[] stepSounds;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void Anim_Step()
    {
        GameObject go = Instantiate(soundPrefab, this.transform.position, this.transform.rotation);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = stepSounds[Random.Range(0, 1000) % stepSounds.Length];
        src.volume = 0.5f;
        src.Play();
        Destroy(go, src.clip.length);

    }
}
