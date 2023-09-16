using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatienceClient : MonoBehaviour
{
    private float maxPatience;
    private float currentPatience;
    private PatienceLeveler PatienceLeveler;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("owo");
        currentPatience = maxPatience;
        PatienceLeveler = GetComponentInChildren<PatienceLeveler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Patience()
    {
        Debug.Log("uwu");
        PatienceLeveler.UpdatePatience(maxPatience, currentPatience);
    }
}
