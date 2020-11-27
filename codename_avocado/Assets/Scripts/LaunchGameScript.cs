using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchGameScript : MonoBehaviour
{
    public static LaunchGameScript singleton;

    public int levelToLoad = 0;

    public void Awake()
    {
        if (singleton != null) return;
        singleton = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LaunchGameScene1()
    {
        SceneManager.LoadScene("BandaidGameScene");
    }

    public void LaunchGameScene(int num)
    {
        //Debug.Log("launch level " + num);
        singleton.levelToLoad = num;

        SceneManager.LoadScene("BandaidGameScene");
        //StartCoroutine(LaunchLevel(num));
    }

    public void LaunchGameSceneWithIntro(int num)
    {
        singleton.levelToLoad = num;

        SceneManager.LoadScene("IntroMovie");
    }

    /*
    IEnumerator LaunchLevel(int num)
    {
        SceneManager.LoadScene("BandaidGameScene");

        yield return new WaitForEndOfFrame();

        LevelLoader loader =
    }
    */
}







