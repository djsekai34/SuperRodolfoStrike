/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo strike
 * @Descripcion: Controlamos el bolo, si ha entrado en la zona de ganar o ha salido y que se acumulen en zona de ganar
 ******************************************************************************/
using System.Collections;
using UnityEngine;

public class Bolo : MonoBehaviour
{
    public bool yaHaGanado = false;
    private Rigidbody2D rb;
    private Collider2D miCollider;
    private SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        miCollider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    //Llamamos a la funcion para que el bolo este dentro de la pantalla
    void Update()
    {
        MantenerDentroDeCamara();
    }

    private void MantenerDentroDeCamara()
    {
        // Convertimos la poscion de nuestro mundo a coordenadas de pantalla
        Vector3 posViewport = Camera.main.WorldToViewportPoint(transform.position);
        bool fuera = false;

        //Si el bolo intenta salir lo frenamos para que no salga
        if (posViewport.x < 0.02f) { posViewport.x = 0.02f; fuera = true; }
        if (posViewport.x > 0.98f) { posViewport.x = 0.98f; fuera = true; }
        if (posViewport.y < 0.02f) { posViewport.y = 0.02f; fuera = true; }
        if (posViewport.y > 0.98f) { posViewport.y = 0.98f; fuera = true; }

        //Si el bolo ha salido lo volvemos a meter en el mapa
        if (fuera)
        {
            transform.position = Camera.main.ViewportToWorldPoint(posViewport);
            rb.linearVelocity = -rb.linearVelocity * 0.5f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ganar"))
        {
            // Creamos una caja ficticia que representa los límites del bolo 
            Bounds limitesBolo = miCollider.bounds;

            // Obtenemos los limites que tenemos en la zona de ganar
            Bounds limitesZona = other.bounds;

            // Creamos un booleano para ver si el bolo esta completamente dentro esto usara el punto minimo que es el inferior izquierdo y el punto maximo que es el superior derecho
            bool estaEnteroDentro = limitesZona.Contains(limitesBolo.min) && limitesZona.Contains(limitesBolo.max);

            // Si el bolo esta completamente dentro y es la primera vez que lo hace lo anotamos
            if (estaEnteroDentro && !yaHaGanado)
            {
                yaHaGanado = true;
                if (GameManager.instance != null) GameManager.instance.BoloAnotado();
            }


            // Si ya ha entrado pero ya no esta completamente dentro le restamos la puntuacion
            else if (!estaEnteroDentro && yaHaGanado)
            {
                yaHaGanado = false;
                if (GameManager.instance != null) GameManager.instance.BoloRestado();
            }
        }
    }

    // Si el bolo toca la bola y este esta en la zona ganar ignoramos a la bola
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bola") && yaHaGanado)
        {
            Physics2D.IgnoreCollision(miCollider, collision.collider);
        }

        // NUEVO: Si chocamos con otro bolo y ambos (o al menos nosotros) estamos en la zona ganar, nos atravesamos
        if (collision.gameObject.CompareTag("Bolos") && yaHaGanado)
        {
            Physics2D.IgnoreCollision(miCollider, collision.collider);
        }
    }
}