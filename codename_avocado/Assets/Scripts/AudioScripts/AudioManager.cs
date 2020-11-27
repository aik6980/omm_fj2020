using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioMixer           m_masterMixer;
    public AudioMixerGroup      m_sfxMixerGroup;
    public MusicGroup[]         m_musicMixerGroup; 

    public MusicData[] music_data;
    public SFXData[] sfx_data;
    public SFXData[] toxic_sfx;

    public float toxic_sfx_interval;
    public float toxic_sfx_interval_variation;

    public float music_interval;
    public float music_interval_variation;
    public float crossfade_duration;

    Queue<int> current_playlist;

    //public float musLerpSmooth = 1;
    //public float envVolumeMultiplier = 0.1f;
    //private float musLayerVolumeMultiplier;
    //private float envLayerVolumeMultiplier;
    //private float currentMusVolume;
    //private float currentEnv1Volume;
    //private float currentEnv2Volume;
    //private float lerpdMusLayerVolume;


    void Awake()
    {
        // Get Sound Level for music and ambience
        //currentMusVolume = getSoundVolume("Mus_Layer_B");
        //currentEnv1Volume = getSoundVolume("Env_BG_Desolate");
        //currentEnv2Volume = getSoundVolume("Env_BG_Coniferous");

        //musLayerVolumeMultiplier = 0.0f;

        //foreach (Sound s in sounds)
        //{
        //    s.source = gameObject.AddComponent<AudioSource>();
        //    s.source.clip = s.clip;
        //    s.source.loop = s.loop;

        //    //s.source.outputAudioMixerGroup = mixerGroup;
        //}

        foreach (SFXData s in sfx_data)
        {
            s.audio_source = gameObject.AddComponent<AudioSource>();
            s.audio_source.clip = s.clip;
            s.audio_source.volume = s.volume;
            s.audio_source.loop = false;

            s.audio_source.outputAudioMixerGroup = m_sfxMixerGroup;
        }

        foreach (SFXData s in toxic_sfx)
        {
            s.audio_source = gameObject.AddComponent<AudioSource>();
            s.audio_source.clip = s.clip;
            s.audio_source.volume = s.volume;
            s.audio_source.loop = false;

            s.audio_source.outputAudioMixerGroup = m_sfxMixerGroup;
        }

        foreach (MusicGroup soundGroup in m_musicMixerGroup)
        {
            soundGroup.audio_sources = new AudioSource[soundGroup.soundName.Length];
            
            for(int i=0;i<soundGroup.soundName.Length;++i)
            {
                var name = soundGroup.soundName[i]; 

                MusicData s = Array.Find(music_data, item => item.name == name);
                var audioSource = gameObject.AddComponent<AudioSource>();

                audioSource.volume = s.volume;
                audioSource.clip = s.clip;
                audioSource.loop = true;
                audioSource.playOnAwake = false;
                audioSource.outputAudioMixerGroup = soundGroup.mixerGroup;

                soundGroup.audio_sources[i] = audioSource;

                audioSource.Play();
            }
            
        }


    }

    void Start()
    {
        current_playlist = GeneratePlaylist();
        // play first track
        SwitchTrack();
        // then switch track every interval
        StartCoroutine(SwitchTrackInterval());
    }

    public void PlayToxicSFX()
    {
        var i = UnityEngine.Random.Range(0, toxic_sfx.Length);

        toxic_sfx[i].audio_source.Play();
    }

    public void PlayMusic(string name)
    {
        MusicGroup music = Array.Find(m_musicMixerGroup, item => item.mixerGroup.name == name);
        if (music == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        //foreach(AudioSource source in music.audio_sources)
        //{
        //    source.UnPause();
        //}

        var result = m_masterMixer.SetFloat(name+"_vol", 0.0f);
    }
    public void StopMusic(string name)
    {
        MusicGroup music = Array.Find(m_musicMixerGroup, item => item.mixerGroup.name == name);
        if (music == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        //foreach (AudioSource source in music.audio_sources)
        //{
        //    source.Pause();
        //}

        m_masterMixer.SetFloat(name + "_vol", -80.0f);
    }

    public void PlaySFX(string sound)
    {
        SFXData s = Array.Find(sfx_data, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        //s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        s.audio_source.Play();
    }

    private void Update()
    {

    }
    Queue<int> GeneratePlaylist()
    {
        int[] indexArray = new int[m_musicMixerGroup.Length];
        // Fill with sequeential indexes then randomize
        for (int i = 0; i < indexArray.Length; ++i)
        {
            indexArray[i] = i;
        }
        RandomizeArray<int>(indexArray);
        return new Queue<int>(indexArray);
    }

    void SwitchTrack()
    {
        var current_track = current_playlist.Dequeue();
        if (current_playlist.Count == 0)
        {
            current_playlist = GeneratePlaylist();
        }

        for (int i = 0; i < m_musicMixerGroup.Length; ++i)
        {
            var mixerGroup_name = m_musicMixerGroup[i].mixerGroup.name;
            if (i == current_track)
            {
                StartCoroutine(StartFade(m_masterMixer, mixerGroup_name + "_vol", crossfade_duration, 1.0f));
            }
            else
            {
                StartCoroutine(StartFade(m_masterMixer, mixerGroup_name + "_vol", crossfade_duration, 0.0f));
            }
        }
    }

    IEnumerator SwitchTrackInterval()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        
        while(true)
        {

            var time_variation = UnityEngine.Random.Range(-1.0f, 1.0f) * music_interval_variation;
            var random_interval = music_interval + time_variation;

            yield return new WaitForSeconds(random_interval);

            SwitchTrack();
        }
    }


    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        float currentVol;
        audioMixer.GetFloat(exposedParam, out currentVol);
        currentVol = Mathf.Pow(10, currentVol / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            //audioMixer.SetFloat(exposedParam, newVol);

            yield return null;
        }
        yield break;
    }

    public static void RandomizeArray<T>(T[] array)
    {
        int size = array.Length;
        for (int i = 0; i < size; i++)
        {
            int indexToSwap = UnityEngine.Random.Range(i, size);
            T swapValue = array[i];
            array[i] = array[indexToSwap];
            array[indexToSwap] = swapValue;
        }
    }

    //IEnumerator FadeSoundIn()
    //{
    //    Sound s = Array.Find(sounds, item => item.name == "Mus_Layer_B");
    //    Sound s1 = Array.Find(sounds, item => item.name == "Env_BG_Desolate");
    //    Sound s2 = Array.Find(sounds, item => item.name == "Env_BG_Coniferous");

    //    while (s.source.volume < 1.0f)
    //    {
    //        s.source.volume += Time.deltaTime / musLerpSmooth;
    //        s1.source.volume -= Time.deltaTime / musLerpSmooth;
    //        s2.source.volume += ((Time.deltaTime / musLerpSmooth) * envVolumeMultiplier);
    //        yield return null;
    //    }
    //}

    //IEnumerator FadeSoundOut()
    //{
    //    Sound s = Array.Find(sounds, item => item.name == "Mus_Layer_B");
    //    Sound s1 = Array.Find(sounds, item => item.name == "Env_BG_Desolate");
    //    Sound s2 = Array.Find(sounds, item => item.name == "Env_BG_Coniferous");

    //    while (s.source.volume > 0.01f)
    //    {
    //        s.source.volume -= Time.deltaTime / musLerpSmooth;
    //        s1.source.volume += ((Time.deltaTime / musLerpSmooth) * envVolumeMultiplier);
    //        s2.source.volume -= Time.deltaTime / musLerpSmooth;
    //        yield return null;
    //    }
    //}
}
