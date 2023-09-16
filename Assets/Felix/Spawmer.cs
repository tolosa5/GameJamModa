using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawmer : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // Posición inicial.
    [SerializeField] private GameObject objectPrefab; // Prefab del objeto a spawnear.
    [SerializeField] private int maxObjectsToSpawn = 5; // Número límite de personajes.
    [SerializeField] private float spawnInterval = 3.0f; // Intervalo de tiempo entre cada spawneo.

    public List<GameObject> spawnedObjects = new List<GameObject>();
    private float lastSpawnTime;

    private void Start()
    {
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        //comprueba si se puede spawnear
        if (spawnedObjects.Count < maxObjectsToSpawn && Time.time - lastSpawnTime >= spawnInterval)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        // Crea una nueva instancia del objeto y añade a lista
        GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
        spawnedObjects.Add(newObject);

        // Actualiza el tiempo del último spawneo///
        lastSpawnTime = Time.time;
    }
}

