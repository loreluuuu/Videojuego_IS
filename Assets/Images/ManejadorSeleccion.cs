using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorSeleccion : MonoBehaviour
{
    // Función que llamará el botón del Niño
    public void ElegirNino()
    {
        // Guardamos un número entero: 0 significa Niño
        PlayerPrefs.SetInt("PersonajeElegido", 0);
        PlayerPrefs.Save(); // Forzamos el guardado en memoria

        PasarAlSiguienteMapa();
    }

    // Función que llamará el botón de la Niña
    public void ElegirNina()
    {
        // Guardamos un número entero: 1 significa Niña
        PlayerPrefs.SetInt("PersonajeElegido", 1);
        PlayerPrefs.Save(); // Forzamos el guardado en memoria

        PasarAlSiguienteMapa();
    }

    private void PasarAlSiguienteMapa()
    {
        // Hacemos que suene el click genérico si el SoundManager está cerca
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }

        // 🗺️ Carga la pantalla que sigue en tu juego (por ejemplo, Mapamundos o Level_01)
        SceneManager.LoadScene("DialogoMundo1"); 
    }
}