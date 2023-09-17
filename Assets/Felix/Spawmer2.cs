using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawmer2 : MonoBehaviour
{
    public string targetTag = "NPC"; // Etiqueta del GameObject que deseas reemplazar.
    public GameObject replacementPrefab; // Prefab para reemplazar el GameObject desaparecido.
    public float respawnDelay = 1.0f; // Tiempo de espera antes de que se reemplace el GameObject.

    private void Start()
    {
        // Iniciar una rutina para verificar continuamente si el GameObject con la etiqueta targetTag desaparece.
        StartCoroutine(CheckAndRespawn());
    }

    private IEnumerator CheckAndRespawn()
    {
        while (true)
        {
            // Buscar todos los GameObjects con la etiqueta targetTag.
            GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(targetTag);

            // Si no hay objetos con la etiqueta, instanciar uno nuevo.
            if (objectsWithTag.Length == 0)
            {
                SpawnReplacement();
            }

            // Esperar antes de volver a verificar.
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    private void SpawnReplacement()
    {
        // Instanciar un nuevo GameObject usando el replacementPrefab en la posición actual.
        GameObject newNPC = Instantiate(replacementPrefab, transform.position, transform.rotation);
        newNPC.transform.SetParent(transform); // Hacer al nuevo NPC hijo del Spawner.
        newNPC.SetActive(true);
        
    }
}
