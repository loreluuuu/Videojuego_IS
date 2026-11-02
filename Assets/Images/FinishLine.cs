using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ManejarFinDeNivel();
        }
    }

    void ManejarFinDeNivel()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }

        // Nivel 1 desbloquea nivel 2
        if (escenaActual == "Level_01")
        {
            PlayerPrefs.SetInt("Level2Unlocked", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Mapamundos");
        }
        // Nivel 2 desbloquea nivel 3
        else if (escenaActual == "Level_02M1")
        {
            PlayerPrefs.SetInt("Level3Unlocked", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Mapamundos");
        }
        // Nivel 3 termina el juego
        else if (escenaActual == "Level_02")
        {
            Debug.Log("Juego terminado");
            SceneManager.LoadScene("StartScreen");
        }
    }
}