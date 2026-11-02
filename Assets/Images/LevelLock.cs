using UnityEngine;
using UnityEngine.UI;

public class LevelLock : MonoBehaviour
{
    public string claveDesbloqueo;

    private Button boton;
    private Image imagen;

    void Start()
    {
        boton = GetComponent<Button>();
        imagen = GetComponent<Image>();

        bool desbloqueado = PlayerPrefs.GetInt(claveDesbloqueo, 0) == 1;

        boton.interactable = desbloqueado;

        if (imagen != null)
        {
            imagen.color = desbloqueado ? Color.white : Color.gray;
        }
    }
}