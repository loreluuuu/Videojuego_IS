using System.Collections;
using UnityEngine;
using NatureBackgroundsPixelArt;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour
{
    [Header("Estadísticas de Vida")]
    public int health = 3;
    private bool isDead = false;

    [Header("Movimiento y Patrulla")]
    public float speed = 2f;
    public float tiempoEsperaBorde = 1f; 
    private int direccionActual = 1;      // 1 = Derecha, -1 = Izquierda
    private bool estaEsperando = false;

    [Header("Detección de Precipicios (Raycast)")]
    public Transform groundCheck; // ➡️ Objeto hijo para revisar el suelo
    public float distanciaLáser = 0.7f;   // Aumentado a 0.7f para asegurar contacto
    public LayerMask groundLayer; // ➡️ Capa 'Ground'

    [Header("Combate / Ataque")]
    public float attackRange = 1.2f;
    public float attackCooldown = 2f;
    private float lastAttackTime;
    private bool isAttacking = false;
    private Transform player;

    [Header("Componentes")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void Update()
    {
        if (isDead || estaEsperando) return;

        // 1. CONTROL DE DISTANCIA CON EL JUGADOR
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                StartAttack();
                return;
            }
        }

        // 2. ⚡ DETECCIÓN DE PRECIPICIOS
        if (!isAttacking && groundCheck != null)
        {
            RaycastHit2D infoSuelo = Physics2D.Raycast(groundCheck.position, Vector2.down, distanciaLáser, groundLayer);

            // Si el rayo NO toca el suelo, encontramos vacío
            if (infoSuelo.collider == null)
            {
                StartCoroutine(RutinaEsperaYGirar());
            }
        }
    }

    void FixedUpdate()
    {
        if (isDead) return;

        // Si está esperando o atacando, frenamos por completo en X
        if (estaEsperando || isAttacking)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            if (anim != null)
            {
                anim.SetFloat("velocidad", 0f);
                anim.SetBool("isWalking", false);
            }
            return;
        }

        // Caminar normal usando físicas estables
        rb.linearVelocity = new Vector2(speed * direccionActual, rb.linearVelocity.y);

        if (anim != null)
        {
            anim.SetFloat("velocidad", Mathf.Abs(rb.linearVelocity.x));
            anim.SetBool("isWalking", true);
        }
    }

    IEnumerator RutinaEsperaYGirar()
    {
        estaEsperando = true;

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        if (anim != null)
        {
            anim.SetFloat("velocidad", 0f);
            anim.SetBool("isWalking", false);
        }

        yield return new WaitForSeconds(tiempoEsperaBorde);

        direccionActual *= -1;

        // Volteo visual del Sprite
        spriteRenderer.flipX = direccionActual == 1;

        // Reposicionar el detector al nuevo frente
        if (groundCheck != null)
        {
            Vector3 posCheck = groundCheck.localPosition;
            
            // 🔥 CORRECCIÓN PRECIPICIO: Si X es 0, lo forzamos a adelantarse un poco mecánicamente
            if (Mathf.Abs(posCheck.x) < 0.05f) {
                posCheck.x = 0.5f; 
            }

            posCheck.x = Mathf.Abs(posCheck.x) * direccionActual;
            groundCheck.localPosition = posCheck;
        }

        estaEsperando = false;
    }

    void StartAttack()
    {
        isAttacking = true;
        rb.linearVelocity = Vector2.zero;

        if (anim != null)
        {
            anim.SetFloat("velocidad", 0f);
            anim.SetBool("isWalking", false);
            anim.SetTrigger("attack"); // 🔄 Cambiado para activar tu trigger exacto de ataque si se llama así
        }

        lastAttackTime = Time.time;
        Invoke("FinishAttack", 0.6f);
    }

    void FinishAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.name == "AtaqueZone")
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        if (isDead) return;
        
        health--;
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            if (anim != null) 
            {
                anim.SetTrigger("hurt"); // ⚡ TRIGER EXACTO EN MINÚSCULAS
            }
        }
    }

    void Die()
    {
        isDead = true;
        
        rb.linearVelocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // Congela posición y rotación limpiamente
        
        if (anim != null) 
        {
            anim.SetTrigger("die"); // ⚡ TRIGGER EXACTO EN MINÚSCULAS
        }

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoMuerteEnemigo();
        }

        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && !isDead)
            {
                // --- ESTO ES LO NUEVO QUE AGREGAMOS ---
               PlayerController player =
                     collision.gameObject.GetComponentInParent<PlayerController>();
                if (player != null && player.isBig)
                {
                    Debug.Log("Jugador gigante, sin daño");
                    return;
                }
                // --------------------------------------

                // --- ESTO ES LO QUE YA TENÍAS Y DEBES DEJAR ---
                if (GameManager.instance != null)
                {
                    GameManager.instance.LoseLife(collision.gameObject);
                }
                // ----------------------------------------------
            }
        }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Vector2 inicio = (Vector2)groundCheck.position;
            Vector2 fin = inicio + (Vector2.down * distanciaLáser);
            Gizmos.DrawLine(inicio, fin);
        }
    }
}