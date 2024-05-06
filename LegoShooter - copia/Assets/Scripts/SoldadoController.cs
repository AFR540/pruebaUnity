using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SoldadoController : MonoBehaviour
{
    public int rutina;
    public float crono;
    public Animator ani;
    public SkinnedMeshRenderer arma;

    public GameObject dest1;
    public GameObject dest2;
    public GameObject dest3;
    public GameObject dest4;

    private Transform playerTr;
    private NavMeshAgent agent;
    private int hitCont;
    private Renderer rend;
    private Renderer[] renderers;
    private List<Color> coloresOriginales = new List<Color>(); // Lista para almacenar los colores originales
    private Color colorHit = new Color(1f, 0f, 0f, 0.5f); // Rojo con algo de transparencia
    private int index = 0;
    private float cronoHit = 0;

    private void Start()
    {
        ani=GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rutina = Random.Range(1, 5);
        hitCont = 0;
        rend = GetComponent<Renderer>();
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            coloresOriginales.Add(renderer.material.color); // Guardar el color original
        }
    }

    public void Acciones()
    {
        if (Vector3.Distance(transform.position, playerTr.transform.position) <= 30 && hitCont<2)
        {
            arma.enabled = true;
            agent.speed = 0;
            agent.destination = playerTr.position;
            ani.SetInteger("Accion", 3);

            Vector3 direction = (playerTr.position - transform.position).normalized; // Dirección hacia el jugador
            Quaternion lookRotation = Quaternion.LookRotation(direction); // Rotación para mirar
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5); // Suavizar rotación

        }
        else if (Vector3.Distance(transform.position, playerTr.transform.position) <= 50 && hitCont < 2)
        {
            arma.enabled=false;
            agent.speed = 8;
            agent.destination = playerTr.position;
            ani.SetInteger("Accion", 2);
        }
        else if (hitCont < 2)
        {
            arma.enabled = false;
            crono += 1*Time.deltaTime;
            if (crono > 30)
            {
                rutina = Random.Range(1, 5);
                crono = 0;
            }
            ani.SetInteger("Accion", 1);
            agent.speed = 2;
            switch (rutina)
            {
                case 1:
                    agent.destination = dest1.transform.position;
                    break;
                case 2:
                    agent.destination = dest2.transform.position;
                    break;
                case 3:
                    agent.destination = dest3.transform.position;
                    break;
                case 4:
                    agent.destination = dest4.transform.position;
                    break;
            }
            if (Vector3.Distance(transform.position, agent.destination) <= 3)
            {
                agent.speed = 0;
                ani.SetInteger("Accion", 0);
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
            if (hitCont == 2)
            {
                ani.SetInteger("Accion", 4);
            }
            cronoHit += 2 * Time.deltaTime;
        }
    }

    public void MorirTrigger()
    {
        Destroy(gameObject);
    }

    private void Update()
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
