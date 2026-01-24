/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Controla la puntuacion y los bolos, lo actualizamos en el hud y cuando pase x tiempo nos vamos a otra escena
 ******************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static int puntuacionFinalEscena; 

    private int puntuacionTotal = 0;
    private int bolosRestantes;
    private bool yaHaTerminadoElNivel = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // Contamos cuantos bolos hay
        bolosRestantes = GameObject.FindGameObjectsWithTag("Bolos").Length;

        // Iniciamos el hud con los bolos correspodientes y su puntuacion correspodiente
        if (HudController.instance != null)
        {
            HudController.instance.ActualizarTextoPuntos(puntuacionTotal);
            HudController.instance.ActualizarTextoBolos(bolosRestantes);
        }
    }

    public void BolaLanzada()
    {
        // Si ya hemos lanzado la bola comenzamos la cuenta de 10 para ir al final de nivel
        if (!yaHaTerminadoElNivel)
        {
            yaHaTerminadoElNivel = true;
            StartCoroutine(CuentaAtrasParaResultados());
        }
    }

    // Anotamos 20 puntos por bolo y restamos un bolo y lo actualizamos en el hud
    public void BoloAnotado()
    {
        puntuacionTotal += 20;
        bolosRestantes--;

        if (HudController.instance != null)
        {
            HudController.instance.ActualizarTextoPuntos(puntuacionTotal);
            HudController.instance.ActualizarTextoBolos(bolosRestantes);
        }
    }

    public void BoloRestado()
    {
        puntuacionTotal -= 20;
        bolosRestantes++;

        if (HudController.instance != null)
        {
            HudController.instance.ActualizarTextoPuntos(puntuacionTotal);
            HudController.instance.ActualizarTextoBolos(bolosRestantes);
        }
    }

    // La cuenta de atras para que cuando pase 7s nos lleve al final de nivel
    IEnumerator CuentaAtrasParaResultados()
    {
        yield return new WaitForSeconds(7f);

        puntuacionFinalEscena = puntuacionTotal;
        SceneManager.LoadScene("FinalNivel");
    }
}