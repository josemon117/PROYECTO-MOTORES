using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayito : MonoBehaviour
{
    public GameObject personaje;
    public float Delay = .3f;
    // Esta funci�n se llamar� desde la animaci�n al finalizar
    void Start()
    {
        Invoke("ActivarPersonajeConDelay", Delay);
    }

    // Funci�n que se ejecutar� despu�s del retraso
    void ActivarPersonajeConDelay()
    {
        // Activar el GameObject del personaje
        personaje.SetActive(true);
    }
}