using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public Button[] m_LevelButtons;

    private void Awake()
    {
        int level_at = PlayerPrefs.GetInt("LevelAt", 1);
        for (int i = 0; i < m_LevelButtons.Length; ++i)
        {
            m_LevelButtons[i].interactable = level_at > i;
        }
    }
}
