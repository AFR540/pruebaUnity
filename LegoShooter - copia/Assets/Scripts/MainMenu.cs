using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject panelInstrucciones;
    public AudioSource clickSound;

    private void Start()
    {
        panelInstrucciones.SetActive(false);
    }
    public void Play()
    {
        clickSound.Play();
        Invoke("LoadNextScene", 1);
    }
    public void Quit()
    {
        clickSound.Play();
        Invoke("SalirJuego", 1);
    }

    public void Instrucciones()
    {
        clickSound.Play();
        panelInstrucciones.SetActive(true);
    }

    public void Equis()
    {
        clickSound.Play();
        panelInstrucciones.SetActive(false);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SalirJuego()
    {
        Application.Quit();
    }
}
