using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;

    [SerializeField] float globalTimeLeft;
    [HideInInspector] public int clients;
    [HideInInspector] public int money;
    float totalTime;

    [SerializeField] Text message;
    [HideInInspector] public GameObject messageGO;
    [SerializeField] GameObject endDayPanel;

    [SerializeField] GameObject[] speechBubbles;
    [SerializeField] Sprite[] emojis;
    [SerializeField] Transform[] clientTransforms;
    [SerializeField] Vector3 bubbleOffset;

    [TextArea] 
    [SerializeField] string[] messagesTexts;


    private void Awake()
    {
        if (gM != null)
        {
            Destroy(gameObject);
        }
        else
        {
            gM = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        messageGO = message.gameObject;
        totalTime = globalTimeLeft;
    }

    private void Update()
    {
        for (int i = 0; i < speechBubbles.Length; i++)
        {
            speechBubbles[i].transform.position = clientTransforms[i].position + bubbleOffset;
        }

        globalTimeLeft -= Time.deltaTime;
        if (globalTimeLeft <= 0)
        {
            EndDay();
        }
    }

    public void AskForBasket()
    {
        ActivateText(messageGO);
        message.text = messagesTexts[0];
    }

    public void DesactivateText(GameObject aux)
    {
        aux.SetActive(false);
    }

    void ActivateText(GameObject aux)
    {
        aux.SetActive(true);
    }

    void EndDay()
    {
        ActivateText(endDayPanel);
    }

    public void NextDay()
    {
        globalTimeLeft = totalTime;
        DesactivateText(endDayPanel);
    }
}
