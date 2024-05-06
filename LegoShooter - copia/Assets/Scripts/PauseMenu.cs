using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PanelPausa;
    public GameObject PanelInstrucciones;
    public AudioSource ad;

    private void Start()
    {
        PanelPausa.SetActive(false);
        PanelInstrucciones.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (PanelPausa.activeSelf)
            {
                QuitarPausa();
            }
            else if (!PanelInstrucciones.activeSelf)
            {
                Pausa();
            }
        }
    }

    public void Pausa()
    {
        PanelPausa.SetActive(true);
        Time.timeScale = 0;
        ad.Play();
    }

    public void QuitarPausa()
    {
        PanelPausa.SetActive(false);
        Time.timeScale = 1;
        ad.Play();
    }

    [System.Obsolete]
    public void Salir()
    {
        Time.timeScale = 1;
        ad.Play();
        DestroyEverything();
        SceneManager.LoadScene(0);
    }

    public void AbrirInstrucciones()
    {
        Time.timeScale = 0;
        PanelInstrucciones.SetActive(true);
        PanelPausa.SetActive(false);
        ad.Play();
    }

    public void CerrarInstrucciones()
    {
        Time.timeScale = 0;
        PanelPausa.SetActive(true);
        PanelInstrucciones.SetActive(false);
        ad.Play();
    }

    [System.Obsolete]
    private void DestroyEverything()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Destroy(obj); // Destruir todos los objetos
        }
    }
}
