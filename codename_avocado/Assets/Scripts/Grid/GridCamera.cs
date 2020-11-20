using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCamera : MonoBehaviour
{
    public GridPlayerCharacter m_Player;

    private Vector3 m_PlayerOffset;

    private void Awake()
    {
        m_PlayerOffset = m_Player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.transform.position - m_PlayerOffset;
    }
}
