using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player player;

    Rigidbody rb;
    NavMeshAgent agent;
    Animator anim;

    Text interactText;
    [SerializeField] GameObject interactTxtGO;
    [SerializeField] GameObject correctClothParticlesGO;
    ParticleSystem correctClothParticles;
    [SerializeField] string[] interactTexts;

    //public List<int> desiredClothesIds;
    GameObject pickedGO;
    GameObject inTriggerGO;
    int inventory;
    int bag;

    [SerializeField] GameObject trunk;
    
    public bool completedBag;

    GameObject nearestClient;
    Request request;

    Camera cam;
    GameObject camGO;

    Vector3 targetPosition;

    [SerializeField] string[] tags;

    enum States {Normal, Holding};
    States currentState;

    private void Awake()
    {
        if (player != null)
        {
            Destroy(gameObject);
        }
        else
        {
            player = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}

//     private void Start()
//     {
//         //rb = GetComponent<Rigidbody>();
//         agent = GetComponent<NavMeshAgent>();
//         anim = GetComponent<Animator>();

//         cam = Camera.main;
//         camGO = cam.gameObject;
//         interactText = interactTxtGO.GetComponent<Text>();

//         correctClothParticles = correctClothParticlesGO.GetComponent<ParticleSystem>();
//     }

//     private void Update()
//     {
//         Movement();

//         switch (currentState)
//         {
//             default:
//             case States.Normal:
//                 agent.speed = 6;
//                 break;
//             case States.Holding:
//                 agent.speed = 5;
//                 break;
//         }
//     }

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.layer == 6)
//         {
//             inTriggerGO = other.gameObject;
//             Debug.Log(inTriggerGO);
//             Debug.Log(inTriggerGO.tag);
//             Interact(inTriggerGO.tag);
//         }
//     }

//     private void OnTriggerExit(Collider other)
//     {
//         if (other.gameObject.layer == 6)
//         {
//             pickedGO = inTriggerGO;
//             inTriggerGO = null;
//             HideText(interactTxtGO);
//         }
//     }

//     void Movement()
//     {
//         if (Input.GetMouseButtonDown(0) && Physics.Raycast(cam.ScreenPointToRay
//             (Input.mousePosition), out RaycastHit hitInfo))
//         {
//             targetPosition = hitInfo.point;
//             agent.SetDestination(targetPosition);
//         }
//         if (rb.velocity != Vector3.zero)
//         {
//             anim.SetBool("Walking", true);
//         }
//         else
//         {
//             anim.SetBool("Walking", false);
//         }
//     }

//     void ShowInteractMessage(int i)
//     {
        
//         interactTxtGO.SetActive(true);

//         interactText.text = interactTexts[i];
//     }

//     void HideText(GameObject textHolder)
//     {
//         textHolder.SetActive(false);
//     }

//     void FindNearestClient()
//     {
//         nearestClient = Spawmer.Instance.spawnedObjects[0];
//         request = nearestClient.GetComponent<Request>();
//     }

//     void Interact(string objTag)
//     {
//         if (objTag == tags[0])
//         {
//             ShowInteractMessage(0);
//         }
//         else if (objTag == tags[1])
//         {
//             ShowInteractMessage(1);
//         }
//         else if (objTag == tags[2])
//         {
//             ShowInteractMessage(2);
//         }

//         if (Input.GetKeyDown(KeyCode.F))
//         {
//             Debug.Log(objTag);
//             if (objTag == tags[0] && currentState == States.Normal)
//             {
//                 PickUp();
//             }
//             else if (objTag == tags[1] && currentState == States.Holding)
//             {
//                 FillBag();
//             }
//             else if (objTag == tags[2] && currentState == States.Holding)
//             {
//                 DropToBasket();
//             }
//         }
//     }

//     void PickUp()
//     {
//         if (currentState == States.Normal)
//         {
//             Debug.Log("pickeando " + pickedGO);
//             currentState = States.Holding;

//             Clothes clothScr = pickedGO.GetComponent<Clothes>();
//             inventory = clothScr.GiveId();

//             trunk.SetActive(true);
//         }
//         else
//         {
//             Debug.Log("manos llenas rey");
//         }
//     }

//     void FillBag()
//     {
//         bag++;
//         Debug.Log("llenando bolsa con " + pickedGO);
//         for (int i = 0; i < request.generatedNumbers.Count; i++)
//         {
//             if (request.generatedNumbers[i] == inventory)
//             {
//                 RightPlacement(i);
//             }
//             else
//             {
//                 Punishment();
//             }
//         }
//     }

//     void Punishment()
//     {
//         GameManager.gM.AskForBasket();
//         //cliente pierde paciencia
//     }

//     void RightPlacement(int i)
//     {
//         request.generatedNumbers.RemoveAt(i);
//         currentState = States.Normal;
//         inventory = 0;
//         trunk.SetActive(false);
//         //se activa feedback
//         correctClothParticles.Play();

//         if (bag >= 3)
//         {
//             CompleteBag();
//         }
//     }

//     void CompleteBag()
//     {
//         bag = 0;
//         completedBag = true;
//         GameManager.gM.clients++;

//         ToBuy toBuyScr = Spawmer.Instance.spawnedObjects[0].GetComponent<ToBuy>();
//         toBuyScr.CompraCompleta = true;
//         Spawmer.Instance.spawnedObjects.RemoveAt(0);

//         //activar que el cliente se vaya y tal

//         GameManager.gM.MoneyCalculator();
//     }

//     void DropToBasket()
//     {
//         inventory = 0;
//         trunk.SetActive(false);
//         currentState = States.Normal;
//         GameManager.gM.DesactivateText(GameManager.gM.messageGO);
//     }
// }
