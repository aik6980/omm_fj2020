using System;
using UnityEngine.Audio;
using UnityEngine;

[Serializable]
public class SFXData
{
    public string name;
    public AudioClip clip;

    public float volume;

    [HideInInspector]
    public AudioSource audio_source;
}

[Serializable]
public class MusicData
{
    public string name;
    public AudioClip clip;
    public float volume;
}

[Serializable]
public class MusicGroup
{
    public AudioMixerGroup mixerGroup;

    public string[] soundName;
    [HideInInspector]
    public AudioSource[] audio_sources;
}