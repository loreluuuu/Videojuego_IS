using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private PlayerControls controls;
    private Vector2 moveInput;

    private bool isGrounded;
    private Rigidbody2D rb; // O Rigidbody si tu juego es 3D
    public bool isBig = false;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    void Awake()
    {
        controls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable() => controls.Player.Enable();
    void OnDisable() => controls.Player.Disable();

    void Update()
    {
        moveInput = controls.Player.Move.ReadValue<Vector2>();

        if (controls.Player.Jump.triggered)
        {
            Debug.Log("Se presionó el botón de salto. ¿isGrounded? " + isGrounded);
            
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                Debug.Log("¡Salto ejecutado!");
            }
        }
    }

    void FixedUpdate()
    {
        // 3. Aplicamos el movimiento físico
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("¡Brincó!");
    }

    public void ActivateBigMode(float duration)
    {
        StartCoroutine(BigModeRoutine(duration));
    }

    private System.Collections.IEnumerator BigModeRoutine(float duration)
    {
        isBig = true;

        transform.localScale = new Vector3(2f, 2f, 1f);

        Debug.Log("MODO GIGANTE");

        yield return new WaitForSeconds(duration);

        transform.localScale = new Vector3(1f, 1f, 1f);

        isBig = false;
    }
}