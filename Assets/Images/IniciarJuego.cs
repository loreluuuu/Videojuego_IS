using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // El sistema de Input de Unity 6

public class IniciarJuego : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [SerializeField] private string nombreSiguienteEscena = "MainMenu";

    void Update()
    {
        // Detectamos si el jugador presiona cualquier tecla o hace un clic
        if ((Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
            (Pointer.current != null && Pointer.current.press.wasPressedThisFrame))
        {
            // Ejecutamos la función modificada
            CargarMenuConSonido();
        }
    }

    void CargarMenuConSonido()
    {
        // 1. Le decimos al SoundManager que toque el sonido del botón/clic de inmediato
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }

        // 2. Cargamos la siguiente escena (el menú principal)
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}