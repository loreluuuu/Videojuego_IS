using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void IrANuevaPartida()
    {
        SceneManager.LoadScene("NuevaPartida");
    }

    public void IrASeleccionPersonaje()
    {
        SceneManager.LoadScene("SeleccionPersonaje");
    }

    public void IrAMapa()
    {
        SceneManager.LoadScene("MapaMundos");
    }

    public void IrAMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}