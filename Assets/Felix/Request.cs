using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Request : MonoBehaviour
{

    public Transform targetPosition; // Posición específica a la que el personaje debe llegar.
    public float stoppingDistance = 2.0f; // Distancia de parada cercana al objetivo.
    public string characterName = "Character"; // Nombre del personaje para el archivo JSON.
    private NavMeshAgent navMeshAgent;
    private bool isWaiting = true;
    public List<int> generatedNumbers = new List<int>(); // Almacenar los números generados, 3 de ellos.
    [SerializeField] List<int> nums;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (isWaiting)
        {
            Debug.Log("generar");
            GenerateRandomNumbers(); // Generar números si el personaje está esperando.
        }

        // StartCoroutine(CheckTransaction()); CHEQUEAR ERRORES
    }

    private void Update()
    {
        if (!isWaiting)
        {
            // Moverse hacia el objetivo.
            navMeshAgent.SetDestination(targetPosition.position);
        }
    }

    private void GenerateRandomNumbers()
    {
        for (int i = 0; i < nums.Count; i++)
        {
            int rand = nums[Random.Range(0, 11)];
            generatedNumbers[i] = rand;
            nums.Remove(rand);
        }
    }
}
