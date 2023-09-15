using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;

    float h, v;
    Camera cam;
    GameObject camGO;
    [SerializeField] float speed;
    Vector3 movDir;

    Vector3 targetPosition;


    enum States {aa, ee};
    States currentState;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        cam = Camera.main;
        camGO = cam.gameObject;
    }

    private void Update()
    {
        Inputs();
        Debug.Log(agent.isStopped);
        Debug.Log(targetPosition);
    }
    private void FixedUpdate()
    {
        //rb.AddForce(movDir);
    }
    void Inputs()
    {
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(cam.ScreenPointToRay
            (Input.mousePosition), out RaycastHit hitInfo))
        {
            targetPosition = hitInfo.point;
            agent.SetDestination(targetPosition);
        }
        //movDir = new Vector3(h, v, 0).normalized * speed;
    }

    void PickUp()
    {

    }

    void GiveObject()
    {

    }

    void Drop()
    {

    }
}
