using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Transform ControladorSuelo;
    [SerializeField] private float distancia;
    [SerializeField] private bool movimientoDerecha;
    [SerializeField] private GameObject efectomuerte;
    private Rigidbody2D rb;
    //public int health = 3;  // Salud inicial del cangreho AGREGADO 02 DIC
    private void Start ()
    {
    rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        RaycastHit2D informacionSuelo = Physics2D.Raycast(ControladorSuelo.position, Vector2.down, distancia);

        rb.velocity = new Vector2(velocidad, rb.velocity.y);

        if (informacionSuelo == false)
        {
            //Girar
            Girar();
        }

    }

    private void Girar()
    {
        movimientoDerecha = !movimientoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        velocidad *= -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.clear;
        Gizmos.DrawLine(ControladorSuelo.transform.position, ControladorSuelo.transform.position + Vector3.down * distancia);
    }

   /* public void RecibirDaño(int cantidadDanio) //AGREGADO 02 DIC
    {
        // Resta la cantidad de daño a la salud
        health -= cantidadDanio;

        // Verifica si la salud llegó a cero o menos
        if (health <= 0)
        {
            // Si la salud es cero o menos, la serpiente ha sido derrotada (puedes agregar más lógica aquí)
            DerrotarCangrejo();
        }
    }*/


    // Método para ser llamado cuando el cangrejo es derrotada
    private void DerrotarCangrejo()
    {
        // Agrega aquí la lógica cuando la serpiente es derrotada (por ejemplo, reproducir una animación, otorgar puntos, etc.)
        // Luego, puedes destruir el GameObject de la serpiente si es necesario.
        Instantiate(efectomuerte, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
