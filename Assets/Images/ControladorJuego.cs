using UnityEngine;

public class ControladorJuego : MonoBehaviour
{
    public void SalirDelJuego()
    {
#if UNITY_EDITOR
        // Esto detiene el modo Play en el Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Esto cierra el juego cuando ya está compilado (.exe, etc.)
        Application.Quit();
#endif
        Debug.Log("El juego se ha cerrado.");
    }
}