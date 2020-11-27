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

        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("SceneManager_sceneLoaded " + arg0.buildIndex);
        LevelWasLoaded(arg0.buildIndex);
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


    private void LevelWasLoaded(int level)
    {
        if (level==1)
        {
            if (singleton!=null && singleton.levelToLoad > 0)
            {
                Debug.Log("back");
                //GameObject start = GameObject.Find("StartButton");
                //if (start != null)
                //    start.GetComponentInChildren<UnityEngine.UI.Button>()?.OnClick();

                GameObject mainmenu = GameObject.Find("MainMenu");
                GameObject levelmenu = GameObject.Find("LevelSelection");

                GameObject sgm = GameObject.Find("StartGameMenus");
                //sgm.GetComponentInChildren<UnityEngine.UI.Button>(true);
                for (int i = 0; i < sgm.transform.childCount; i++)
                {
                    Transform c = sgm.transform.GetChild(i);
                    Debug.Log(c.name);
                    if (c.name == "MainMenu")
                        mainmenu = c.gameObject;
                    if (c.name == "LevelSelection")
                        levelmenu = c.gameObject;
                }

                if (mainmenu!=null & levelmenu!=null)
                {
                    mainmenu.SetActive(false);
                    levelmenu.SetActive(true);
                }
            }
        }
    }
}







