using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Botones de niveles")]
    public Button[] niveles; // Arrastra aquí tus botones (nivel1, nivel2, etc.)

    void Start()
    {
        // Obtiene el nivel desbloqueado guardado (por defecto 1)
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelDesbloqueado", 1);

        // Recorre todos los niveles
        for (int i = 0; i < niveles.Length; i++)
        {
            if (i < nivelDesbloqueado)
            {
                // Nivel desbloqueado
                niveles[i].interactable = true;
            }
            else
            {
                // Nivel bloqueado
                niveles[i].interactable = false;
            }
        }
    }

    // 🔥 Cargar nivel al hacer clic
    public void CargarNivel(int numeroNivel)
    {
        SceneManager.LoadScene("Nivel" + numeroNivel);
    }

    // ✅ Marcar nivel como completado
    public void CompletarNivel(int numeroNivel)
    {
        int nivelActual = PlayerPrefs.GetInt("NivelDesbloqueado", 1);

        // Solo avanza si es el último nivel alcanzado
        if (numeroNivel >= nivelActual)
        {
            PlayerPrefs.SetInt("NivelDesbloqueado", numeroNivel + 1);
            PlayerPrefs.Save();
        }
    }

    // 🔁 Reiniciar progreso (opcional)
    public void ReiniciarProgreso()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Progreso reiniciado");
    }
}