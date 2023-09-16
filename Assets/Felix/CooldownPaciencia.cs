using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownPaciencia : MonoBehaviour
{
    public Image image; // Referencia al componente Image.
    public Color32[] colores;
    [Range(3.0f, 15.0f)] public float transitionTime = 5.0f; // Tiempo en segundos para cada transición.

    private int startIndex = 0;

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

        StartCoroutine(Cycle());
    }

    public IEnumerator Cycle()
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
                
                // Ajustar el valor "fill amount" en función del progreso del ciclo.
                image.fillAmount = 1.0f - t;

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            startIndex = (startIndex + 1) % 4;
            yield return null;
        }
    }
}
