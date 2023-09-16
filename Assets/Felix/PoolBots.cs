using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBots : MonoBehaviour
{
    public static PoolBots instance;
    private List<GameObject> pooledBots = new List<GameObject>();
    [SerializeField, Range(2, 5)] private int amountToPool = 3;
    [SerializeField] GameObject BotsPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // Resto de la inicialización de la pol de objetos
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject Bots = Instantiate(BotsPrefab);
            Bots.SetActive(false);
            pooledBots.Add(Bots);
            Bots.transform.parent = transform; // Establecer el padre del objeto instanciado como el GameObject actual
            // nuevaNota.transform.localScale = new Vector3(NotaScale, NotaScale, NotaScale);
            // nuevaNota.SetActive(true);

        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledBots.Count; i++)
        {
            if (!pooledBots[i].activeInHierarchy)
            {
                return pooledBots[i];
            }

        }
        return null;
    }

    
}
