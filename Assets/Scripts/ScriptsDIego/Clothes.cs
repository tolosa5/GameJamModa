using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothes : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Sprite sprite;
    [SerializeField] string clothName;

    public int GiveId()
    {
        return id;
    }
}
