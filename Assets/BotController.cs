using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    private SpawnerController spawnerController;
    private Transform currentWaypoint;
    private Transform exitPoint;
    private float delayAtCaja;
    private NavMeshAgent navMeshAgent;
    public float distanceThreshold = 0.1f;

    private Transform spawnPoint;

    public void Init(SpawnerController spawner, Transform startWaypoint, Transform exit, float delay)
    {
        this.spawnerController = spawner;
        currentWaypoint = startWaypoint;
        exitPoint = exit;
        delayAtCaja = delay;
        navMeshAgent = GetComponent<NavMeshAgent>();
        StartMoving();
        this.spawnerController = spawner;

        // Configura el destino inicial en el punto de origen.
        navMeshAgent.SetDestination(startWaypoint.position);
        navMeshAgent.isStopped = true;
    }

    public void StartMoving()
    {
        StartCoroutine(MoveToNextWaypoint());
        StartCoroutine(StartMovingDelayed());
    }

    private IEnumerator StartMovingDelayed()
    {
        yield return new WaitForSeconds(0.5f); // Añade un pequeño retraso antes de comenzar a moverse.
        navMeshAgent.SetDestination(currentWaypoint.position);
        StartCoroutine(MoveToNextWaypoint());
        navMeshAgent.isStopped = false;
    }

    private IEnumerator MoveToNextWaypoint()
    {
        navMeshAgent.SetDestination(currentWaypoint.position);
        while (Vector3.Distance(transform.position, currentWaypoint.position) > distanceThreshold)
        {
            yield return null;
        }

        if (Vector3.Distance(transform.position, exitPoint.position) > distanceThreshold)
        {
            yield return new WaitForSeconds(delayAtCaja);
            spawnerController.RemoveBot(this);
        }
        else
        {
            spawnerController.NotifyBotFinished(this);
        }
    }

    public Transform GetWaypoint()
    {
        return currentWaypoint;
    }
}
























// using System.Collections;
// using UnityEngine;
// using UnityEngine.AI;

// public class BotController : MonoBehaviour
// {
//     [SerializeField] Transform Origen;
//     [SerializeField] Transform Caja;
//     [SerializeField] Transform Cola1;
//     [SerializeField] Transform Cola2;
//     [SerializeField] Transform Cola3;
//     [SerializeField] Transform Exit;
//     [SerializeField] NavMeshAgent navMeshAgent;
//     [SerializeField] Contants contants; // Asegúrate de asignar este componente desde el Inspector.



//     public float distanceThreshold = 0.1f; // Umbral de distancia para determinar si ha llegado al destino.

//     private void Start()
//     {
//         navMeshAgent = GetComponent<NavMeshAgent>();
//         contants.Ismoving = true;
//         navMeshAgent.SetDestination(Origen.position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * 2.0f);
//         Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
//     }

//     private void Update()
//     {
//         CambiarDestino();
//     }

//     private void CambiarDestino()
//     {
//         // MOVIMIENTO INICIAL
//         if (contants.Ismoving == true && Vector3.Distance(transform.position, Origen.position) < distanceThreshold)
//         {
//             contants.Ismoving = false;
//             navMeshAgent.SetDestination(Cola3.position);
//             Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
//             contants.Complete4 = true;
//         }

//         // MOVIMIENTO ULTIMO EN LA FILA
//         if (contants.Complete4 == true && Vector3.Distance(transform.position, Cola3.position) < distanceThreshold)
//         {
//             contants.Complete4 = false;
//             navMeshAgent.SetDestination(Cola2.position);
//             Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
//             contants.Complete3 = true;
//         }

//         // MOVIMIENTO PENÚLTIMO EN LA FILA
//         if (contants.Complete3  == true && Vector3.Distance(transform.position, Cola2.position) < distanceThreshold)
//         {
//             contants.Ismoving = false;
//             navMeshAgent.SetDestination(Cola1.position);
//             contants.Complete2 = true;

//         }

//         // MOVIMIENTO PRIMERO EN LA FILA
//         if (contants.Complete2  == true && Vector3.Distance(transform.position, Cola1.position) < distanceThreshold)
//         {
//             contants.Ismoving = false;
//             navMeshAgent.SetDestination(Caja.position);
//             contants.Complete1 = true;
//         }

//         // MOVIMIENTO EN CAJA HACIA LA SALIDA
//         if (contants.Complete1 == true && Vector3.Distance(transform.position, Caja.position) < distanceThreshold)
//         {
//             contants.Ismoving = true;
//             navMeshAgent.SetDestination(Exit.position);
//             Debug.Log("Ha llegado al estado de Caja");
//             if(Vector3.Distance(transform.position, Exit.position) < distanceThreshold)
//             {
//                 Destroy(this.gameObject);
//             }
//         }
//     }
// }
