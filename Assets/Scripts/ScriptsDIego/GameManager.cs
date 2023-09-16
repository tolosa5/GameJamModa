using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;

    [SerializeField] float globalTimeLeft;
    [HideInInspector] public int clients;

    [SerializeField] Text message;
    [SerializeField] GameObject endDayPanel;
    [HideInInspector] public GameObject messageGO;

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
    }

    private void Update()
    {
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
}
