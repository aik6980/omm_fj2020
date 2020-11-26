using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioMixer           m_masterMixer;
    public AudioMixerGroup      m_sfxMixerGroup;
    public MusicGroup[]         m_musicMixerGroup; 

    public MusicData[] music_data;
    public SFXData[] sfx_data;

    public float music_interval = 5;
    public float music_interval_variation;
    int current_track = 0;

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

        //PlaySFX("UI_Level_Complete");
    }

    void Start()
    {
        //Play("Mus_Layer_A");
        //Play("Mus_Layer_B");
        //Play("Env_BG_Desolate");
        //Play("Env_BG_Coniferous");

        //Sound s = Array.Find(sounds, item => item.name == "Env_BG_Coniferous");
        //s.source.volume = 0.0f;
        //Sound s2 = Array.Find(sounds, item => item.name == "Env_BG_Desolate");
        //s2.source.volume = envVolumeMultiplier;

        StartCoroutine(SwitchTrackInterval());
    }

    //float getSoundVolume(string sound)
    //{
    //    Sound s = Array.Find(sounds, item => item.name == sound);
    //    if (s == null)
    //    {
    //        Debug.LogWarning("Sound: " + name + " not found!");
    //    }

    //    return s.volume;
    //}

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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //PlayMusic("Music_a");
            //StopMusic("Music_b");

            //StartCoroutine(StartFade(m_masterMixer, "Music_a_vol", 1.0f, 0.0f));
            //StartCoroutine(StartFade(m_masterMixer, "Music_b_vol", 3.0f, 1.0f));
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //PlayMusic("Music_b");
            //StopMusic("Music_a");

            //StartCoroutine(StartFade(m_masterMixer, "Music_a_vol", 3.0f, 0.0f));
            //StartCoroutine(StartFade(m_masterMixer, "Music_b_vol", 1.0f, 1.0f));
        }

        // REPLACE WITH LOGIC FROM GAME (WHEN GRASS COVERS ARBITRARY % OF THE LEVEL)
        //if (musLayerVolumeMultiplier == 0.0f)
        //{
        //    if (Input.GetKey(KeyCode.UpArrow))
        //    {
        //        musLayerVolumeMultiplier = 1.0f;
        //        Debug.Log("'Healed' Ambience/Music triggered");
        //        StartCoroutine("FadeSoundIn");
        //    }

        //}
        //if (musLayerVolumeMultiplier == 1.0f)
        //{
        //    if (Input.GetKey(KeyCode.DownArrow))
        //    {
        //        musLayerVolumeMultiplier = 0.0f;
        //        Debug.Log("'Desolate' Ambience/Music triggered");
        //        StartCoroutine("FadeSoundOut");
        //    }
        //}
    }

    IEnumerator SwitchTrackInterval()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        
        while(true)
        {
            var next_track = (current_track + 1) % m_musicMixerGroup.Length;

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(music_interval);

            for (int i = 0; i < m_musicMixerGroup.Length; ++i)
            {
                var mixerGroup_name = m_musicMixerGroup[i].mixerGroup.name;
                if (i == next_track)
                {
                    StartCoroutine(StartFade(m_masterMixer, mixerGroup_name + "_vol", 3.0f, 1.0f));
                }
                else
                {
                    StartCoroutine(StartFade(m_masterMixer, mixerGroup_name + "_vol", 3.0f, 0.0f));
                }
            }

            current_track = next_track;
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
