using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void NuevaPartida()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void ContinuarPartida()
    {
        SceneManager.LoadScene("Level1");
    }

    public void AbrirAjustes()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
