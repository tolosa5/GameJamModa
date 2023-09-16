using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;

    public List<int> desiredClothesIds;
    int interactedObjId;
    GameObject pickedGO;
    int inventory;

    float h, v;
    Camera cam;
    GameObject camGO;
    Vector3 movDir;

    Vector3 targetPosition;

    [SerializeField] string[] tags;
    [SerializeField] LayerMask isInteractable;

    enum States {Normal, Holding};
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

        switch (currentState)
        {
            default:
            case States.Normal:

                break;
            case States.Holding:

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            pickedGO = other.gameObject;
            Interact(pickedGO.tag);
        }
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

    void ShowInteractMessage(int i)
    {
        switch (i)
        {
            default:
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
        }
        //mostrar mensaje de interaccion
    }

    void Interact(string objTag)
    {
        if (objTag == tags[0])
        {
            ShowInteractMessage(0);
        }
        else if (objTag == tags[1])
        {
            ShowInteractMessage(1);
        }
        else if (objTag == tags[2])
        {
            ShowInteractMessage(2);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(objTag);
            if (objTag == tags[0] && currentState != States.Holding)
            {
                PickUp();
                Clothes clothScr = pickedGO.GetComponent<Clothes>();
                inventory = clothScr.GiveId();
            }
            else if (objTag == tags[1] && currentState == States.Holding)
            {
                FillBag();
            }
            else if (objTag == tags[2] && currentState == States.Holding)
            {
                DropToBasket();
            }
        }
    }

    void PickUp()
    {
        if (currentState == States.Normal)
        {
            currentState = States.Holding;
        }
        else
        {
            Debug.Log("manos llenas rey");
        }
    }

    void FillBag()
    {
        for (int i = 0; i < desiredClothesIds.Count; i++)
        {
            if (desiredClothesIds[i] == inventory)
            {
                RightPlacement(i);
            }
            else
            {
                Punishment();
            }
            
        }
    }

    void Punishment()
    {
        //cliente pierde paciencia, devolver prenda a cesta
    }

    void RightPlacement(int i)
    {
        desiredClothesIds.RemoveAt(i);
        //algun feedback positivo
    }

    void DropToBasket()
    {
        inventory = 0;
    }
}
