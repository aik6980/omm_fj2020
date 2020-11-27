using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoWatcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer.loopPointReached += source => SceneManager.LoadScene("BandaidStartMenu");
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("BandaidStartMenu");
        }
    }
}
