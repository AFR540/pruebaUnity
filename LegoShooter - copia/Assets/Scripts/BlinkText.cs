using UnityEngine;
using UnityEngine.UI; // Para Text
using TMPro; // Para TextMeshProUGUI

public class BlinkText : MonoBehaviour
{
    public float blinkInterval = 0.5f; // Intervalo en segundos para parpadear
    private float timer;
    private bool isVisible;

    // Puedes usar uno de estos según el tipo de texto en tu proyecto
    private Text uiText; // Para Text normal
    private TextMeshProUGUI tmpText; // Para TextMeshProUGUI

    void Start()
    {
        // Intenta obtener ambos componentes
        uiText = GetComponent<Text>();
        tmpText = GetComponent<TextMeshProUGUI>();

        if (uiText == null && tmpText == null)
        {
            Debug.LogError("No se encontró ningún componente de texto en el objeto.");
            return;
        }

        isVisible = true; // El texto comienza visible
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= blinkInterval)
        {
            isVisible = !isVisible; // Cambia el estado de visibilidad
            timer = 0; // Reinicia el temporizador

            // Aplica el cambio de visibilidad
            if (uiText != null)
            {
                uiText.enabled = isVisible;
            }
            if (tmpText != null)
            {
                tmpText.enabled = isVisible;
            }
        }
    }
}
