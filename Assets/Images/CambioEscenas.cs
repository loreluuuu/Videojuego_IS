using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscenas : MonoBehaviour
{
    public void CargarEscenaPorNombre(string nombreEscena)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }

        SceneManager.LoadScene(nombreEscena);
    }

    public void CargarEscenaPorIndice(int indiceEscena)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }

        SceneManager.LoadScene(indiceEscena);
    }

    public void SalirDelJuego()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }

        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}