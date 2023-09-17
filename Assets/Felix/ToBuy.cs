using UnityEngine;
using UnityEngine.AI;

public class ToBuy : MonoBehaviour
{
    public Transform DestinoCaja; // Punto de destino al que se dirigirá el NPC.
    [SerializeField] Transform origin; // Punto de origen del NPC.
    public bool CompraCompleta = false; // INDICA SI LA COMPRA SE COMPLETO o No
    private NavMeshAgent navMeshAgent;
    public bool RegresoOrigen = false; // Indica si el NPC está regresando al origen.

    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Inicialmente, el NPC se dirige al destino.
        SetDestination(DestinoCaja.position);
        RegresoOrigen = false;
        CompraCompleta = false;
    }

    private void Update()
    {
        // Verifica si el NPC ha llegado al destino.
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            // Si debe regresar al origen y aún no está en proceso de retorno, establece la ruta de regreso.
            if (CompraCompleta == true && !RegresoOrigen)
            {
                RegresoOrigen = true;
                SetDestination(origin.position);
            }
        }

        // Si el NPC está en proceso de regresar al origen y ha llegado, detén el proceso.
        if (RegresoOrigen && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && !navMeshAgent.pathPending)
        {
            // RegresoOrigen = false;
            this.gameObject.SetActive(false);
        }
    }

    // Establece el destino del NPC utilizando NavMeshAgent.
    private void SetDestination(Vector3 targetPosition)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(targetPosition);
        }
    }
}
