using UnityEngine;
using NatureBackgroundsPixelArt;

public class DiamondToken : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("TOCÓ DIAMANTE");

        PlayerController player =
             other.GetComponentInParent<PlayerController>();
        Debug.Log(player);

        if (player != null)
        {
            player.ActivateBigMode(10f);
        }

        GameManager.instance.AddScore(1000);

        Destroy(gameObject);
    }
}