using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serpientesnake : MonoBehaviour
{
    public float speed = 2.0f;  // Velocidad de oscilación
    private float initialY;     // Posición inicial en el eje Y
    private SpriteRenderer sprite;
    private float currentDirection = 1; // 1 para derecha, -1 para izquierda
    public float desplazamiento = 3f;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        initialY = transform.position.y;
    }

    void Update()
    {
        // Calcula el desplazamiento horizontal basado en el tiempo
        float horizontalMovement = currentDirection;

        // Aplica el movimiento a la posición de la serpiente
        transform.position = new Vector3(transform.position.x + horizontalMovement * speed * Time.deltaTime, initialY, transform.position.z);

        // Cambia la escala del sprite según la dirección del movimiento
        if (horizontalMovement > 0)
        {
            sprite.flipX = true; // Sprite mira hacia la derecha
        }
        else if (horizontalMovement < 0)
        {
            sprite.flipX = false; // Sprite mira hacia la izquierda
        }

        // Cambia de dirección cuando llega a un extremo
        if (transform.position.x > desplazamiento)
        {
            currentDirection = -1;
        }
        else if (transform.position.x < -desplazamiento)
        {
            currentDirection = 1;
        }
    }
}
