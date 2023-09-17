using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //important

public class RandonMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float rango; //radius of sphere

    public Transform AreaDeCaminata; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(AreaDeCaminata.position, rango, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
        Debug.Log(this.gameObject == Spawmer.instance.spawnedClients[0]);
        if (this.gameObject == Spawmer.instance.spawnedClients[0])
        {
            Debug.Log("llegue");
            //this.GetComponent<RandonMovement>().enabled = false;
        }
    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        { 
           
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnDisable()
    {
        ToBuy toBuyScr = GetComponent<ToBuy>();
        toBuyScr.enabled = true;
    }
}