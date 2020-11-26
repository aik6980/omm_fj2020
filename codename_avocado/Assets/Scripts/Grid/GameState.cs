using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public GameObject winMenu;

	public void GameOver()
	{
		// reload level...
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

    public void Success()
    {
        winMenu?.SetActive(true);
    }
}
