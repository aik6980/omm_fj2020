using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject winMenu;
    public GameObject gameCompleteMenu;

	public void GameOver()
	{
		// reload level...
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void Success()
    {
        //ToDo: i this was the LAST level, do something different!

        winMenu?.SetActive(true);
    }

    public void Win()
    {
        gameCompleteMenu?.SetActive(true);
    }
}
