using UnityEngine;

public class ActivadorPersonaje : MonoBehaviour
{
    [Header("Objetos Visuales Hijos")]
    [Tooltip("Arrastra aquí el objeto hijo 'Visual_Nino'")]
    public GameObject visualNino;

    [Tooltip("Arrastra aquí el objeto hijo 'Visual_Nina'")]
    public GameObject visualNina;

    void Awake()
    {
        // Buscamos qué personaje se guardó. Si no hay nada registrado, por defecto será 0 (Niño)
        int seleccion = PlayerPrefs.GetInt("PersonajeElegido", 0);

        if (seleccion == 0)
        {
            // Activa al Niño y apaga a la Niña
            if (visualNino != null) visualNino.SetActive(true);
            if (visualNina != null) visualNina.SetActive(false);
            Debug.Log("🧙‍♂️ ¡Visual del NIÑO activado con éxito!");
        }
        else
        {
            // Activa a la Niña y apaga al Niño
            if (visualNino != null) visualNino.SetActive(false);
            if (visualNina != null) visualNina.SetActive(true);
            Debug.Log("🧝‍♀️ ¡Visual de la NIÑA activado con éxito!");
        }
    }
}