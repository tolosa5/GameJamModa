using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    [SerializeField] Transform Origen;
    [SerializeField] Transform Caja;
    [SerializeField] Transform Cola1;
    [SerializeField] Transform Cola2;
    [SerializeField] Transform Cola3;
    [SerializeField] Transform Exit;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Contants contants; // Asegúrate de asignar este componente desde el Inspector.

    
    
    public float distanceThreshold = 0.1f; // Umbral de distancia para determinar si ha llegado al destino.

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        contants.Ismoving = true;
        navMeshAgent.SetDestination(Origen.position + new Vector3(Random.insideUnitCircle.x, 0f, Random.insideUnitCircle.y) * 2.0f);
        Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
    }

    private void Update()
    {
        CambiarDestino();
    }

    private void CambiarDestino()
    {
        // MOVIMIENTO INICIAL
        if (contants.Ismoving == true && Vector3.Distance(transform.position, Origen.position) < distanceThreshold)
        {
            contants.Ismoving = false;
            navMeshAgent.SetDestination(Cola3.position);
            Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
            contants.Complete4 = true;
        }

        // MOVIMIENTO ULTIMO EN LA FILA
        if (contants.Complete4 == true && Vector3.Distance(transform.position, Cola3.position) < distanceThreshold)
        {
            contants.Complete4 = false;
            navMeshAgent.SetDestination(Cola2.position);
            Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
            contants.Complete3 = true;
        }

        // MOVIMIENTO PENÚLTIMO EN LA FILA
        if (contants.Complete3  == true && Vector3.Distance(transform.position, Cola2.position) < distanceThreshold)
        {
            contants.Ismoving = false;
            navMeshAgent.SetDestination(Cola1.position);
            contants.Complete2 = true;

        }

        // MOVIMIENTO PRIMERO EN LA FILA
        if (contants.Complete2  == true && Vector3.Distance(transform.position, Cola1.position) < distanceThreshold)
        {
            contants.Ismoving = false;
            navMeshAgent.SetDestination(Caja.position);
            contants.Complete1 = true;
        }

        // MOVIMIENTO EN CAJA HACIA LA SALIDA
        if (contants.Complete1 == true && Vector3.Distance(transform.position, Caja.position) < distanceThreshold)
        {
            contants.Ismoving = true;
            navMeshAgent.SetDestination(Exit.position);
            Debug.Log("Ha llegado al estado de Caja");
            if(Vector3.Distance(transform.position, Exit.position) < distanceThreshold)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
