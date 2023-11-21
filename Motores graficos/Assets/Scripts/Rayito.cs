using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayito : MonoBehaviour
{
    public GameObject personaje;
    public float Delay = .3f;
    // Esta función se llamará desde la animación al finalizar
    void Start()
    {
        Invoke("ActivarPersonajeConDelay", Delay);
    }

    // Función que se ejecutará después del retraso
    void ActivarPersonajeConDelay()
    {
        // Activar el GameObject del personaje
        personaje.SetActive(true);
    }
}