/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Controlamos los botones para que nos lleve a la escena
 ******************************************************************************/

using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesControl : MonoBehaviour
{
    public void OnBotonJugar()
    {
        SceneManager.LoadScene("Juego");
    }

    public void OnBotonMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void OnBotonCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }
    public void OnBotonSalir()
    {
        Application.Quit();
    }
}

