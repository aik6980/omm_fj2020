using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCamera : MonoBehaviour
{
    public Transform m_Player;

    private Vector3 m_PlayerOffset;
    public float m_strength = 1.0f;

    private void Awake()
    {
        m_PlayerOffset = m_Player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = m_Player.position - m_PlayerOffset;
        transform.position = Vector3.Lerp(transform.position, m_Player.position - m_PlayerOffset, Time.deltaTime * m_strength);
    }
}
