using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventProcessor : MonoBehaviour
{
    public GameObject soundPrefab;

    public AudioClip[] spawnSounds;
    public AudioClip[] stepSounds;
    public AudioClip[] jumpSounds;
    public AudioClip[] failSounds;      //play when tying but cannot unfold
    public AudioClip[] unfoldSounds;
    public AudioClip[] deathSounds;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void PlaySoundFrom(ref AudioClip[] sounds, float volume = 1.0f)
    {
        if (sounds == null || sounds.Length <= 0) return;

        GameObject go = Instantiate(soundPrefab, this.transform.position, this.transform.rotation);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = sounds[Random.Range(0, 1000) % sounds.Length];
        src.volume = volume;
        src.Play();
        Destroy(go, src.clip.length);
    }

    void Anim_Step()
    {
        PlaySoundFrom(ref stepSounds, 0.5f);
        /*
        GameObject go = Instantiate(soundPrefab, this.transform.position, this.transform.rotation);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = stepSounds[Random.Range(0, 1000) % stepSounds.Length];
        src.volume = 0.5f;
        src.Play();
        Destroy(go, src.clip.length);
        */
    }
    void Anim_Jump()
    {
        PlaySoundFrom(ref jumpSounds, 1.0f);
        /*
        GameObject go = Instantiate(soundPrefab, this.transform.position, this.transform.rotation);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = jumpSounds[Random.Range(0, 1000) % jumpSounds.Length];
        src.volume = 1.0f;
        src.Play();
        Destroy(go, src.clip.length);
        */
    }
    void Anim_Unfold()
    {
        PlaySoundFrom(ref unfoldSounds, 1.0f);
        /*
        Debug.Log("unfold");
        GameObject go = Instantiate(soundPrefab, this.transform.position, this.transform.rotation);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = unfoldSounds[Random.Range(0, 1000) % unfoldSounds.Length];
        src.volume = 1.0f;
        src.Play();
        Destroy(go, src.clip.length);
        */
    }

    void Anim_Death()
    {
        PlaySoundFrom(ref deathSounds, 1.0f);
        /*
        GameObject go = Instantiate(soundPrefab, this.transform.position, this.transform.rotation);
        AudioSource src = go.GetComponent<AudioSource>();
        src.clip = unfoldSounds[Random.Range(0, 1000) % unfoldSounds.Length];
        src.volume = 1.0f;
        src.Play();
        Destroy(go, src.clip.length);
        */
    }

    void Anim_Spawn()
    {
        PlaySoundFrom(ref spawnSounds, 1.0f);
    }

    void Anim_Fail()
    {
        PlaySoundFrom(ref failSounds, 1.0f);
    }
}
