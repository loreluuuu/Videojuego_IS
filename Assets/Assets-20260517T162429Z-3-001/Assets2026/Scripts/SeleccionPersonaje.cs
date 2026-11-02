using UnityEngine;
using UnityEngine.SceneManagement;

public class SeleccionPersonaje : MonoBehaviour
{
    public void ElegirNiño()
    {
        PlayerPrefs.SetString("Personaje", "Niño");
        SceneManager.LoadScene("Mapamundos");
    }

    public void ElegirNiña()
    {
        PlayerPrefs.SetString("Personaje", "Niña");
        SceneManager.LoadScene("Mapamundos");
    }
}