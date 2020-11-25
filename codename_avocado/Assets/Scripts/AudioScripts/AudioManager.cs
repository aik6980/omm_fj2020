using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;


    public float musLerpSmooth = 1;
    public float envVolumeMultiplier = 0.1f;
    private float musLayerVolumeMultiplier;
    private float envLayerVolumeMultiplier;
    private float currentMusVolume;
    private float currentEnv1Volume;
    private float currentEnv2Volume;
    private float lerpdMusLayerVolume;


    void Awake()
    {
        // Get Sound Level for music and ambience
        currentMusVolume = getSoundVolume("Mus_Layer_B");
        currentEnv1Volume = getSoundVolume("Env_BG_Desolate");
        currentEnv2Volume = getSoundVolume("Env_BG_Coniferous");

        musLayerVolumeMultiplier = 0.0f;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    void Start()
    {
        Play("Mus_Layer_A");
        Play("Mus_Layer_B");
        Play("Env_BG_Desolate");
        Play("Env_BG_Coniferous");

        Sound s = Array.Find(sounds, item => item.name == "Env_BG_Coniferous");
        s.source.volume = 0.0f;
        Sound s2 = Array.Find(sounds, item => item.name == "Env_BG_Desolate");
        s2.source.volume = envVolumeMultiplier;
    }

    float getSoundVolume(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }

        return s.volume;
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.source.Play();
    }

    private void Update()
    {
        // REPLACE WITH LOGIC FROM GAME (WHEN GRASS COVERS ARBITRARY % OF THE LEVEL)
        if (musLayerVolumeMultiplier == 0.0f)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                musLayerVolumeMultiplier = 1.0f;
                Debug.Log("'Healed' Ambience/Music triggered");
                StartCoroutine("FadeSoundIn");
            }

        }
        if (musLayerVolumeMultiplier == 1.0f)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                musLayerVolumeMultiplier = 0.0f;
                Debug.Log("'Desolate' Ambience/Music triggered");
                StartCoroutine("FadeSoundOut");
            }
        }
    }

    IEnumerator FadeSoundIn()
    {
        Sound s = Array.Find(sounds, item => item.name == "Mus_Layer_B");
        Sound s1 = Array.Find(sounds, item => item.name == "Env_BG_Desolate");
        Sound s2 = Array.Find(sounds, item => item.name == "Env_BG_Coniferous");

        while (s.source.volume < 1.0f)
        {
            s.source.volume += Time.deltaTime / musLerpSmooth;
            s1.source.volume -= Time.deltaTime / musLerpSmooth;
            s2.source.volume += ((Time.deltaTime / musLerpSmooth) * envVolumeMultiplier);
            yield return null;
        }
    }

    IEnumerator FadeSoundOut()
    {
        Sound s = Array.Find(sounds, item => item.name == "Mus_Layer_B");
        Sound s1 = Array.Find(sounds, item => item.name == "Env_BG_Desolate");
        Sound s2 = Array.Find(sounds, item => item.name == "Env_BG_Coniferous");

        while (s.source.volume > 0.01f)
        {
            s.source.volume -= Time.deltaTime / musLerpSmooth;
            s1.source.volume += ((Time.deltaTime / musLerpSmooth) * envVolumeMultiplier);
            s2.source.volume -= Time.deltaTime / musLerpSmooth;
            yield return null;
        }
    }
}
