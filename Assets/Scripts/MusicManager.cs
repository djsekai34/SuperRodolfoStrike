/******************************************************************************
 * @Autor: David
 * @Fecha: 21 de enero de 2026
 * @Proyecto: Super Rodolfo Strike
 * @Descripcion: Controlamos la musica que suena en cada apartado del juego y que no se pierda al cambiar de escena
 ******************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Configuración de Audio")]
    [SerializeField] private AudioSource fuenteMusica;

    [Header("Clips de Música")]
    public AudioClip musicaMenuYCreditos;
    public AudioClip musicaJugar;
    public AudioClip musicaFinDeNivel;

    private string escenaActual;

    void Awake()
    {
        // Creamos un sistema para que si cambiamos de escena no se pierda la musica
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Cuando carguemos una escena ponemos la musica
    void OnEnable()
    {
        SceneManager.sceneLoaded += AlCargarEscena;
    }

    // Cuando no la necesitemos la quitamos
    void OnDisable()
    {
        SceneManager.sceneLoaded -= AlCargarEscena;
    }

    // Aqui hacemmos que cada escena tenga su musica, menos la de ganar que la cargaremos en otro lado
    void AlCargarEscena(Scene escena, LoadSceneMode modo)
    {
        string nombreEscena = escena.name;

        if (nombreEscena == "Menu" || nombreEscena == "Creditos")
        {
            CambiarMusica(musicaMenuYCreditos);
        }
        else if (nombreEscena == "Juego") 
        {
            CambiarMusica(musicaJugar);
        }
    }

    public void CambiarMusica(AudioClip nuevoClip)
    {
        // Si ya tenemos una cancion no la reiniciamos
        if (fuenteMusica.clip == nuevoClip) return;

        // Si hemos cambiado de musica la paramos y ponemos la nueva musica
        fuenteMusica.Stop();
        fuenteMusica.clip = nuevoClip;
        fuenteMusica.Play();
        fuenteMusica.loop = true;
    }

    // Para el final de nivel
    public void PlayFinDeNivel()
    {
        CambiarMusica(musicaFinDeNivel);
        fuenteMusica.loop = false; 
    }
}