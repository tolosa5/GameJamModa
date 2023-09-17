using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OYENTE : MonoBehaviour
{
    public RandonMovement scriptA; // Asigna el componente ScriptA en el Inspector.
    public ToBuy scriptB;
    public Request Request;
    [SerializeField] private float minValue = 0.5f;
    [SerializeField] private int maxValue = 4;

    private void Start()
    {
        

        if (scriptA != null)
        {
            scriptA.enabled = false;
        }
        Random.Range(minValue, maxValue + 1);
    }
}

