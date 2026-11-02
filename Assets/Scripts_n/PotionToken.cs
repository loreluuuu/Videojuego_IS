using UnityEngine;
using NatureBackgroundsPixelArt;

public class PotionToken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.ActivatePotion(3f, 5f, 5f);
        }

        Destroy(gameObject);
    }
}