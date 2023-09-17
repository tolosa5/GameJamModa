using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gM;

    int currentDay = 1;

    [SerializeField] float globalTimeLeft;
    [HideInInspector] public int clients;
    [HideInInspector] public float money;
    float neededMoney;
    [SerializeField] float baseMoneyGains;
    [SerializeField] Image fillerImage;

    float moneyMultiplier;
    float totalTime;

    [SerializeField] Text message;
    [HideInInspector] public GameObject messageGO;
    [SerializeField] GameObject endDayPanel, congratsPanel, gameOverPanel;

    [SerializeField] GameObject[] speechBubbles;
    [SerializeField] Sprite[] emojis;
    [SerializeField] Transform[] clientTransforms;
    [SerializeField] Vector3 bubbleOffset;

    [TextArea] 
    [SerializeField] string[] messagesTexts;

    [SerializeField] List<Transform> placeHoldersPositions;
    [SerializeField] GameObject[] clothes;

    [SerializeField] TextMeshProUGUI coins, day;
    [SerializeField] Image timerFiller;


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

        if (globalTimeLeft >= 0)
        {
            globalTimeLeft -= Time.deltaTime;
            timerFiller.fillAmount = globalTimeLeft;
        }
        if (globalTimeLeft <= 0)
        {
            EndDay();
        }

        switch (currentDay)
        {
            default:
            case 1:

                break;

            case 2:

                break;
        }
    }

    void ClothesOrganizer()
    {
        for (int i = 0; i < clothes.Length; i++)
        {
            int rand = Random.Range(0, placeHoldersPositions.Count);
            clothes[i].transform.position = placeHoldersPositions[rand].position;
            placeHoldersPositions.RemoveAt(rand);
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

    public void MoneyCalculator()
    {
        if (Player.player.completedBag)
        {
            Player.player.completedBag = false;
            moneyMultiplier = fillerImage.fillAmount;

            float clientMoney = baseMoneyGains *= moneyMultiplier;
            money += clientMoney;

        }
    }

    void EndDay()
    {
        ActivateText(endDayPanel);
        EndDayPanelConfig();
    }

    void EndDayPanelConfig()
    {
        if (currentDay == 1) congratsPanel.SetActive(true);
        else
        {
            if (money <= neededMoney) gameOverPanel.SetActive(true);
            else
            {
                congratsPanel.SetActive(true);
            }
        }
    }




    //se llama por UI
    public void NextDay()
    {
        globalTimeLeft = totalTime;
        DesactivateText(endDayPanel);
        currentDay++;
    }

    //se llama por UI
    public void Reestart()
    {
        globalTimeLeft = totalTime;
        DesactivateText(endDayPanel);
        currentDay = 1;
    }
}
