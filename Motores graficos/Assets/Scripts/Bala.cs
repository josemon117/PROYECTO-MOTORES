using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float velocidad = 10f;
    public int damage = 1;
    public float tiempoDeVida = 3.0f; // tiempo en segundos antes de que la bala desaparezca


    void Update()
    {
        // Mover la bala en la dirección hacia adelante
        //transform.Translate(Vector2.right * velocidad * Time.deltaTime);
        // Reducir el tiempo de vida de la bala
        tiempoDeVida -= Time.deltaTime;
        if (tiempoDeVida <= 0)
        {
            Destroy(gameObject); // Destruir la bala
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        /*// Verificar si la bala impacta con un objeto
        if (other.CompareTag("Enemy"))
        {
            // Llamar a un método en el enemigo para infligir daño
            other.GetComponent <serpientesnake>().RecibirDaño(damage);

            // Destruir la bala al impactar con un enemigo
            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            // Destruir la bala al impactar con un muro
            Destroy(gameObject);
        }*/
    }
}
