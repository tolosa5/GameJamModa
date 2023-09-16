using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent agent;

    Text interactText;
    [SerializeField] GameObject interactTxtGO;
    [SerializeField] string[] interactTexts;

    public List<int> desiredClothesIds;
    GameObject pickedGO;
    int inventory;
    int bag;

    Camera cam;
    GameObject camGO;

    Vector3 targetPosition;

    [SerializeField] string[] tags;

    enum States {Normal, Holding};
    States currentState;

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        cam = Camera.main;
        camGO = cam.gameObject;
        interactText = interactTxtGO.GetComponent<Text>();
    }

    private void Update()
    {
        Movement();
        Debug.Log(targetPosition);

        switch (currentState)
        {
            default:
            case States.Normal:
                agent.speed = 6;
                break;
            case States.Holding:
                agent.speed = 5;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            pickedGO = null;
            HideText(interactTxtGO);
        }
    }

    void Movement()
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
        interactTxtGO.transform.position = pickedGO.transform.position;
        interactTxtGO.SetActive(true);

        interactText.text = interactTexts[i];
        /*
        switch (i)
        {
            default:
            case 0:
                interactText.text = interactTexts[0];
                break;
            case 1:
                interactText.text = "";
                break;
            case 2:
                interactText.text = "";
                break;
        }
        */
        //mostrar mensaje de interaccion
    }

    void HideText(GameObject textHolder)
    {
        textHolder.SetActive(false);
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
            if (objTag == tags[0] && currentState == States.Normal)
            {
                PickUp();
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

            Clothes clothScr = pickedGO.GetComponent<Clothes>();
            inventory = clothScr.GiveId();
        }
        else
        {
            Debug.Log("manos llenas rey");
        }
    }

    void FillBag()
    {
        bag++;
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
        GameManager.gM.AskForBasket();
        //cliente pierde paciencia, devolver prenda a cesta
    }

    void RightPlacement(int i)
    {
        desiredClothesIds.RemoveAt(i);
        currentState = States.Normal;
        inventory = 0;

        if (bag >= 3)
        {
            CompleteBag();
        }
        //algun feedback positivo
    }

    void CompleteBag()
    {
        bag = 0;
        GameManager.gM.clients++;
        //activar que el cliente se vaya y tal
    }

    void DropToBasket()
    {
        inventory = 0;
        currentState = States.Normal;
        GameManager.gM.DesactivateText(GameManager.gM.messageGO);
    }
}
