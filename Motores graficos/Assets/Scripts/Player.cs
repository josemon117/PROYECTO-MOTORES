using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public int health = 5; // Establece la salud inicial del jugador
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Collider2D collision;
    private Animator anim;
    public LayerMask WallLayer;
    private float dirX = 0;
    public float MoveSpeed = 7f;
    public float JumpForce = 8f;
    public LayerMask JumpGround;
    private enum MovementState { idle, jumping, running, clingging }
    private BoxCollider2D collisions;
    float xInicial, YInicial;
    private bool isHanging = false;
    private bool RecibioDaño = false;
    private bool isInvulnerable = false;
    public GameObject balaPrefab;      // Asigna el prefab de la bala desde el Inspector de Unity
    public Transform puntoDisparo;     // Asigna el punto de disparo desde el Inspector de Unity
    public float tiempoEntreDisparos = 0.5f; // Tiempo en segundos entre cada disparo
    private float tiempoUltimoDisparo; // Tiempo del último disparo
    public UIManager uiManager;
    public float Delay = .3f;

    [SerializeField] private AudioClip saltoSonido;


    // Start is called before the first frame update

    void Start()
    {
        Debug.Log("Inicia Start");
        ActualizarInterfaz();
        gameObject.SetActive(false);
        

        xInicial = transform.position.x;
        YInicial = transform.position.y;
        collisions = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        collision = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        tiempoUltimoDisparo = Time.time; // Inicializar el tiempo del último disparo
        Invoke("ActivarPersonajeConDelay", Delay);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > tiempoUltimoDisparo + tiempoEntreDisparos)
        {
            Debug.Log("Detecta Disparo");
            Disparar();
            tiempoUltimoDisparo = Time.time; // Actualizar el tiempo del último disparo
        }
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * MoveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            Debug.Log("Detecta Salto");
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            ControladorSonido.Instance.EjecutarSonido(saltoSonido);
        }
        bool isHittingCeiling = Physics2D.Raycast(transform.position, Vector2.up, 1f, JumpGround);
        if (isHittingCeiling && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        // Realizar un raycast hacia la izquierda o derecha para detectar la pared.
        bool isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right, 1f, WallLayer) || Physics2D.Raycast(transform.position, Vector2.left, 1f, WallLayer);
        // Si el personaje está en contacto con una pared y presiona "Shift", puedes habilitar la sujeción.
        if (isTouchingWall && Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("isTouchingWall is true"); // Este mensaje se mostrará en la consola.
            // Habilitar la sujeción.
            isHanging = true;
            rb.velocity = new Vector4(rb.velocity.x, 0.2f, rb.velocity.y, 0);

            anim.SetInteger("MovementState", 3);
        }
        else
        {
            // Si el personaje no está sujeto a la pared, deshabilitar la sujeción.
            isHanging = false;
        }
        if (health <= 0)
        {
            // Implementa el código para manejar la muerte del jugador si la salud llega a cero o menos.
            // Esto podría incluir la animación de muerte, reiniciar el nivel, etc.
            //MorirYReiniciar();
        }
        UpdateAnimationState();
    }
    void ActivarPersonajeConDelay()
    {
        // Activar el GameObject del personaje
        gameObject.SetActive(true);
    }

    void Disparar()
    {
        
        // Instanciar una nueva bala
        GameObject nuevaBala = Instantiate(balaPrefab, puntoDisparo.position, Quaternion.identity);

        // Configurar la velocidad de la bala según la orientación del personaje
        Bala scriptBala = nuevaBala.GetComponent<Bala>();
        if (scriptBala != null)
        {
            scriptBala.velocidad = 10f;  // Ajusta según sea necesario
        }
        // Actualizar la interfaz después de disparar
        ActualizarInterfaz();
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& isInvulnerable == false) // Asegúrate de que "Serpiente" sea la etiqueta de las serpientes.
        {

            RecibioDaño = true;
            StartCoroutine(ResetRecibioDaño());
            // Hacer al jugador temporalmente invulnerable y cambiar su capa de colisión
            isInvulnerable = true;
            gameObject.layer = LayerMask.NameToLayer("PlayerInvulnerable");
            // Resta una cantidad de salud al jugador cuando colisiona con una serpiente.
            health -= 1;
            anim.SetTrigger("Hurt");
            Debug.Log("¡El jugador recibió 1 punto de daño! Salud actual: " + health);
            if (health <= 0)
            {
                // Implementa el código para manejar la muerte del jugador si la salud llega a cero o menos.
                // Esto podría incluir la animación de muerte, reiniciar el nivel, etc.
            }
            StartCoroutine(DisableInvulnerabilityAfterDelay(1.0f));  // Cambia el tiempo según lo que necesites.
        }
    }

    IEnumerator DisableInvulnerabilityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isInvulnerable = false;
        gameObject.layer = LayerMask.NameToLayer("Player"); // Restablece la capa de colisión del jugador.
        ActualizarInterfaz();
    }

    // Corrutina para restablecer RecibioDaño a false después de un tiempo.
    private IEnumerator ResetRecibioDaño()
    {
        yield return new WaitForSeconds(0.5f); // Cambia el tiempo según lo que necesites.
        RecibioDaño = false;
    }

    private void UpdateAnimationState()
    {
        // Asigna un valor predeterminado a 'state' cuando la declaras
        MovementState state = MovementState.idle;

        if (dirX > 0 && RecibioDaño == false)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0 && RecibioDaño == false)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        if (!isGrounded() && RecibioDaño == false)
        {
            state = MovementState.jumping;
        }
        if (isHanging == true && RecibioDaño == false) 
        { 
            state = MovementState.clingging;
        }
        

        anim.SetInteger("MovementState", (int)state);
    
    }
    void MorirYReiniciar()
    {
        // Aquí puedes poner cualquier lógica adicional antes de reiniciar, si es necesario.
        Debug.Log("El jugador ha muerto. Reiniciando la escena del nivel 1.");

        // Reinicia la escena del nivel 1
        SceneManager.LoadScene("Nivel 1");
    }

    void ActualizarInterfaz()
    {
        for (int i = 0; i < uiManager.corazones.Length; i++)
        {
            if (i < health)
            {
                // Corazón visible
                uiManager.corazones[i].enabled = true;
            }
            else
            {
                // Corazón oculto
                uiManager.corazones[i].enabled = false;
            }
        }
    }
 


    private bool isGrounded()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, JumpGround);
    }
}
