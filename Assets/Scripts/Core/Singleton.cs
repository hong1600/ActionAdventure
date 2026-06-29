using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T instance
    {
        get { return  _instance; }
    }

    protected virtual void Awake()
    {
        T current = this as T;

        if (_instance == null)
        {
            _instance = current;
            return;
        }

        if (_instance == current) return;

        Destroy(this.gameObject);
    }
}
