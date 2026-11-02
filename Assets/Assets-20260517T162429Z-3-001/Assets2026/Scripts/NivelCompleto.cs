using UnityEngine;

public class NivelCompleto : MonoBehaviour
{
    public int numeroNivel; // aquí pones 1, 2, 3...

    public void GanarNivel()
    {
        // Marca el nivel como completado
        FindObjectOfType<LevelManager>().CompletarNivel(numeroNivel);

        Debug.Log("Nivel completado: " + numeroNivel);
    }
}