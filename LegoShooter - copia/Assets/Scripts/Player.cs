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
    // Variables para acumular los �ngulos de rotaci�n
    private float yawY; // Rotaci�n horizontal
    private float yawX; // Rotaci�n vertical

    // Rango de rotaci�n para el eje X
    public float yawXMinLimit = -80f; // L�mite m�nimo para el eje X
    public float yawXMaxLimit = 85f;  // L�mite m�ximo para el eje X

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

        // Captura el movimiento del rat�n y ajusta la rotaci�n acumulada
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yawY += mouseX * rotationSpeed * Time.deltaTime; // Acumula la rotaci�n horizontal
        yawX -= mouseY * rotationSpeed * Time.deltaTime; // Acumula la rotaci�n vertical

        // Limita la rotaci�n acumulada para el eje X dentro de un rango
        yawX = Mathf.Clamp(yawX, yawXMinLimit, yawXMaxLimit);

        // Aplica la rotaci�n acumulada alrededor de los ejes X e Y
        rb.rotation = Quaternion.Euler(yawX, yawY, 0); // L�mita el eje X, pero permite rotaci�n libre en el eje Y

        // Captura el movimiento horizontal y vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Aplica el movimiento basado en la direcci�n de rotaci�n
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
