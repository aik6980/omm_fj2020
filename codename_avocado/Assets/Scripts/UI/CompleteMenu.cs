using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteMenu : MonoBehaviour
{

    void OnActivate()
    {
        Time.timeScale = 0;
    }

    void OnDeActivate()
    {
        Time.timeScale = 1;
    }
}
