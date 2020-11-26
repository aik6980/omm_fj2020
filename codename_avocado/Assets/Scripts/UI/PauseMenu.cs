using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject root;

    //TEMP HACK
    public GridPlayerCharacter player;


    void Start()
    {
        root.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!root.activeSelf)
        {//inactive
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                root.SetActive(true);
            }
        } else
        {//active
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                root.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        root.SetActive(false);
    }

    public void Restart()
    {
        root.SetActive(false);

        Time.timeScale = 1;
        LaunchGameScript ls = LaunchGameScript.singleton;
        if (ls)
        {
            ls.levelToLoad--;
        }
        player.m_Grid.LoadNextLevel();
    }

    public void QuitToMenu()
    {
        root.SetActive(false);

        Time.timeScale = 1;
        Application.LoadLevel(0);
    }

    public void QuitGame()
    {
        root.SetActive(false);

        Time.timeScale = 1;
        Application.Quit();
    }

}
