using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI livesText; // Nueva referencia para el texto de vidas

    [Header("Game Settings")]
    public int maxLives = 3;
    public Transform respawnPoint; // Arrastra aquí un objeto vacío que sea el inicio del nivel

    private int score = 0;
    private int coins = 0;
    private int currentLives;
    private float startTime;

    void Awake()
    {
        if (instance == null) instance = this;
        startTime = Time.time;
        currentLives = maxLives;
        UpdateUI();
    }

    void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = (t % 60).ToString("00");
        timeText.text = "Time: " + minutes + ":" + seconds;
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        score += 10 * amount;
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateUI();
    }

    public void LoseLife(GameObject playerObject)
    {
        PlayerController player =
        playerObject.GetComponent<PlayerController>();

    if (player != null && player.isBig)
    {
        Debug.Log("Jugador gigante");
        return;
    }
        currentLives--;
        UpdateUI();
        if (currentLives > 0)
        {
            // Respawn
            playerObject.transform.position = respawnPoint.position;
            // Reiniciar velocidad
            playerObject.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
        else
        {
            Debug.Log("Game Over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        coinsText.text = "Coins: " + coins;
        livesText.text = "Vidas: " + currentLives;
    }

    public void AddLife(int amount)
    {
        currentLives += amount;
        UpdateUI();
    }

    public void SetCheckpoint(Vector3 newPosition)
    {
        respawnPoint.position = newPosition;
    }
}