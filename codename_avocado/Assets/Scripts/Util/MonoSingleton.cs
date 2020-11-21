using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T s_instance;
    public static T Instance
    {
        get
        {
            if (s_instance == null)
            {
                //first try to find it in the existing scene
                s_instance = FindObjectOfType<T>();
                if (s_instance == null)
                {
                    s_instance = GetOrCreateInstance();
                    //Debug.LogErrorFormat("No instance of {0} found.  This should be instantiated at runtime.", typeof(T).ToString());
                }
            }
            return s_instance;
        }
    }

    /// <summary>
    /// This version does not use FindObjectOfType and should be used when not on the main thread
    /// </summary>
    public static T DirectInstance
    {
        get
        {
            return s_instance;
        }
    }

    public static bool HasInstance
    {
        get
        {
            return s_instance != null || FindObjectOfType<T>() != null;
        }
    }

    public static T GetOrCreateInstance()
    {
        if (!HasInstance)
        {
            //first try to find it in the existing scene
            GameObject createdGameObject = new GameObject(typeof(T).ToString(), typeof(T));
            s_instance = createdGameObject.GetComponent<T>();
        }
        return Instance;
    }
}