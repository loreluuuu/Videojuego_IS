using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [Header("Configuración de Ataque")]
    public GameObject ataqueArea;     // Arrastra aquí el objeto 'AtaqueZone'
    public float duracionAtaque = 0.2f; // Cuánto tiempo se queda activo el golpe
    
    private bool puedeAtacar = true;
    private Animator anim;

    void Awake()
    {
        // Asegurarnos de que el área de ataque empiece desactivada
        if (ataqueArea != null) ataqueArea.SetActive(false);
    }

    void Start()
    {
        // 🔥 CORRECCIÓN CLAVE: Busca de forma inteligente en el hijo activo (Niño o Niña)
        anim = GetComponentInChildren<Animator>();
    }

    // Este método se vincula con el Input System (Acción "Attack")
    public void OnAttack(InputValue value)
    {
        if (value.isPressed && puedeAtacar)
        {
            StartCoroutine(RealizarAtaque());
        }
    }

    IEnumerator RealizarAtaque()
    {
        puedeAtacar = false;
        
        // ⚡ REVISIÓN DE SEGURIDAD: Si por alguna razón se cambió de personaje,
        // nos aseguramos de volver a capturar el Animator del hijo que esté activo.
        if (anim == null || !anim.gameObject.activeInHierarchy)
        {
            anim = GetComponentInChildren<Animator>();
        }
        
        // 1. Activar animación (Dispara el trigger "attack" del Animator actual)
        if (anim != null) 
        {
            anim.SetTrigger("attack");
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sonidoAtaque);
        }

        // 2. Activar el colisionador de daño
        if (ataqueArea != null) ataqueArea.SetActive(true);

        // 3. Esperar un momento (el tiempo que dura el golpe)
        yield return new WaitForSeconds(duracionAtaque);

        // 4. Desactivar el colisionador
        if (ataqueArea != null) ataqueArea.SetActive(false);
        
        puedeAtacar = true;
    }

    // Código de colisión para dañar enemigos:
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // Asegúrate de que el Golem tenga el tag 'Enemy'
        {
            JefeGolem golem = collision.GetComponent<JefeGolem>();
            if (golem != null)
            {
                golem.RecibirDanio(2); // Le quita 2 de vida por cada espadazo
            }
        }
    }
}