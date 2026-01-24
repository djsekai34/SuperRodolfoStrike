/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Reproducoimos la musica de fin de nivel y mostramos los puntos obtenidos.
 ******************************************************************************/

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinNivel : MonoBehaviour
{
    public TextMeshProUGUI textoPuntos;

    void Start()
    {
        // Reproducimos la musica correspondiente 
        if (MusicManager.instance != null)
        {
            MusicManager.instance.PlayFinDeNivel();
        }
        // Leemos los puntos correspodientes y los mostramos
        int puntosFinales = GameManager.puntuacionFinalEscena;
        textoPuntos.text = puntosFinales.ToString() + " PUNTOS";
    }
}
