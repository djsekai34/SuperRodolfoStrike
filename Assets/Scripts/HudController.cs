/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Mostramos y actualizamos la puntuacion y los bolos en el hud
 ******************************************************************************/
using TMPro;
using UnityEngine;

public class HudController : MonoBehaviour
{
    public static HudController instance;

    [SerializeField] private TextMeshProUGUI textoPuntos;
    [SerializeField] private TextMeshProUGUI textoBolos;

    void Awake()
    {
        instance = this;
    }

    // Funciones para actualizar la interfaz con los puntos y los bolos
    public void ActualizarTextoPuntos(int puntos)
    {
        textoPuntos.text = "Puntos: " + puntos;
    }

    public void ActualizarTextoBolos(int cantidad)
    {
        textoBolos.text = "Bolos: " + cantidad;
    }
}
