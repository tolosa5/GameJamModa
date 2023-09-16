// using System;
// using System.Collections;
// using System.IO;
// using UnityEngine;
// using UnityEngine.AI;

// public class Request : MonoBehaviour
// {
//     [SerializeField, Range(1,10)] int RangoNum = 5;


//     public Transform targetPosition; // Posición específica a la que el personaje debe llegar.
//     public float stoppingDistance = 2.0f; // Distancia de parada cercana al objetivo.
//     public string characterName = "Character"; // Nombre del personaje para el archivo JSON.
//     private NavMeshAgent navMeshAgent;
//     private bool isWaiting = true;
//     private int[] generatedNumbers = new int[3]; // Almacenar los números generados.
//     private const string saveFileName = "CharacterData.json"; // Nombre del archivo JSON.
//     private CharacterData characterData; // Datos del personaje.

//     [Serializable]
//     private class CharacterData
//     {
//         public int[] generatedNumbers; // Números generados para el personaje.
//     }

//     private void Start()
//     {
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         LoadCharacterData(); // Cargar datos del personaje al inicio.

//         if (isWaiting)
//         {
//             GenerateRandomNumbers(); // Generar números si el personaje está esperando.
//             SaveCharacterData(); // Guardar los nuevos datos generados.
//         }

//         StartCoroutine(CheckTransaction());
//     }

//     private void Update()
//     {
//         if (!isWaiting)
//         {
//             // Moverse hacia el objetivo.
//             navMeshAgent.SetDestination(targetPosition.position);
//         }
//     }

//     private void GenerateRandomNumbers()
//     {
//         System.Random random = new System.Random();
//         for (int i = 0; i < generatedNumbers.Length; i++)
//         {
//             generatedNumbers[i] = random.Next(1, 11); // Números aleatorios del 1 al 10.
//         }
//     }

//     private void SaveCharacterData()
//     {
//         characterData = new CharacterData
//         {
//             generatedNumbers = generatedNumbers
//         };

//         string json = JsonUtility.ToJson(characterData);
//         File.WriteAllText(saveFileName, json);
//     }

//     private void LoadCharacterData()
//     {
//         if (File.Exists(saveFileName))
//         {
//             string json = File.ReadAllText(saveFileName);
//             characterData = JsonUtility.FromJson<CharacterData>(json);
//             generatedNumbers = characterData.generatedNumbers;
//         }
//     }

//     private IEnumerator CheckTransaction()
//     {
//         yield return new WaitForSeconds(1.0f); // Espera para asegurarse de que el personaje se haya detenido.

//         // Verificar si los números coinciden con los del BAG.
//         GameObject bag = GameObject.Find("BAG");
//         if (bag != null)
//         {
//             BagScript bagScript = bag.GetComponent<BagScript>();
//             if (bagScript != null && BagContainsNumbers(bagScript))
//             {
//                 Debug.Log("Transacción completada.");
//                 isWaiting = true;
//                 navMeshAgent.isStopped = false;
//             }
//         }
//     }

//     private bool BagContainsNumbers(BagScript bagScript)
//     {
//         foreach (int num in generatedNumbers)
//         {
//             if (!bagScript.ContainsNumber(num))
//             {
//                 return false;
//             }
//         }
//         return true;
//     }
// }
