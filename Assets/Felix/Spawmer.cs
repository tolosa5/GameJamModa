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

    Request request;

    public List<GameObject> spawnedClients = new List<GameObject>();

    private static Spawmer instance;

    // Propiedad estática para acceder a la instancia única.
    public static Spawmer Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    private void FixedUpdate()
    {
        OnNPCDestroyed();    
    }

    private IEnumerator SpawnNPCs()
    {
        while (true)
        {
            // Remueve cualquier objeto nulo de la lista
            spawnedClients.RemoveAll(item => item == null);

            if (spawnedClients.Count < maxObjectsToSpawn)
            {
                GameObject newNPC = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);
                newNPC.transform.SetParent(transform); // Hacer al nuevo NPC hijo del Spawner.
                newNPC.SetActive(true);

                spawnedClients.Add(newNPC);

                // Comprueba si se alcanzó el límite de NPCs y si IsBuying es falso.
                if (spawnedClients.Count == maxObjectsToSpawn && !IsBuying)
                {
                    // Selecciona aleatoriamente un NPC para comprar.
                    int randomIndex = Random.Range(0, spawnedClients.Count);
                    GameObject npcToBuy = spawnedClients[randomIndex];
                    Debug.Log("Se ha seleccionado comprar: " + npcToBuy.name);
                    RandonMovement NPCRandon = npcToBuy.GetComponent<RandonMovement>();
                    ToBuy toBuy = npcToBuy.GetComponent<ToBuy>();

                    // Activa la variable IsBuying.
                    IsBuying = true;
                    NPCRandon.enabled = false;
                    toBuy.enabled = true;

                    // Iniciar una nueva rutina para comprobar si se destruye el NPC seleccionado.
                    StartCoroutine(CheckNPCDestroyed(npcToBuy));
                }
            }

            // Espera al siguiente intervalo antes de intentar spawnear otro NPC.
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Llamado cuando un NPC es destruido
    public void OnNPCDestroyed()
    {
        if (spawnedClients.Count < maxObjectsToSpawn)
        {
            IsBuying = false; // Restablece IsBuying a false.
            StartCoroutine(SpawnNPCs());
            Debug.Log("Se ha reducido el número de NPCs interados. Spawneará nuevos NPCs.");
        }
    }

    private IEnumerator CheckNPCDestroyed(GameObject npc)
    {
        // Esperar hasta que el NPC seleccionado sea destruido.
        yield return new WaitForSeconds(0.1f);

        if (npc == null)
        {
            // Si el NPC se destruyó, restablecer la lógica de selección y activarla nuevamente.
            IsBuying = false;

            // También puedes reiniciar el ciclo de spawn en caso de ser necesario.
            StartCoroutine(SpawnNPCs());
        }
    }
}
