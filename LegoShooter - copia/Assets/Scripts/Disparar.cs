using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    public AudioSource ad;
    public GameObject balaInicio;
    public GameObject balaPrefab;
    private float velBala = 5000;
    private GameObject bala;
    private float cooldown = 0;

    public Light linterna;
    public AudioSource encenderLinterna;
    public AudioSource apagarLinterna;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && cooldown==0)
        {
            ad.Play();
            GameObject bala = Instantiate(balaPrefab, balaInicio.transform.position, balaInicio.transform.rotation) as GameObject;
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * velBala);
            bala.AddComponent<BalaCollision>();
            cooldown += 1 * Time.deltaTime;
        }
        if (cooldown > 0)
        {
            cooldown += 1 * Time.deltaTime;
            if (cooldown >= 1) {
                cooldown = 0;
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (linterna.enabled == true)
            {
                linterna.enabled = false;
                apagarLinterna.Play();
            }
            else
            {
                linterna.enabled = true;
                encenderLinterna.Play();
            }
        }
    }

}
