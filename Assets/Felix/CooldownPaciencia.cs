using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPaciencia : MonoBehaviour
{
    public Image image; // Referencia al componente Image.
    public Color32[] colores;
    [Range(3.0f, 15.0f)] public float transitionTime = 4.0f; // Tiempo en segundos para cada transición.
    [SerializeField] Image CaritaFeliz;
    [SerializeField] Image CaritaSeria;
    [SerializeField] Image CaritaFTriste;

    private int startIndex = 0;
    [SerializeField] private float TiempoDesaparecer = 15.0f; // Tiempo de transición del fill amount.

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        colores = new Color32[4]
        {
            new Color32(255, 0, 0, 255),     // red
            new Color32(255, 165, 0, 255),  // orange
            new Color32(255, 255, 0, 255),  // yellow
            new Color32(0, 255, 0, 255)     // green
        };

        StartCoroutine(CycleColors());
        StartCoroutine(CycleFillAmount());
    }

    public IEnumerator CycleFace()
    {
        yield return null;
    }


    public IEnumerator CycleColors()
    {
        while (true)
        {
            Color startColor = colores[startIndex];
            Color endColor = colores[(startIndex + 1) % 4];

            float elapsedTime = 0f;
            while (elapsedTime < transitionTime)
            {
                float t = elapsedTime / transitionTime;
                image.color = Color.Lerp(startColor, endColor, t);

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
            while (elapsedTime < TiempoDesaparecer)
            {
                float t = elapsedTime / TiempoDesaparecer;
                image.fillAmount = 1.0f - t;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            image.fillAmount = 0f;

            // Cambiar los colores antes de que el fill amount llegue a 0.
            startIndex = (startIndex + 1) % 4;
        }
    }
}
