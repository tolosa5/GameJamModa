using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contants : MonoBehaviour
{
    public static Contants instance;

    public bool Ismoving = false;
    public bool Complete4 = false;
    public bool Complete3 = false;

    public bool Complete2 = false;

    public bool Complete1 = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}

