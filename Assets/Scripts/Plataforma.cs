/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Controlamos que la plataforma ignore colisiones cuando está en la zona de ganar.
 ******************************************************************************/

using UnityEngine;

public class Plataforma : MonoBehaviour
{
    private Collider2D miCollider;
    private bool estaEnZonaGanar = false;
    private SpriteRenderer sprite;
    private int ordenOriginal;

    void Start()
    {
        miCollider = GetComponent<Collider2D>();

        sprite = GetComponent<SpriteRenderer>();

        if (sprite != null) ordenOriginal = sprite.sortingOrder; // Guardamos el layer original
    }

    // Detectamos si la plataforma entra en la zona de ganar
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ganar"))
        {
            estaEnZonaGanar = true;
            if (sprite != null) sprite.sortingOrder = -20; // Le ponemos menos para que se vaya al fondo
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ganar"))
        {
            estaEnZonaGanar = false;
            if (sprite != null) sprite.sortingOrder = ordenOriginal;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Si la plataforma a tocado el suelo o pared ojo no se ignora para que no se caiga
        if (collision.gameObject.CompareTag("SueloPared"))
        {
            return; 
        }

        // Si estamos en la zona de ganar ignoramos todo lo que venga
        if (estaEnZonaGanar)
        {
            Physics2D.IgnoreCollision(miCollider, collision.collider);
            return;
        }
    }
}
