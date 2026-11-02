using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        if (GameManager.instance == null) return;

        GameManager.instance.AddCoin(1);
        GameManager.instance.AddScore(10);

        Destroy(gameObject);
    }
}