using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class APIHandler : MonoBehaviour
{
    public static APIHandler Instance;

    public FirebaseMgr  databaseAPI;
    public AuthAPI authAPI;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        Instance = this;
        databaseAPI = GetComponent<FirebaseMgr>();
        authAPI = GetComponent<AuthAPI>();

        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
