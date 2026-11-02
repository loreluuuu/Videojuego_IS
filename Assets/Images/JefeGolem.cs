using UnityEngine;
using NatureBackgroundsPixelArt;

public class JefeGolem : MonoBehaviour
{
    [Header("Configuración del Jefe")]
    public float velocidad = 2f;
    public float distancia = 3f;
    public int danio = 1;
    public int vida = 20;

    [Header("Tiempos de Ataque")]
    public float tiempoEntreAtaques = 3f; // Cada cuántos segundos intenta atacar
    private float cronometroAtaque;

    private Vector3 puntoInicial;
    private bool moviendoDerecha = true;
    
    // Componentes del Golem
    private Animator anim;
    private bool estaAtacando = false;
    public GameObject burbujaTeletransporte;

    void Start()
    {
        puntoInicial = transform.position;
        anim = GetComponent<Animator>(); // Buscamos el Animator
        cronometroAtaque = tiempoEntreAtaques; // Inicializamos el reloj
    }

    void Update()
    {
        // Control del reloj para lanzar ataques automáticos
        cronometroAtaque -= Time.deltaTime;
        if (cronometroAtaque <= 0 && !estaAtacando)
        {
            LanzarGolpe();
        }

        // Si el jefe está reproduciendo la animación de ataque, se queda quieto
        if (estaAtacando) return;

        // PATRULLA BÁSICA (Solo se mueve si no está atacando)
        if (moviendoDerecha)
        {
            transform.Translate(Vector2.right * velocidad * Time.deltaTime);

            if (transform.position.x >= puntoInicial.x + distancia)
            {
                moviendoDerecha = false;
                Girar();
            }
        }
        else
        {
            transform.Translate(Vector2.left * velocidad * Time.deltaTime);

            if (transform.position.x <= puntoInicial.x - distancia)
            {
                moviendoDerecha = true;
                Girar();
            }
        }

        // Enviamos la velocidad al Animator para el caminar
        if (anim != null)
        {
            anim.SetFloat("velocidadX", Mathf.Abs(velocidad));
        }
    }

    void LanzarGolpe()
    {
        estaAtacando = true;
        cronometroAtaque = tiempoEntreAtaques; // Reiniciamos el reloj para el próximo golpe

        if (anim != null)
        {
            anim.SetTrigger("atacar"); // ➡️ Encendemos el botón en el Animator
            anim.SetFloat("velocidadX", 0f); // Frenamos la animación de caminar
        }
    }

    // 🔥 TRUCO CLAVE: Esta función la llamaremos desde el final de la animación de ataque en Unity
    public void FinDelAtaque()
    {
        estaAtacando = false;
    }

    void Girar()
    {
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    // Daño al jugador por contacto
    // Daño al jugador por contacto
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // --- NUEVO: Verificamos si el jugador es invencible ---
           PlayerController player = other.GetComponent<PlayerController>();

            if (player != null && player.isBig)
            {
                Debug.Log("Jugador gigante");
                return;
            }
            // -----------------------------------------------------

            PlayerMovement jugador = other.GetComponent<PlayerMovement>();
            if (jugador != null)
            {
                Vector2 direccionEmpuje = (other.transform.position - transform.position).normalized;
                jugador.RecibirDanio(danio, direccionEmpuje);
            }
        }
    }

    public void RecibirDanio(int dmg)
    {
        vida -= dmg;
        Debug.Log("¡Golem golpeado! Vida restante: " + vida);
        if (vida <= 0) Morir();
    }

    public void Morir() 
    {
        Debug.Log("💀 ¡Jefe derrotado!");

        // 🔊 Reproducir el sonido de muerte que agregamos al SoundManager
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySonidoMuerteEnemigo();
        }

        // 🔥 ACTIVAR LA BURBUJA: Aquí hacemos aparecer mágicamente el teletransportador
        if (burbujaTeletransporte != null)
        {
            burbujaTeletransporte.SetActive(true);
            Debug.Log("✨ Burbuja de teletransporte activada. ¡Ve a la plataforma final!");
        }

        // Destruir al jefe
        Destroy(gameObject);
    }
}