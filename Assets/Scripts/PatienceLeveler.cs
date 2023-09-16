using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatienceLeveler : MonoBehaviour
{
    public Canvas Canvas2D;
    public Image speechBubble;
    [SerializeField] private float patienceTimer = 0.25f;
    [SerializeField] private Gradient patienceGradient;
    private float client = 1f;
    private Color newPatienceColor;
    private Coroutine drainPatienceCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        speechBubble = GetComponent<Image>();
        //see patience color
        speechBubble.color = patienceGradient.Evaluate(client);
        CheckPatienceAmount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePatience(float maxPatience, float currentPatience)
    {
        Debug.Log("ayaya");
        client = currentPatience / maxPatience;
        drainPatienceCoroutine = StartCoroutine(DrainPatience());
        CheckPatienceAmount();
    }

    private IEnumerator DrainPatience()
    {
        float fillAmount = speechBubble.fillAmount;
        Color currentColor = speechBubble.color;
        float elapsedTime = 0f;
        while (elapsedTime < patienceTimer)
        {
            elapsedTime += Time.deltaTime;
            //lerp the fill amount
            speechBubble.fillAmount = Mathf.Lerp(fillAmount, client, (elapsedTime / patienceTimer));
            //lerp the color based on the gradient
            speechBubble.color = Color.Lerp(currentColor, newPatienceColor, (elapsedTime/patienceTimer));
            yield return null;
        }
    }
    private void CheckPatienceAmount()
    {
        newPatienceColor = patienceGradient.Evaluate(client);
    }
}
