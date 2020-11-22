using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    public WorldGrid m_Grid;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            WorldBuilder.GetOrCreateInstance().BuildLevel(m_Grid, 1);
        }
    }
}
