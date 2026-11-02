using UnityEngine;
using UnityEngine.InputSystem; 

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración")]
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    private Rigidbody2D rb;
    private bool isGrounded;
    
    // Referencias al sistema de Input
    private PlayerControls controls;
    private Vector2 moveInput;

    // 🔥 NUEVO: Referencia al Animator que estará en los hijos
    private Animator anim;

    void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 🔥 CLAVE: Busca de forma inteligente en el hijo activo (Visual_Nino o Visual_Nina)
        anim = GetComponentInChildren<Animator>();
        
        if (anim == null)
        {
            Debug.LogWarning("¡Aviso! No se encontró Animator en los hijos. Se reintentará cuando se active el personaje.");
        }
    }

    void OnEnable() => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        // Si por alguna razón cambió el personaje en ejecución y no se guardó el Animator, lo reasignamos de forma segura
        if (anim == null || !anim.gameObject.activeInHierarchy)
        {
            anim = GetComponentInChildren<Animator>();
        }

        // LEER MOVIMIENTO:
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        // SALTO:
        if (controls.Player.Jump.triggered && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.sonidoSalto);
            }
        }

        // 🔥 ENVIAR PARÁMETROS AL ANIMATOR CONTROLLER:
        if (anim != null)
        {
            // 1. Enviamos el valor absoluto de la caminata (siempre positivo) al parámetro "velocidad"
            anim.SetFloat("velocidad", Mathf.Abs(moveInput.x));

            // 2. Enviamos el estado del suelo al parámetro "enSuelo"
            anim.SetBool("enSuelo", isGrounded);
        }

        // 🔥 VOLTEAR EL PERSONAJE (Flip Sprite):
        if (moveInput.x > 0.1f)
        {
            transform.localScale = new Vector3(11.706f, 11.935f, 1f); // Escala original de tu captura mirando a la derecha
        }
        else if (moveInput.x < -0.1f)
        {
            transform.localScale = new Vector3(-11.706f, 11.935f, 1f); // Escala invertida en X para mirar a la izquierda
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void RecibirDanio(int cantidad, Vector2 direccionEmpuje)
    {
        Debug.Log("¡Ay! El jugador recibió daño. Dirección del golpe: " + direccionEmpuje);
        
        // Disparamos la animación de golpe en el hijo si está configurado
        if (anim != null) anim.SetTrigger("hurt");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; 
            rb.AddForce(direccionEmpuje * 10f, ForceMode2D.Impulse); 
        }
    }
}