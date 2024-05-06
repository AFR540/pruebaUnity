using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldadoDisparar : MonoBehaviour
{
    public AudioSource ad;
    public GameObject balaInicio;
    public GameObject balaPrefab;
    private float velBala = 5000;
    private GameObject bala;

    public void DispararTrigger()
    {
        ad.Play();
        // Crear un error aleatorio en la rotaci�n horizontal sobre el eje Y
        float angleErrorY = Random.Range(-10f, 10f); // Rango de desviaci�n en grados
        float angleErrorX = Random.Range(-3f, 3f); 

        // Crear una rotaci�n basada en el error aleatorio
        Quaternion desviacion = Quaternion.Euler(angleErrorX, angleErrorY, 0); // 0 en X y Z, solo cambia Y

        bala = Instantiate(balaPrefab, balaInicio.transform.position, balaInicio.transform.rotation*desviacion) as GameObject;
        Rigidbody rb = bala.GetComponent<Rigidbody>();
        Vector3 direccion = desviacion * transform.forward;
        rb.AddForce(direccion * velBala);

        bala.AddComponent<BalaCollision>();
    }
}

public class BalaCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        //Debug.Log(collision.gameObject.name);
    }
}
