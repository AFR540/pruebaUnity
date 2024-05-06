using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    public Animator puerta;
    public int cont1;
    public int cont2;
    public int cont3;
    public int cont4;

    private void Start()
    {
        cont1 = 0;
        cont2 = 0;
        cont3 = 0;
        cont4 = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemigo")
        {
            switch (gameObject.name)
            {
                case "TriggerPuerta":
                case "TriggerPuerta(1)":
                    if (cont1 == 0)
                    {
                        puerta.Play("abrirPuerta");
                    }
                    cont1++;
                    break;
                case "TriggerPuerta2":
                case "TriggerPuerta2(1)":
                    if (cont2 == 0)
                    {
                        puerta.Play("abrirPuerta2");
                    }
                    cont2++;
                    break;
                case "TriggerPuerta3":
                case "TriggerPuerta3(1)":
                    if (cont3 == 0)
                    {
                        puerta.Play("abrirPuerta3");
                    }
                    cont3++;
                    break;
                case "TriggerPuerta4":
                case "TriggerPuerta4(1)":
                    if (cont4 == 0)
                    {
                        puerta.Play("abrirPuerta4");
                    }
                    cont4++;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Enemigo") 
        {
            switch (gameObject.name)
            {
                case "TriggerPuerta":
                case "TriggerPuerta(1)":
                    cont1--;
                    if (cont1 == 0)
                    {
                        puerta.Play("cerrarPuerta");
                    }
                    break;
                case "TriggerPuerta2":
                case "TriggerPuerta2(1)":
                    cont2--;
                    if (cont2 == 0)
                    {
                        puerta.Play("cerrarPuerta2");
                    }
                    break;
                case "TriggerPuerta3":
                case "TriggerPuerta3(1)":
                    cont3--;
                    if (cont3 == 0)
                    {
                        puerta.Play("cerrarPuerta3");
                    }
                    break;
                case "TriggerPuerta4":
                case "TriggerPuerta4(1)":
                    cont4--;
                    if (cont4 == 0)
                    {
                        puerta.Play("cerrarPuerta4");
                    }
                    break;
            }
        }
    }
}
