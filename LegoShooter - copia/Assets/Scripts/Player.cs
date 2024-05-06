using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    public static Player instance;

    public float speed = 7f;
    public float rotationSpeed = 500f;
    // Variables para acumular los ángulos de rotación
    private float yawY; // Rotación horizontal
    private float yawX; // Rotación vertical

    // Rango de rotación para el eje X
    public float yawXMinLimit = -80f; // Límite mínimo para el eje X
    public float yawXMaxLimit = 85f;  // Límite máximo para el eje X

    public AudioSource sonidoCaminar;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.transform.position = instance.transform.position;
            gameObject.transform.rotation = instance.transform.rotation;
            Destroy(instance.gameObject);
            instance = this;
        }
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = 11f; // Si se mantiene Shift, speed es 11f
            sonidoCaminar.pitch = 2.0f;
        }
        else
        {
            speed = 6f; // Si no se mantiene Shift, speed es 6f
            sonidoCaminar.pitch = 1.0f;
        }

        // Captura el movimiento del ratón y ajusta la rotación acumulada
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yawY += mouseX * rotationSpeed * Time.deltaTime; // Acumula la rotación horizontal
        yawX -= mouseY * rotationSpeed * Time.deltaTime; // Acumula la rotación vertical

        // Limita la rotación acumulada para el eje X dentro de un rango
        yawX = Mathf.Clamp(yawX, yawXMinLimit, yawXMaxLimit);

        // Aplica la rotación acumulada alrededor de los ejes X e Y
        rb.rotation = Quaternion.Euler(yawX, yawY, 0); // Límita el eje X, pero permite rotación libre en el eje Y

        // Captura el movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Aplica el movimiento basado en la dirección de rotación
        Vector3 movement = transform.forward * moveVertical * speed + transform.right * moveHorizontal * speed;
        movement.y = rb.velocity.y; // Mantener la componente vertical del Rigidbody
        rb.velocity = movement;

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (!sonidoCaminar.isPlaying)
            {
                sonidoCaminar.Play();
            }
        }
        else
        {
            if (sonidoCaminar.isPlaying)
            {
                sonidoCaminar.Stop();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BalaEnemigo")
        {
            FindAnyObjectByType<GameManager>().QuitarVida();
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribir al evento de carga de escena
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribir del evento de carga de escena
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "WinScene" || scene.name=="GameOver") // Nombre de la escena donde se debe destruir el objeto
        {
            Destroy(gameObject); // Destruir el objeto si es esa escena
        }
    }
}
