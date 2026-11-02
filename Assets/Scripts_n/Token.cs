using UnityEngine;
using NatureBackgroundsPixelArt;

public class Token : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.instance.AddCoin(10);
        GameManager.instance.AddScore(100);
            
        Destroy(gameObject);
    }
}