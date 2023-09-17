using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (isWaiting)
        {
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
        System.Random random = new System.Random();
        for (int i = 0; i < generatedNumbers.Count; i++)
        {
            generatedNumbers[i] = random.Next(1, 11); // Números aleatorios del 1 al 10.
        }
    }
}
