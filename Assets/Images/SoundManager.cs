using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.InputSystem; // ⚡ NUEVO: Necesario para usar el nuevo Input System en Unity 6

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Componentes de Audio")]
    public AudioSource emisorMusica; 
    public AudioSource emisorSFX;    

    [Header("Músicas de Fondo (Clips de Audio)")]
    public AudioClip musicaMenu;
    public AudioClip musicaNivel1;
    public AudioClip musicaNivel2; 

    [Header("Sonidos de Menús")]
    public AudioClip sonidoBoton;

    [Header("Sonidos de Gameplay")]
    public AudioClip sonidoSalto;
    public AudioClip sonidoAtaque;
    public AudioClip sonidoMoneda;
    public AudioClip sonidoMuerteEnemigo;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += AlCambiarDeEscena;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= AlCambiarDeEscena;
    }

    void Start()
    {
        PlayMusica(musicaMenu);
    }

    void Update()
    {
        // ⚡ SOLUCIÓN MODERNA: Reemplazamos el Input clásico por el nuevo sistema compatible
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            string escenaActual = SceneManager.GetActiveScene().name;

            // 1. Si estás en el Menú Principal y presionas Escape, regresa a la StartScreen
            if (escenaActual == "MainMenu")
            {
                SceneManager.LoadScene("StartScreen");
            }
            // 2. Si estás en la pantalla de Ajustes u Opciones, te regresa al Menú Principal
            else if (escenaActual == "SettingsMenu")
            {
                SceneManager.LoadScene("MainMenu");
            }

            // 3. Si estás en la pantalla de Nueva Partida, te regresa al Menú Principal
            if (escenaActual == "NuevaPartidaScene")
            {
                SceneManager.LoadScene("MainMenu");
            }

            // 4. Si estás en la selección de personaje, te regresa a la pantalla de Nueva Partida
            else if (escenaActual == "SeleccionPersonajeScene")
            {
                SceneManager.LoadScene("NuevaPartidaScene");
            }

            // 5. Si estás en el mapa o diálogos, que te devuelva al menú principal de forma segura
            else if (escenaActual == "DialogoMundo1")
            {
                SceneManager.LoadScene("SelectionPersonajeScene");
            }

            // 6. Si estás en el mapa del mundo, que te devuelva al menú principal de forma segura
            if (escenaActual == "Mapamundos")
            {
                SceneManager.LoadScene("MainMenu");
            }
            // 7. Si estás jugando en un nivel
            else if (escenaActual == "Level_01" || escenaActual == "Level_02")
            {
                Debug.Log("Presionaste escape en pleno juego. Aquí puedes abrir un panel de pausa.");
            }
        }
    }

    void AlCambiarDeEscena(Scene escena, LoadSceneMode modo)
    {
        // 1. SI ENTRAS AL NIVEL 1
        if (escena.name == "Level_01")
        {
            PlayMusica(musicaNivel1);
        }
        
        // 2. SI ENTRAS AL NIVEL 2
        else if (escena.name == "Level_02") 
        {
            PlayMusica(musicaNivel2);
        }
        
        // 3. TU CONFIGURACIÓN DE MENÚS (Mantenida intacta porque está perfecta)
        else if (escena.name == "StartScreen" || escena.name == "MainMenu" || escena.name == "NuevaPartidaScene" || escena.name == "SeleccionPersonajeScene"
        || escena.name == "Mapamundos" || escena.name == "DialogoMundo1" || escena.name == "SettingsMenu")
        {
            PlayMusica(musicaMenu);
        }
        
        // 4. POR SEGURIDAD: Evita sonidos encimados en pantallas vacías
        else
        {
            if (emisorMusica != null)
            {
                emisorMusica.Stop();
            }
        }
    }

    public void PlayMusica(AudioClip nuevaMusica)
    {
        if (emisorMusica == null || nuevaMusica == null) return;
        if (emisorMusica.clip == nuevaMusica && emisorMusica.isPlaying) return;

        emisorMusica.Stop(); 
        emisorMusica.clip = nuevaMusica;
        emisorMusica.loop = true; 
        emisorMusica.Play(); 
    }

    public void PlaySFX(AudioClip clipEfecto)
    {
        if (emisorSFX != null && clipEfecto != null)
        {
            emisorSFX.PlayOneShot(clipEfecto);
        }
    }

    public void PlaySonidoBoton() { PlaySFX(sonidoBoton); }
    public void PlaySonidoMuerteEnemigo() { PlaySFX(sonidoMuerteEnemigo); }
    public void PlaySonidoSalto() { PlaySFX(sonidoSalto); }
    public void PlaySonidoAtaque() { PlaySFX(sonidoAtaque); }
    public void PlaySonidoMoneda() { PlaySFX(sonidoMoneda); }
}