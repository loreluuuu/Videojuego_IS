using UnityEngine;

public class SelectorPersonaje : MonoBehaviour
{
    [Header("Visuales de los Personajes")]
    public GameObject visualNinoObject;
    public GameObject visualNinaObject;

    void Awake()
    {
        // Lee la memoria de la clave exacta guardada en el menú
        int seleccion = PlayerPrefs.GetInt("PersonajeElegido", 0);

        if (seleccion == 0)
        {
            // Activa Niño, apaga Niña
            if (visualNinoObject != null) visualNinoObject.SetActive(true);
            if (visualNinaObject != null) visualNinaObject.SetActive(false);
            Debug.Log("🧙‍♂️ Visual establecido en: NIÑO");
        }
        else
        {
            // Activa Niña, apaga Niño
            if (visualNinoObject != null) visualNinoObject.SetActive(false);
            if (visualNinaObject != null) visualNinaObject.SetActive(true);
            Debug.Log("🧝‍♀️ Visual establecido en: NIÑA");
        }
    }
}