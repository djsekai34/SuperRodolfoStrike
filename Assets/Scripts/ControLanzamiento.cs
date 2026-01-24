/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Controlamos el lanzamiento de la bola, su agarre en toque de pantalla, su musica y sus físicas por ultimo si esta en ganar ignore bolos y plataformas.
 ******************************************************************************/

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ControLanzamiento : MonoBehaviour
{
    private Camera cam;
    private Rigidbody2D rb;
    private SpringJoint2D spring;
    private Collider2D miCollider;

    private bool estaSiendoArrastrada = false;
    private bool yaHaSidoLanzada = false;
    private Vector2 puntoInicial;

    [Header("Ajustes de Lanzamiento")]
    [SerializeField] private float radioDeAgarre = 1.2f;
    [SerializeField] private float umbralDeLanzamiento = 0.2f;

    [Header("Físicas de la bola")]
    private float fuerzaImpulso = 14f;
    private float gravedadVuelo = 1.2f;
    private float masaBola = 3.4f;

    [Header("Sonidos")]
    [SerializeField] private AudioSource fuenteSonido; 
    [SerializeField] private AudioClip sonidoLanzamiento;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        miCollider = GetComponent<Collider2D>();

        rb.mass = masaBola;
        rb.gravityScale = 1f;

        // Si la bola tiene su SprintJoin puesto lo guardamos ya que es nuestro punto inicial
        if (spring != null)
        {
            puntoInicial = spring.connectedAnchor;
        }
    }

    void Update()
    {
        // Si hemos lanzado la bola llamamos a la funcion para que no se salga del mapa y ponemos return para NO volverla a coger
        if (yaHaSidoLanzada)
        {
            ControlarLimitesPantalla();
            return; 
        }

        // Detectamos que tenemos una pantalla
        var ts = Touchscreen.current;
        if (ts == null) return;

        if (ts.primaryTouch.press.isPressed)
        {
            // Detectamos donde esta el dedo en pixeles y los convertimos a la posicion real en el mundo
            Vector2 posicionTocar = ts.primaryTouch.position.ReadValue();
            Vector3 posicionMundo = cam.ScreenToWorldPoint(new Vector3(posicionTocar.x, posicionTocar.y, 10f));

            // Aqui nos aseguramos que hemos tocado la pantalla
            if (ts.primaryTouch.press.wasPressedThisFrame)
            {
                // Aqui comprobamos que hemos puesto el dedo en la zona de agarre
                if (Vector2.Distance(posicionMundo, transform.position) <= radioDeAgarre)
                {
                    // Ponemos que esta siendo agarrada su rigid body lo pasamos a kinematic, la frenamos en seco para que se coga bien y se activa el sprint joint
                    estaSiendoArrastrada = true;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    rb.linearVelocity = Vector2.zero;
                    rb.gravityScale = 1f;
                    if (spring != null) spring.enabled = true;
                }
            }

            // Si tenemos la bola arrastrandose 
            if (estaSiendoArrastrada)
            {
                // Pegamos la bola a nuestro dedo
                rb.position = posicionMundo;
                // Calculamos la distancia de la bola cuando la tenemos cogida y su punto original y la lanzamos
                if (Vector2.Distance(rb.position, puntoInicial) < umbralDeLanzamiento)
                {
                    Lanzar();
                }
            }
        }
        // Si estaba siendo arrastrada la bola y lo soltamos pues lanzamos
        else if (estaSiendoArrastrada)
        {
            Lanzar();
        }
    }

    private void ControlarLimitesPantalla()
    {
        Vector3 posViewport = cam.WorldToViewportPoint(transform.position);
        bool golpeaBorde = false;

 
        if (posViewport.x < 0.03f) { posViewport.x = 0.03f; golpeaBorde = true; }
        if (posViewport.x > 0.97f) { posViewport.x = 0.97f; golpeaBorde = true; }
        if (posViewport.y < 0.03f) { posViewport.y = 0.03f; golpeaBorde = true; }
        if (posViewport.y > 0.97f) { posViewport.y = 0.97f; golpeaBorde = true; }

        if (golpeaBorde)
        {
            transform.position = cam.ViewportToWorldPoint(posViewport);

            rb.linearVelocity = -rb.linearVelocity * 0.6f;
        }
    }

    private void Lanzar()
    {
        //Cambiamos las variables de estado
        estaSiendoArrastrada = false;
        yaHaSidoLanzada = true;
        
        //Activamos su rigibody dinamico y cortamos el muelle
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (spring != null) spring.enabled = false;

        // Le aplicamos la gravedad que hayamos puesto
        rb.gravityScale = gravedadVuelo;

        if (fuenteSonido != null && sonidoLanzamiento != null)
        {
            fuenteSonido.PlayOneShot(sonidoLanzamiento);
        }

        // Establecemos que ya no ignore colisiones con bolos y plataformas (ojo esto lo tenemos por si lanzamos la bola desde la zona de ganar)
        GameObject[] bolos = GameObject.FindGameObjectsWithTag("Bolos");
        foreach (GameObject bolo in bolos)
        {
            Physics2D.IgnoreCollision(miCollider, bolo.GetComponent<Collider2D>(), false);
        }

        GameObject[] plataformas = GameObject.FindGameObjectsWithTag("Plataforma");
        foreach (GameObject plat in plataformas)
        {
            Physics2D.IgnoreCollision(miCollider, plat.GetComponent<Collider2D>(), false);
        }


        //Calculamos el punto inicial y donde lo hemos soltado y lanzamos la bola
        Vector2 direccion = (puntoInicial - rb.position);
        rb.AddForce(direccion * fuerzaImpulso, ForceMode2D.Impulse);
        // Avisamos al game manager para que inicie la cuenta atras
        if (GameManager.instance != null) GameManager.instance.BolaLanzada();
    }

    // Si la bola esta siendo arrastrada ignoramos colisiones con bolos y plataformas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (estaSiendoArrastrada)
        {
            if (collision.gameObject.CompareTag("Bolos") || collision.gameObject.CompareTag("Plataforma"))
            {
                Physics2D.IgnoreCollision(miCollider, collision.collider, true);
            }
        }
    }
}