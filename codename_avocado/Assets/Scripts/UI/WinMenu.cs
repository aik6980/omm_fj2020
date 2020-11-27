using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public GameObject UI_root;

    //TEMP HACK
    public GridPlayerCharacter player;


    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    void OnActivate()
    {
        Time.timeScale = 0;
        Debug.Log("Win_on");
        UI_root.SetActive(true);

        AudioManager.GetOrCreateInstance().PlaySFX("UI_Level_Complete");

        //player.m_Grid.LoadNextLevel();

    }

    void OnDeActivate()
    {
        Time.timeScale = 1;
        Debug.Log("Win_off");
        UI_root.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetAxisRaw("Jump") > 0.1f)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        player.m_Grid.LoadNextLevel();
        this.gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        LaunchGameScript ls = LaunchGameScript.singleton;
        if (ls)
        {
            ls.levelToLoad--;
        }
        player.m_Grid.LoadNextLevel();
        this.gameObject.SetActive(false);
    }

    public void QuitToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("BandaidStartMenu");
    }

}
