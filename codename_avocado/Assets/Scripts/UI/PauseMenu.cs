using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject root;


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

    public void QuitToMenu()
    {
        Application.LoadLevel(0);
    }
}
