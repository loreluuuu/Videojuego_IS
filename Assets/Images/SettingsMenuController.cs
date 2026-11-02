using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderFX;

    public void Regresar()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
