using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VaderController : MonoBehaviour
{
    public Animator ani;
    public SkinnedMeshRenderer espada;
    public Light luzEspada;

    private Transform playerTr;
    private NavMeshAgent agent;
    private int hitCont;
    private Renderer rend;
    private Renderer[] renderers;
    private List<Color> coloresOriginales = new List<Color>(); // Lista para almacenar los colores originales
    private Color colorHit = new Color(1f, 0f, 0f, 0.5f); // Rojo con algo de transparencia
    private int index = 0;
    private float cronoHit = 0;

    public AudioSource encenderEspada;
    public AudioSource baseEspada;
    public AudioSource pegarEspada;
    private Boolean espadaEncendida;

    public AudioSource sonidoAmbiente;
    public AudioSource musicaBatalla;

    
    void Start()
    {
        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        hitCont = 0;
        rend = GetComponent<Renderer>();
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            coloresOriginales.Add(renderer.material.color); // Guardar el color original
        }
        espada.enabled = false;
        luzEspada.enabled = false;
        espadaEncendida = false;
    }

    private void Acciones()
    {
        if (Vector3.Distance(transform.position, playerTr.transform.position) <= 5 && hitCont < 15)
        {
            ani.SetBool("Atacar", true);
            agent.speed = 0;
            agent.destination = playerTr.transform.position;

            Vector3 direction = (playerTr.position - transform.position).normalized; // Dirección hacia el jugador
            Quaternion lookRotation = Quaternion.LookRotation(direction); // Rotación para mirar
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5); // Suavizar rotación
        }else if (Vector3.Distance(transform.position, playerTr.transform.position) <= 40 && hitCont < 15)
        {
            espada.enabled = true;
            luzEspada.enabled=true;
            ani.SetBool("Atacar", false);
            ani.SetBool("Cerca", true);
            agent.speed = 6;
            agent.destination = playerTr.transform.position;
            if (!espadaEncendida)
            {
                encenderEspada.Play();
                baseEspada.Play();
                musicaBatalla.Play();
                espadaEncendida = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            foreach (Renderer renderer in renderers)
            {
                renderer.material.color = colorHit; // Cambiar el color del material
            }
            hitCont++;
            if (hitCont >= 15)
            {
                agent.speed = 0;
                ani.SetBool("Morir", true);
            }
            cronoHit += 2 * Time.deltaTime;
        }
    }

    public void AtaqueVader()
    {
        if (Vector3.Distance(transform.position, playerTr.transform.position) <= 5 && hitCont < 15)
        {
            FindAnyObjectByType<GameManager>().QuitarVida();
        }
    }

    public void VaderMorir()
    {
        Destroy(gameObject);
    }

    public void SonidoPegar()
    {
        pegarEspada.Play();
    }

    // Update is called once per frame
    void Update()
    {
        playerTr = FindAnyObjectByType<Player>().transform;
        Acciones();
        index = 0;
        if (cronoHit > 0)
        {
            cronoHit += 2 * Time.deltaTime;
            if (cronoHit >= 1)
            {
                cronoHit = 0;
                foreach (Renderer renderer in renderers)
                {
                    renderer.material.color = coloresOriginales[index];
                    index++;
                }
            }
        }
    }
}
