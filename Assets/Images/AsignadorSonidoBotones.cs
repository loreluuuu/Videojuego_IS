using UnityEngine;
using UnityEngine.UI;

public class AsignadorSonidoBotones : MonoBehaviour
{
    void Start()
    {
        // ⚡ SOLUCIÓN AL WARNING: Usamos la nueva sintaxis limpia sin parámetros obsoletos
        Button[] todosLosBotones = Object.FindObjectsByType<Button>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        // Les asignamos la función de sonido por código de forma segura
        foreach (Button boton in todosLosBotones)
        {
        // Usamos onClick.AddListener que es el componente correcto para botones
        boton.onClick.AddListener(() => {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySonidoBoton();
            }
        });
        }
    }

    void IntentarReproducirSonido()
    {
        // Si el SoundManager eterno está vivo, mandamos la señal de audio
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoBoton();
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el SoundManager vivo. Recuerda iniciar siempre desde StartScreen.");
        }
    }
}