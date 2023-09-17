using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawmer : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // Posición inicial.
    [SerializeField] private GameObject objectPrefab; // Prefab del objeto a spawnear.
    [SerializeField] private int maxObjectsToSpawn = 8; // Número límite de personajes.
    [SerializeField] private float spawnInterval = 1.0f; // Intervalo de tiempo entre cada spawneo.
    public bool IsBuying = false;

    public List<GameObject> spawnedObjects = new List<GameObject>();

    private static Spawmer instance;

    // Propiedad estática para acceder a la instancia única.
    public static Spawmer Instance
    {
        get { return instance; }
    }

    private void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    private IEnumerator SpawnNPCs()
    {
        while (true)
        {
            if (spawnedObjects.Count < maxObjectsToSpawn)
            {
                GameObject newNPC = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
                newNPC.transform.SetParent(transform); // Hacer al nuevo NPC hijo del Spawner.

                spawnedObjects.Add(newNPC);

                // Comprueba si se alcanzó el límite de NPCs y si IsBuying es falso.
                if (spawnedObjects.Count == maxObjectsToSpawn && !IsBuying)
                {
                    // Selecciona aleatoriamente un NPC para comprar.
                    int randomIndex = Random.Range(0, spawnedObjects.Count);
                    GameObject npcToBuy = spawnedObjects[randomIndex];
                    Debug.Log("Se ha seleccionado comprar: " + npcToBuy.name);

                    // Activa la variable IsBuying.
                    IsBuying = true;
                }
            }

            // Espera al siguiente intervalo antes de intentar spawnear otro NPC.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Llamado cuando un NPC es destruido CONTAINS de la lista array que no existe
    public void OnNPCDestroyed(GameObject npc)
    {
        if (spawnedObjects.Contains(npc))
        {
            spawnedObjects.Remove(npc);

            // Comprueba si se ha reducido el número de NPCs interados en escena.
            if (spawnedObjects.Count < maxObjectsToSpawn)
            {
                Debug.Log("Se ha reducido el número de NPCs interados. Spawneará nuevos NPCs.");
            }
        }
    }
}

