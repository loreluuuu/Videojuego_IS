using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Sistema de Unity 6 para la tecla Escape

public class MenuPausa : MonoBehaviour
{
    [Header("UI de la Pausa")]
    [Tooltip("Arrastra aquí el objeto 'PanelPausa' que crearemos en el Canvas.")]
    public GameObject objetoPanelPausa;

    // Variable interna para saber si el juego ya está pausado
    private bool juegoPausado = false;

    void Start()
    {
        // Nos aseguramos de que al iniciar el nivel la pausa esté apagada
        if (objetoPanelPausa != null)
        {
            objetoPanelPausa.SetActive(false);
        }
        
        // Por si acaso, nos aseguramos de que el tiempo corra normal
        Time.timeScale = 1f; 
    }

    void Update()
    {
        // Si el jugador presiona la tecla ESCAPE dentro del nivel, se activa/desactiva la pausa
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (juegoPausado)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Pausar()
    {
        juegoPausado = true;
        objetoPanelPausa.SetActive(true); // Muestra el menú de pausa en pantalla

        // Hace sonar el clic de la interfaz
        if (SoundManager.Instance != null) SoundManager.Instance.PlaySonidoBoton();

        // ➡️ TRUCO CLAVE: Congela el tiempo del juego (físicas, enemigos, timers se detienen)
        Time.timeScale = 0f; 
    }

    public void Continuar()
    {
        juegoPausado = false;
        objetoPanelPausa.SetActive(false); // Oculta el menú de pausa

        if (SoundManager.Instance != null) SoundManager.Instance.PlaySonidoBoton();

        // ➡️ Devuelve el tiempo a la normalidad
        Time.timeScale = 1f; 
    }

    public void SalirAlMapa()
    {
        // IMPORTANTÍSIMO: Antes de cambiar de escena, debes despausar el tiempo,
        // de lo contrario el mapa de la galaxia se quedará congelado.
        Time.timeScale = 1f;

        if (SoundManager.Instance != null) SoundManager.Instance.PlaySonidoBoton();

        // Cargamos la pantalla del espacio que registramos ayer
        SceneManager.LoadScene("Mapamundos");
    }
}