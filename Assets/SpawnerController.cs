using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private GameObject botPrefab; // Prefab del clon.
    [SerializeField] private Transform[] waypoints; // Lista de puntos de destino.
    [SerializeField] private Transform exitPoint; // Punto de salida.
    [SerializeField] private float spawnInterval = 3.0f; // Intervalo de aparición.
    [SerializeField] private float delayAtCaja = 1.0f; // Retraso en Caja antes de ir a la salida.
    private List<Transform> availableWaypoints = new List<Transform>();
    private List<BotController> spawnedBots = new List<BotController>();

    private void Start()
    {
        availableWaypoints.AddRange(waypoints);
    }

    private void FixedUpdate()
    {
        StartCoroutine(SpawnBots());
    }

    private IEnumerator SpawnBots()
    {
        while (true)
        {
            if (availableWaypoints.Count > 0)
            {
                Transform spawnPoint = availableWaypoints[0];
                availableWaypoints.RemoveAt(0);
                GameObject newBot = Instantiate(botPrefab, spawnPoint.position, Quaternion.identity);
                BotController botController = newBot.GetComponent<BotController>();
                botController.Init(this, spawnPoint, exitPoint, delayAtCaja);
                spawnedBots.Add(botController);
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void NotifyBotFinished(BotController bot)
    {
        if (spawnedBots.Contains(bot))
        {
            int index = spawnedBots.IndexOf(bot);
            if (index + 1 < spawnedBots.Count)
            {
                BotController nextBot = spawnedBots[index + 1];
                nextBot.StartMoving();
            }
        }
    }

    public void RemoveBot(BotController bot)
    {
        if (spawnedBots.Contains(bot))
        {
            spawnedBots.Remove(bot);
            Destroy(bot.gameObject);

            for (int i = 0; i < spawnedBots.Count; i++)
            {
                spawnedBots[i].StartMoving();
            }

            StartCoroutine(SpawnNewBotAfterDelay(5.0f));
        }
    }

    private IEnumerator SpawnNewBotAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnBots());
    }
}


