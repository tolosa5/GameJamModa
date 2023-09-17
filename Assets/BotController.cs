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
            
        }

        // MOVIMIENTO ULTIMO EN LA FILA
        if (contants.Ismoving == true && Vector3.Distance(transform.position, Cola3.position) < distanceThreshold)
        {
            contants.Ismoving = false;
            navMeshAgent.SetDestination(Cola2.position);
            Debug.Log($"La posicion actual es: {this.gameObject.transform.position}");
            
        }

        // MOVIMIENTO PENÚLTIMO EN LA FILA
        if (contants.Ismoving == true && Vector3.Distance(transform.position, Cola2.position) < distanceThreshold)
        {
            contants.Ismoving = false;
            navMeshAgent.SetDestination(Cola1.position);
            

        }

        // MOVIMIENTO PRIMERO EN LA FILA
        if (contants.Ismoving == true && Vector3.Distance(transform.position, Cola1.position) < distanceThreshold)
        {
            contants.Ismoving = false;
            navMeshAgent.SetDestination(Caja.position);
        }

        // MOVIMIENTO EN CAJA HACIA LA SALIDA
        if (contants.Ismoving == true && Vector3.Distance(transform.position, Caja.position) < distanceThreshold)
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
