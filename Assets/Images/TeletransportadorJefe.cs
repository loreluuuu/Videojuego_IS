using UnityEngine;
using Unity.Cinemachine; // ➡️ OBLIGATORIO para poder controlar Cinemachine desde código
// NOTA: Si usas una versión más vieja de Unity y te da error la línea de arriba, cámbiala por: using Cinemachine;

public class TeletransportadorJefe : MonoBehaviour
{
    [Header("Destino")]
    public Transform puntoDestinoJefe;

    [Header("EfectosVisuales (Opcional)")]
    public GameObject efectoParticulas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (puntoDestinoJefe != null)
            {
                // 1. Teletransportamos al jugador
                other.transform.position = puntoDestinoJefe.position;

                // 2. ➡️ EL TRUCO PARA LA CÁMARA:
                // Buscamos la cámara virtual de Cinemachine que esté activa en el nivel
                CinemachineCamera camaraVirtual = FindFirstObjectByType<CinemachineCamera>();
                // NOTA: Si usas una versión vieja de Unity y te da error, cambia la línea de arriba por: CinemachineVirtualCamera camaraVirtual = FindFirstObjectByType<CinemachineVirtualCamera>();

                if (camaraVirtual != null)
                {
                    // Le ordenamos a Cinemachine que ignore el suavizado por este frame y salte directo al objetivo
                    camaraVirtual.OnTargetObjectWarped(other.transform, puntoDestinoJefe.position - other.transform.position);
                }

                // 3. Audio y partículas (Lo que ya tenías)
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlaySonidoBoton(); 
                }

                if (efectoParticulas != null)
                {
                    Instantiate(efectoParticulas, transform.position, Quaternion.identity);
                }

                Debug.Log("¡Jugador y Cámara teletransportados ante el Golem! 🔥");

                // 4. Desaparece el portal
                Destroy(gameObject);
            }
        }
    }
}