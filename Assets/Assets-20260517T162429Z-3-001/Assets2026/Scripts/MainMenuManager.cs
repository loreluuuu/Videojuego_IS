using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void IrANuevaPartida()
    {
        SceneManager.LoadScene("NuevaPartidaScene");
    }
}