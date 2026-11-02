using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemigoPerfecto : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 2f;
    private int direccionActual = 1; // 1 = Derecha, -1 = Izquierda

    [Header("Detección de Precipicios (Raycast)")]
    public Transform detectorSuelo; 
    public float distanciaLáser = 0.5f;
    public LayerMask capaSuelo; 

    [Header("Componentes Automáticos")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    void Awake()
    {
        // Capturamos los componentes pegados al enemigo
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Si usas animaciones, lo busca solo
    }

    void Update()
    {
        // 1. Detección de vacío adelante
        if (detectorSuelo != null)
        {
            RaycastHit2D infoSuelo = Physics2D.Raycast(detectorSuelo.position, Vector2.down, distanciaLáser, capaSuelo);

            // Si el láser deja de tocar el suelo, cambiamos de dirección inmediatamente
            if (infoSuelo.collider == false)
            {
                CambiarDireccion();
            }
        }
    }

    void FixedUpdate()
    {
        // 2. Mover al enemigo usando físicas estables en FixedUpdate (Evita atorarse en bloques)
        rb.linearVelocity = new Vector2(velocidad * direccionActual, rb.linearVelocity.y);

        // 3. Control de Animación (Opcional)
        // Si tu parámetro en el Animator para correr se llama "velocidadX", pásale la velocidad actual
        if (animator != null)
        {
            animator.SetFloat("velocidadX", Mathf.Abs(rb.linearVelocity.x));
        }
    }

    void CambiarDireccion()
    {
        // Invertimos la dirección matemática (si era 1 pasa a -1, si era -1 pasa a 1)
        direccionActual *= -1;

        // ⚡ SOLUCIÓN DE ANIMACIÓN Y COLISIONADORES: 
        // Volteamos solo el dibujo usando flipX. El colisionador y la escala se quedan intactos,
        // evitando que se aplaste como una línea o se atore en el suelo.
        if (direccionActual == 1)
        {
            spriteRenderer.flipX = false; // Mira a la derecha original
        }
        else
        {
            spriteRenderer.flipX = true;  // Se voltea a la izquierda limpiamente
        }

        // Mover el detector al lado correspondiente para que verifique el nuevo frente
        if (detectorSuelo != null)
        {
            Vector3 posicionLocal = detectorSuelo.localPosition;
            posicionLocal.x = Mathf.Abs(posicionLocal.x) * direccionActual;
            detectorSuelo.localPosition = posicionLocal;
        }
    }

    // Dibujamos el láser guía en la pestaña Escena
    private void OnDrawGizmos()
    {
        if (detectorSuelo != null)
        {
            Gizmos.color = Color.red;
            Vector2 inicio = (Vector2)detectorSuelo.position;
            Vector2 fin = inicio + (Vector2.down * distanciaLáser);
            Gizmos.DrawLine(inicio, fin);
        }
    }
}