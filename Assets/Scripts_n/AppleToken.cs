using UnityEngine;
using NatureBackgroundsPixelArt;

public class AppleToken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        // Sumar una vida al GameManager
        GameManager.instance.AddLife(1);

        Destroy(gameObject);
    }
}
