using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text textoEnemigos;
    private int numEnemigos;
    private int vidas;
    private List<RawImage> vidasIMG = new List<RawImage>();
    private int totalScenes;

    public RawImage vida1;
    public RawImage vida2;
    public RawImage vida3;
    public RawImage vida4;
    public RawImage vida5;

    private float cuenta;
    public TMP_Text textoTiempo;
    public TMP_Text tiempoRestante;
    public LightFlickerController lightFlickerController;

    // Start is called before the first frame update
    void Start()
    {
        numEnemigos = transform.childCount;
        textoEnemigos.text = numEnemigos.ToString();
        vidas = 5;
        
        vidasIMG.Add(vida1);
        vidasIMG.Add(vida2);
        vidasIMG.Add(vida3);
        vidasIMG.Add(vida4);
        vidasIMG.Add(vida5);

        cuenta = 10;
        textoTiempo.enabled = false;
        tiempoRestante.enabled = false;

        totalScenes = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    void Update()
    {
        numEnemigos = transform.childCount;
        textoEnemigos.text = numEnemigos.ToString();
        if (totalScenes - SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (numEnemigos <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            if (numEnemigos <= 0)
            {
                textoTiempo.enabled = true;
                tiempoRestante.enabled = true;
                cuenta -= 1 * Time.deltaTime;
                lightFlickerController.StartFlickering();
            }
            float redondeo = (float)Mathf.Round(cuenta);
            tiempoRestante.text = redondeo.ToString();
            if (cuenta <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void QuitarVida()
    {
        vidas--;
        vidasIMG[vidas].enabled = false;

        if (vidas == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
