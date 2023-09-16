using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPaciencia : MonoBehaviour
{
    public Image image; // Referencia al componente Image.
    public Color32[] colores;
    [SerializeField] public Image Feliz;
    [SerializeField] public Image Seria;
    [SerializeField] public Image Triste;

    [Range(3.0f, 15.0f)] public float transitionTimeColor = 5.0f; // Tiempo en segundos para cada transición.

    private int startIndex = 0;
    public float fillTransitionTime = 15.0f; // Tiempo de transición del fill amount.

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

        Feliz.GetComponent<Image>();
        Seria.GetComponent<Image>();
        Triste.GetComponent<Image>();

        colores = new Color32[4]
        {
            new Color32(0, 255, 0, 255),    // green
            new Color32(255, 255, 0, 255),  // yellow
            new Color32(255, 165, 0, 255),  // orange
            new Color32(255, 0, 0, 255)     // red
        };

        StartCoroutine(CycleColors());
        StartCoroutine(CycleFillAmount());
    }

    void Facetime(float t)
    {
        // Valores de t para los cambios de estado.
        float felizThreshold = 0.70f;
        float seriaThreshold = 0.30f;
        float tristeThreshold = 0.01f;

        if (t >= felizThreshold)
        {
            // Cambiar a la imagen feliz.
            Feliz.gameObject.SetActive(true);
            Seria.gameObject.SetActive(false);
            Triste.gameObject.SetActive(false);
        }
        else if (t >= seriaThreshold)
        {
            // Cambiar a la imagen seria.
            Feliz.gameObject.SetActive(false);
            Seria.gameObject.SetActive(true);
            Triste.gameObject.SetActive(false);
        }
        else if (t >= tristeThreshold)
        {
            // Cambiar a la imagen triste.
            Feliz.gameObject.SetActive(false);
            Seria.gameObject.SetActive(false);
            Triste.gameObject.SetActive(true);
        }
        else
        {
            // No hay ninguna imagen activada si t es menor que tristeThreshold.
            Feliz.gameObject.SetActive(false);
            Seria.gameObject.SetActive(false);
            Triste.gameObject.SetActive(false);
        }
    }

    public IEnumerator CycleColors()
    {
        while (true)
        {
            Color startColor = colores[startIndex];
            Color endColor = colores[(startIndex + 1) % 4];

            float elapsedTime = 0f;
            while (elapsedTime < transitionTimeColor)
            {
                float t = elapsedTime / transitionTimeColor;
                image.color = Color.Lerp(startColor, endColor, t);

                // Llamar a Facetime con el valor 1.0f - t.
                Facetime(1.0f - t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            startIndex = (startIndex + 1) % 4;
        }
    }

    public IEnumerator CycleFillAmount()
    {
        while (true)
        {
            float elapsedTime = 0f;
            while (elapsedTime < fillTransitionTime)
            {
                float t = elapsedTime / fillTransitionTime;
                image.fillAmount = 1.0f - t;

                // Llamar a Facetime con el valor 1.0f - t.
                Facetime(1.0f - t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.fillAmount = 0f;

            // Cambiar los colores antes de que el fill amount llegue a 0.
            startIndex = (startIndex + 1) % 4;
        }
    }
}
