using UnityEngine;

public class LightFlickerController : MonoBehaviour
{
    public float minFlickerInterval = 0.01f;
    public float maxFlickerInterval = 0.5f;
    public GameObject techo;
    private GameObject[] techos;
    private Light[] luces;
    private bool flickering = false; // Indica si el parpadeo está activo
    private float timeToNextFlicker;

    public AudioSource sonidoParpadeo;

    void Start()
    {
        luces = GetComponentsInChildren<Light>();
        // Obtener todos los hijos de 'techo'
        int childCount = techo.transform.childCount; // Número total de hijos
        techos = new GameObject[childCount]; // Crear un array para los techos

        for (int i = 0; i < childCount; i++)
        {
            techos[i] = techo.transform.GetChild(i).gameObject; // Obtener cada hijo
        }
        ResetFlickerTimer();
    }

    void Update()
    {
        if (flickering)
        {
            timeToNextFlicker -= Time.deltaTime;
            if (timeToNextFlicker <= 0)
            {
                FlickerLights(); // Parpadear luces
                ResetFlickerTimer();
            }
        }
    }

    public void StartFlickering()
    {
        flickering = true; // Iniciar parpadeo
    }

    public void StopFlickering()
    {
        flickering = false; // Detener parpadeo
        // Asegurarse de que las luces estén encendidas al detener el parpadeo
        foreach (var luz in luces)
        {
            luz.enabled = true;
        }
        foreach (var tech in techos)
        {
            tech.SetActive(true);
        }
    }

    private void FlickerLights()
    {
        foreach (var luz in luces)
        {
            luz.enabled = !luz.enabled; // Alternar estado de las luces
            if (!luz.enabled)
            {
                PlayFlickerSound(); // Reproducir sonido cuando la luz se apaga
            }
        }
        foreach (var tech in techos)
        {
            tech.SetActive(!tech.activeSelf);
        }
    }

    private void PlayFlickerSound()
    {
        if (sonidoParpadeo != null)
        {
            sonidoParpadeo.Play(); // Reproducir sonido
        }
    }

    private void ResetFlickerTimer()
    {
        timeToNextFlicker = Random.Range(minFlickerInterval, maxFlickerInterval);
    }
}
