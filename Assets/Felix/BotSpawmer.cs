using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class BotSpawmer : MonoBehaviour
{
    [SerializeField] Transform inicial;

    bool Spawming = false;

    [SerializeField] int MaxBots = 5;

    int numNPCs = 0;


    private void Update()
    {
        NumberOfBots();
        IsSpawming();

        if (Spawming == true)
        {
            Spawm();
        }

    }

    void IsSpawming()
    {
        if (MaxBots >= numNPCs)
        {
            Spawming = true;
        }
        else
        {
            Spawming = false;
        }
    }
    private void Spawm()
    {
        if(Spawming == true)
        {
        // aqui adquirimos valores de la Pool
        GameObject NewBot = PoolBots.instance.GetPooledObject();

        if (NewBot != null)
        {
            NewBot.transform.position = inicial.position;
            NewBot.SetActive(true);
            Debug.Log("Se ha reiniciado un Beat sonoro de la Pool");
        }
        }
    }

    void NumberOfBots()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");
        // Obtener el número de objetos con el tag especificado
        numNPCs = npcs.Length;
        // Puedes usar numNPCs como un entero
        Debug.Log("Número de NPCs encontrados: " + numNPCs);
    }
}





