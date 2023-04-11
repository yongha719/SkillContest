using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectsOfType(typeof(T)) as T;
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = (T)this;
    }


    private void OnDestroy()
    {
        instance = null;
    }
}
