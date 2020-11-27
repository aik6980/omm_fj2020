using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoWatcher : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Graphic graphic;
    public string nextScene;

    private void Awake()
    {
        videoPlayer.time = 0;
        videoPlayer.loopPointReached += source => Done();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Done();
        }
    }

    private void Done()
    {
        IEnumerator FadeOut(UnityEngine.UI.Graphic img)
        {
            float a = 0.0f;
            while (a < 1.0f)
            {
                a = Mathf.Clamp(a + (Time.unscaledDeltaTime * 1f), 0f, 1f);
                graphic.color = Color.Lerp(Color.white, Color.black, a);
                yield return null;
            }
            SceneManager.LoadScene(nextScene/*"BandaidStartMenu"*/);
        }

        StartCoroutine(FadeOut(graphic));
    }
}
