using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevaPartidaManager : MonoBehaviour
{
    // Ir a selección de personaje
    public void IrASeleccionPersonaje()
    {
        SceneManager.LoadScene("SeleccionPersonajeScene");
    }

    // Regresar al menú principal
    public void RegresarAMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}